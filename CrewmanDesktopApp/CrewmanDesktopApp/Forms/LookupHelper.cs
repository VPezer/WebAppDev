using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace CrewmanDesktopApp.Forms
{
    /// <summary>Jednoobrazno postavljanje LookUpEdit editora (padajući popis s jednim stupcem "Name").</summary>
    internal static class LookupHelper
    {
        /// <param name="edit">Editor koji se postavlja.</param>
        /// <param name="dataSource">Lista objekata s javnim svojstvima Id i Name (Rank ili Vessel).</param>
        public static void Configure(LookUpEdit edit, object dataSource)
        {
            RepositoryItemLookUpEdit properties = edit.Properties;
            properties.DataSource = dataSource;
            properties.DisplayMember = "Name";
            properties.ValueMember = "Id";
            properties.Columns.Clear();
            properties.Columns.Add(new LookUpColumnInfo("Name"));
            properties.ShowHeader = false;
            properties.ShowFooter = false;
            properties.DropDownRows = 12;
            // korisnik može samo birati iz popisa (ne može upisati proizvoljan tekst)
            properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        }
    }
}
