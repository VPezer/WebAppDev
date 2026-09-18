using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CrewmanDesktopApp.Data;
using CrewmanDesktopApp.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CrewmanDesktopApp.Forms
{
    /// <summary>
    /// Glavni prozor: pretraga pomoraca (tekst + filtri po rangu i brodu), tablica rezultata
    /// i gumbi za dodavanje, uređivanje i brisanje.
    /// </summary>
    public partial class MainForm : XtraForm
    {
        /// <summary>Vrijednost stavke "Svi rangovi" / "Svi brodovi" u filterima (nijedan pravi Id nije 0).</summary>
        private const int AllItemsId = 0;

        private List<Rank> _ranks = new List<Rank>();
        private List<Vessel> _vessels = new List<Vessel>();

        /// <summary>Dok je true, promjene filtera ne pokreću pretragu (koristi se pri programskom punjenju).</summary>
        private bool _suspendFilterEvents;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLookups();
                RefreshList(null);
            }
            catch (Exception ex)
            {
                ShowError("Učitavanje podataka nije uspjelo.", ex);
                UpdateButtons();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            textSearch.Focus();
        }

        // ------------------------------------------------------------------
        // Šifrarnici (rangovi i brodovi) i filtri
        // ------------------------------------------------------------------

        private void LoadLookups()
        {
            _ranks = LookupRepository.GetRanks();
            _vessels = LookupRepository.GetVessels();

            _suspendFilterEvents = true;
            try
            {
                int? selectedRank = GetFilterValue(lookupRank);
                int? selectedVessel = GetFilterValue(lookupVessel);

                var rankItems = new List<Rank> { new Rank { Id = AllItemsId, Name = "Svi rangovi" } };
                rankItems.AddRange(_ranks);
                LookupHelper.Configure(lookupRank, rankItems);
                lookupRank.EditValue = selectedRank ?? AllItemsId;

                var vesselItems = new List<Vessel> { new Vessel { Id = AllItemsId, Name = "Svi brodovi" } };
                vesselItems.AddRange(_vessels);
                LookupHelper.Configure(lookupVessel, vesselItems);
                lookupVessel.EditValue = selectedVessel ?? AllItemsId;
            }
            finally
            {
                _suspendFilterEvents = false;
            }
        }

        /// <summary>Id odabran u filteru ili null ako je odabrano "Svi ...".</summary>
        private static int? GetFilterValue(LookUpEdit edit)
        {
            return edit.EditValue is int id && id != AllItemsId ? id : (int?)null;
        }

        private bool HasActiveFilter
        {
            get
            {
                return textSearch.Text.Trim().Length > 0
                    || GetFilterValue(lookupRank).HasValue
                    || GetFilterValue(lookupVessel).HasValue;
            }
        }

        private void filter_EditValueChanged(object sender, EventArgs e)
        {
            if (_suspendFilterEvents)
            {
                return;
            }

            // pretraga se pokreće 300 ms nakon zadnje promjene, da ne ide upit na svaki pritisak tipke
            timerSearch.Stop();
            timerSearch.Start();
        }

        private void timerSearch_Tick(object sender, EventArgs e)
        {
            timerSearch.Stop();
            RefreshListSafe(null);
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void ClearFilters()
        {
            timerSearch.Stop();
            ResetFilterEditors();
            RefreshListSafe(null);
            textSearch.Focus();
        }

        private void ResetFilterEditors()
        {
            _suspendFilterEvents = true;
            try
            {
                textSearch.EditValue = null;
                lookupRank.EditValue = AllItemsId;
                lookupVessel.EditValue = AllItemsId;
            }
            finally
            {
                _suspendFilterEvents = false;
            }
        }

        // ------------------------------------------------------------------
        // Popis pomoraca
        // ------------------------------------------------------------------

        /// <summary>
        /// Ponovno izvršava pretragu i osvježava tablicu; pokušava zadržati (ili postaviti) označeni redak.
        /// Vraća true ako je traženi redak pronađen i označen.
        /// </summary>
        private bool RefreshList(int? selectId)
        {
            var focused = gridView.GetFocusedRow() as SeafarerListItem;
            int? idToSelect = selectId ?? (focused != null ? focused.Id : (int?)null);

            List<SeafarerListItem> items = SeafarerRepository.Search(
                textSearch.Text,
                GetFilterValue(lookupRank),
                GetFilterValue(lookupVessel));

            gridControl.DataSource = items;

            bool selected = idToSelect.HasValue && SelectRow(idToSelect.Value);

            UpdateStatus(items.Count);
            UpdateButtons();
            return selected;
        }

        private void RefreshListSafe(int? selectId)
        {
            try
            {
                RefreshList(selectId);
            }
            catch (Exception ex)
            {
                ShowError("Pretraga nije uspjela.", ex);
            }
        }

        private bool SelectRow(int id)
        {
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                var item = gridView.GetRow(rowHandle) as SeafarerListItem;
                if (item != null && item.Id == id)
                {
                    gridView.FocusedRowHandle = rowHandle;
                    gridView.MakeRowVisible(rowHandle);
                    return true;
                }
            }
            return false;
        }

        private void UpdateStatus(int count)
        {
            if (count == 0)
            {
                labelStatus.Text = HasActiveFilter
                    ? "Nema pomoraca koji odgovaraju pretrazi."
                    : "Još nema pomoraca - kliknite \"Dodaj pomorca\".";
                return;
            }

            labelStatus.Text = "Prikazano: " + count + " " + Plural(count, "pomorac", "pomorca", "pomoraca")
                + (HasActiveFilter ? "  (filtrirano)" : string.Empty);
        }

        /// <summary>Hrvatski oblik imenice uz broj: 1 pomorac, 2 pomorca, 5 pomoraca.</summary>
        private static string Plural(int count, string one, string few, string many)
        {
            int mod10 = count % 10;
            int mod100 = count % 100;

            if (mod10 == 1 && mod100 != 11)
            {
                return one;
            }
            if (mod10 >= 2 && mod10 <= 4 && (mod100 < 12 || mod100 > 14))
            {
                return few;
            }
            return many;
        }

        private void UpdateButtons()
        {
            bool hasRow = gridView.GetFocusedRow() is SeafarerListItem;
            btnEdit.Enabled = hasRow;
            btnDelete.Enabled = hasRow;
        }

        private SeafarerListItem GetSelectedItem()
        {
            return gridView.GetFocusedRow() as SeafarerListItem;
        }

        // ------------------------------------------------------------------
        // Dodavanje / uređivanje / brisanje
        // ------------------------------------------------------------------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSeafarer();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelected();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void AddSeafarer()
        {
            using (var form = new SeafarerEditForm(null, _ranks, _vessels))
            {
                DialogResult result = form.ShowDialog(this);
                AfterDialog(form, result);
            }
        }

        private void EditSelected()
        {
            SeafarerListItem item = GetSelectedItem();
            if (item == null)
            {
                return;
            }

            Seafarer seafarer;
            try
            {
                seafarer = SeafarerRepository.GetById(item.Id);
            }
            catch (Exception ex)
            {
                ShowError("Učitavanje pomorca nije uspjelo.", ex);
                return;
            }

            if (seafarer == null)
            {
                XtraMessageBox.Show(this,
                    "Pomorac " + item.FullName + " više ne postoji u bazi. Popis će se osvježiti.",
                    "Crewman", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshListSafe(null);
                return;
            }

            using (var form = new SeafarerEditForm(seafarer, _ranks, _vessels))
            {
                DialogResult result = form.ShowDialog(this);
                AfterDialog(form, result);
            }
        }

        /// <summary>Nakon zatvaranja dijaloga osvježava šifrarnike (ako je dodan rang/brod) i popis (ako je pomorac spremljen).</summary>
        private void AfterDialog(SeafarerEditForm form, DialogResult result)
        {
            try
            {
                if (form.LookupsChanged)
                {
                    // u dijalogu je dodan novi rang ili brod (i kad se odustalo od pomorca) - osvježi popise i filtre
                    LoadLookups();
                }

                if (result != DialogResult.OK)
                {
                    return;
                }

                if (!RefreshList(form.SavedSeafarerId) && HasActiveFilter)
                {
                    // spremljeni pomorac ne prolazi trenutne filtre - makni ih da korisnik vidi što je spremio
                    ResetFilterEditors();
                    RefreshList(form.SavedSeafarerId);
                }
            }
            catch (Exception ex)
            {
                ShowError("Osvježavanje popisa nije uspjelo.", ex);
            }
        }

        private void DeleteSelected()
        {
            SeafarerListItem item = GetSelectedItem();
            if (item == null)
            {
                return;
            }

            DialogResult answer = XtraMessageBox.Show(this,
                "Želite li obrisati pomorca \"" + item.FullName + "\"?\n\nOva se radnja ne može poništiti.",
                "Brisanje pomorca", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            try
            {
                SeafarerRepository.Delete(item.Id);
            }
            catch (Exception ex)
            {
                ShowError("Brisanje nije uspjelo.", ex);
                return;
            }

            RefreshListSafe(null);
        }

        // ------------------------------------------------------------------
        // Događaji tablice i tipkovnice
        // ------------------------------------------------------------------

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            // dvoklik otvara uređivanje samo ako je kliknut redak (ne zaglavlje ili prazan prostor)
            GridHitInfo hit = gridView.CalcHitInfo(gridControl.PointToClient(Control.MousePosition));
            if (hit.InRow || hit.InRowCell)
            {
                EditSelected();
            }
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            UpdateButtons();
        }

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditSelected();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteSelected();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Insert)
            {
                AddSeafarer();
                e.Handled = true;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                AddSeafarer();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                textSearch.Focus();
                textSearch.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                RefreshListSafe(null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Esc u nekom od filtera (dok padajući popis nije otvoren) briše sve filtre.
        /// Obrađuje se ovdje, prije nego što tipku vidi sam editor, jer DevExpress editori Esc koriste za vlastiti "undo".
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && layoutFilters.ContainsFocus && HasActiveFilter
                && !lookupRank.IsPopupOpen && !lookupVessel.IsPopupOpen)
            {
                ClearFilters();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowError(string message, Exception ex)
        {
            XtraMessageBox.Show(this, message + "\n\n" + ex.Message, "Crewman - greška",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
