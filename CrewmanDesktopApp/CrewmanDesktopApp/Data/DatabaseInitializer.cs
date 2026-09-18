using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CrewmanDesktopApp.Data
{
    public enum DatabaseStatus
    {
        /// <summary>Baza i tablice postoje - aplikacija može raditi.</summary>
        Ready,

        /// <summary>Baza ne postoji ili u njoj nema tablica - treba pokrenuti skriptu.</summary>
        Missing,

        /// <summary>Baza postoji, ali trenutni korisnik nema pravo pristupa.</summary>
        AccessDenied
    }

    /// <summary>
    /// Provjerava postoji li baza i po potrebi je kreira pokretanjem ugrađene T-SQL skripte
    /// (Database\CrewmanDatabase.sql). Ista skripta može se pokrenuti i ručno u SSMS-u.
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>Naziv baze koju kreira skripta CrewmanDatabase.sql.</summary>
        public const string ScriptDatabaseName = "Crewman";

        private const string ScriptResourceName = "CrewmanDatabase.sql";
        private const int ErrorCannotOpenDatabase = 4060;   // "Cannot open database ... requested by the login"

        /// <summary>Naziv baze iz connection stringa u App.config.</summary>
        public static string ConfiguredDatabaseName
        {
            get { return new SqlConnectionStringBuilder(Db.ConnectionString).InitialCatalog; }
        }

        /// <summary>
        /// Automatsko kreiranje je moguće samo ako connection string pokazuje na bazu istog naziva
        /// kao u skripti (Crewman); inače korisnik skriptu pokreće ručno.
        /// </summary>
        public static bool CanAutoCreate
        {
            get { return string.Equals(ConfiguredDatabaseName, ScriptDatabaseName, StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// Spaja se na bazu iz App.config i provjerava postoji li tablica Seafarers.
        /// Baca SqlException ako server nije dostupan ili prijava ne uspije.
        /// </summary>
        public static DatabaseStatus CheckStatus()
        {
            try
            {
                using (SqlConnection connection = Db.OpenConnection())
                using (var command = new SqlCommand(
                    "SELECT CASE WHEN OBJECT_ID(N'dbo.Seafarers', N'U') IS NULL THEN 0 ELSE 1 END;", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar()) == 1 ? DatabaseStatus.Ready : DatabaseStatus.Missing;
                }
            }
            catch (SqlException ex) when (ex.Errors.Cast<SqlError>().Any(e => e.Number == ErrorCannotOpenDatabase))
            {
                // greška 4060 znači "baza ne postoji" ILI "postoji, ali korisnik nema pristup"
                return DatabaseExistsOnServer() ? DatabaseStatus.AccessDenied : DatabaseStatus.Missing;
            }
        }

        /// <summary>Preko baze master provjerava postoji li konfigurirana baza; ako ni master nije dostupan, pretpostavlja da ne postoji.</summary>
        private static bool DatabaseExistsOnServer()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(Db.ConnectionString) { InitialCatalog = "master" };
                using (var connection = new SqlConnection(builder.ConnectionString))
                using (var command = new SqlCommand("SELECT DB_ID(@Name);", connection))
                {
                    command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 128).Value = ConfiguredDatabaseName;
                    connection.Open();
                    return command.ExecuteScalar() != DBNull.Value;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Spaja se na bazu master i izvršava ugrađenu skriptu (kreira bazu, tablice i početne podatke).
        /// Skripta je idempotentna pa je ponovno pokretanje bezopasno.
        /// </summary>
        public static void CreateDatabase()
        {
            var builder = new SqlConnectionStringBuilder(Db.ConnectionString) { InitialCatalog = "master" };

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                foreach (string batch in SplitBatches(LoadScript()))
                {
                    using (var command = new SqlCommand(batch, connection))
                    {
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>Učitava SQL skriptu ugrađenu u .exe (EmbeddedResource u .csproj).</summary>
        internal static string LoadScript()
        {
            using (Stream stream = typeof(DatabaseInitializer).Assembly.GetManifestResourceStream(ScriptResourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("Ugrađena SQL skripta '" + ScriptResourceName + "' nije pronađena.");
                }
                using (var reader = new StreamReader(stream, Encoding.UTF8, true))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// SqlCommand ne razumije naredbu GO (to je naredba SSMS/sqlcmd alata),
        /// pa skriptu dijelimo na batch-eve na svakom retku koji sadrži samo GO.
        /// </summary>
        internal static IEnumerable<string> SplitBatches(string script)
        {
            string[] batches = Regex.Split(
                script,
                @"^[ \t]*GO[ \t]*(?:--[^\r\n]*)?[ \t]*\r?$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            return batches
                .Select(batch => batch.Trim())
                .Where(batch => batch.Length > 0);
        }
    }
}
