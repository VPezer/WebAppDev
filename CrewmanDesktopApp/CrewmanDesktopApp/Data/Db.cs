using System.Configuration;
using System.Data.SqlClient;

namespace CrewmanDesktopApp.Data
{
    /// <summary>Otvara veze prema SQL Serveru na temelju connection stringa "Crewman" iz App.config.</summary>
    public static class Db
    {
        public const string ConnectionStringName = "Crewman";

        public static string ConnectionString
        {
            get
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConnectionStringName];
                if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    throw new ConfigurationErrorsException(
                        "U App.config nedostaje connection string '" + ConnectionStringName + "'.");
                }
                return settings.ConnectionString;
            }
        }

        /// <summary>Vraća otvorenu vezu. Pozivatelj je odgovoran za Dispose (koristiti unutar using bloka).</summary>
        public static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
