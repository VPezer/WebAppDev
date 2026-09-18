using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CrewmanDesktopApp.Data;
using CrewmanDesktopApp.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;

namespace CrewmanDesktopApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Hrvatski format datuma i brojeva u cijeloj aplikaciji
            var culture = CultureInfo.GetCultureInfo("hr-HR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => ShowUnexpectedError(e.Exception);

            // DevExpress: prilagodba visokoj rezoluciji, font, tema i hrvatski nazivi gumba
            WindowsFormsSettings.SetDPIAware();
            WindowsFormsSettings.DefaultFont = new Font("Segoe UI", 9f);
            UserLookAndFeel.Default.SetSkinStyle("WXI");
            CroatianEditorsLocalizer.Activate();

            if (!PrepareDatabase())
            {
                return;
            }

            Application.Run(new MainForm());
        }

        /// <summary>
        /// Provjerava vezu na bazu. Ako baza još ne postoji, nudi automatsko kreiranje
        /// (pokreće ugrađenu skriptu Database\CrewmanDatabase.sql).
        /// </summary>
        private static bool PrepareDatabase()
        {
            DatabaseStatus status;
            try
            {
                status = DatabaseInitializer.CheckStatus();
            }
            catch (Exception ex)
            {
                ShowStartupError(
                    "Nije moguće spojiti se na SQL Server.\n" +
                    "Provjerite je li SQL Server pokrenut i je li connection string u App.config ispravan.", ex);
                return false;
            }

            if (status == DatabaseStatus.Ready)
            {
                return true;
            }

            if (status == DatabaseStatus.AccessDenied)
            {
                ShowStartupError(
                    "Baza '" + DatabaseInitializer.ConfiguredDatabaseName + "' postoji, ali trenutni Windows korisnik nema pravo pristupa.\n" +
                    "Dodijelite korisniku pristup bazi u SQL Serveru ili u App.config upišite korisnika koji ga ima.", null);
                return false;
            }

            if (!DatabaseInitializer.CanAutoCreate)
            {
                ShowStartupError(
                    "Baza '" + DatabaseInitializer.ConfiguredDatabaseName + "' ne postoji ili je prazna.\n" +
                    "Pokrenite skriptu Database\\CrewmanDatabase.sql u SQL Server Management Studiju " +
                    "(skripta kreira bazu naziva '" + DatabaseInitializer.ScriptDatabaseName + "').", null);
                return false;
            }

            DialogResult answer = XtraMessageBox.Show(
                "Baza podataka '" + DatabaseInitializer.ScriptDatabaseName + "' još ne postoji ili je prazna.\n\n" +
                "Želite li je sada pripremiti (baza, tablice i demo podaci)?",
                "Crewman - prvo pokretanje",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return false;
            }

            try
            {
                DatabaseInitializer.CreateDatabase();
                return true;
            }
            catch (Exception ex)
            {
                ShowStartupError(
                    "Kreiranje baze nije uspjelo.\n" +
                    "Pokrenite skriptu Database\\CrewmanDatabase.sql ručno u SQL Server Management Studiju.", ex);
                return false;
            }
        }

        private static void ShowStartupError(string message, Exception ex)
        {
            string text = message + "\n\nConnection string (App.config):\n" + SafeConnectionString();
            if (ex != null)
            {
                text += "\n\nDetalji:\n" + ex.Message;
            }
            XtraMessageBox.Show(text, "Crewman - greška pri pokretanju", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ShowUnexpectedError(Exception ex)
        {
            XtraMessageBox.Show(
                "Dogodila se neočekivana greška:\n\n" + ex.Message,
                "Crewman - greška",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static string SafeConnectionString()
        {
            try
            {
                // lozinka (ako se koristi SQL prijava) ne smije završiti u poruci na ekranu
                var builder = new SqlConnectionStringBuilder(Db.ConnectionString);
                if (builder.Password.Length > 0)
                {
                    builder.Password = "*****";
                }
                return builder.ConnectionString;
            }
            catch (Exception ex)
            {
                return "(" + ex.Message + ")";
            }
        }
    }
}
