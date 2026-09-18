using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CrewmanDesktopApp.Data;
using CrewmanDesktopApp.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace CrewmanDesktopApp.Forms
{
    /// <summary>Dijalog za unos novog ili uređivanje postojećeg pomorca.</summary>
    public partial class SeafarerEditForm : XtraForm
    {
        /// <summary>Vrijednost stavke "(nije ukrcan)" u popisu brodova (nijedan pravi Id nije 0).</summary>
        private const int NoVesselId = 0;

        private const int MinimumAge = 16;

        private static readonly Regex EmailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]{2,}$", RegexOptions.Compiled);

        /// <summary>Ponuđene nacionalnosti; korisnik može upisati i bilo koju drugu.</summary>
        private static readonly string[] Nationalities =
        {
            "Hrvatska", "Bosna i Hercegovina", "Slovenija", "Srbija", "Crna Gora", "Sjeverna Makedonija",
            "Italija", "Grčka", "Turska", "Bugarska", "Rumunjska", "Poljska", "Ukrajina", "Rusija",
            "Njemačka", "Nizozemska", "Norveška", "Velika Britanija", "Filipini", "Indija", "Indonezija",
            "Kina", "SAD"
        };

        private readonly Seafarer _seafarer;
        private List<Rank> _ranks;
        private List<Vessel> _vessels;
        private bool _loading;

        /// <summary>Id spremljenog pomorca (nakon što dijalog vrati DialogResult.OK).</summary>
        public int SavedSeafarerId { get; private set; }

        /// <summary>True ako je korisnik u ovom dijalogu dodao novi rang ili brod (glavni prozor tada osvježava filtre).</summary>
        public bool LookupsChanged { get; private set; }

        /// <param name="seafarer">Pomorac za uređivanje ili null za novog.</param>
        /// <param name="ranks">Svi rangovi iz baze.</param>
        /// <param name="vessels">Svi brodovi iz baze.</param>
        public SeafarerEditForm(Seafarer seafarer, List<Rank> ranks, List<Vessel> vessels)
        {
            InitializeComponent();

            _seafarer = seafarer ?? new Seafarer();
            _ranks = new List<Rank>(ranks);
            _vessels = new List<Vessel>(vessels);

            Text = _seafarer.Id == 0 ? "Novi pomorac" : "Uređivanje pomorca - " + _seafarer.FullName;

            comboNationality.Properties.Items.AddRange(Nationalities);

            // gumb "+" u padajućim popisima za brzo dodavanje novog ranga / broda
            lookupRank.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Plus) { ToolTip = "Dodaj novi rang" });
            lookupVessel.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Plus) { ToolTip = "Dodaj novi brod" });

            BindRanks();
            BindVessels();
            LoadValues();
        }

        // ------------------------------------------------------------------
        // Punjenje kontrola
        // ------------------------------------------------------------------

        private void BindRanks()
        {
            LookupHelper.Configure(lookupRank, _ranks.OrderBy(r => r.Name).ToList());
        }

        private void BindVessels()
        {
            var items = new List<Vessel> { new Vessel { Id = NoVesselId, Name = "(nije ukrcan)" } };
            items.AddRange(_vessels.OrderBy(v => v.Name));
            LookupHelper.Configure(lookupVessel, items);
        }

        private void LoadValues()
        {
            _loading = true;
            try
            {
                textFirstName.Text = _seafarer.FirstName;
                textLastName.Text = _seafarer.LastName;
                dateOfBirth.EditValue = _seafarer.Id == 0 ? null : (object)_seafarer.DateOfBirth;
                comboNationality.Text = _seafarer.Nationality;
                textEmail.Text = _seafarer.Email;
                lookupRank.EditValue = _seafarer.Id == 0 ? null : (object)_seafarer.RankId;
                lookupVessel.EditValue = _seafarer.VesselId ?? NoVesselId;
                dateEmbarkation.EditValue = _seafarer.EmbarkationDate;
            }
            finally
            {
                _loading = false;
            }

            UpdateEmbarkationState(false);
        }

        private int? GetSelectedRankId()
        {
            return lookupRank.EditValue is int id && id > 0 ? id : (int?)null;
        }

        private int? GetSelectedVesselId()
        {
            return lookupVessel.EditValue is int id && id != NoVesselId ? id : (int?)null;
        }

        private static DateTime? GetDate(DateEdit edit)
        {
            return edit.EditValue is DateTime value ? value.Date : (DateTime?)null;
        }

        /// <summary>
        /// Datum ukrcaja moguće je unijeti samo kada je odabran brod.
        /// Kada korisnik sam odabere brod, a datum je prazan, predlaže se današnji datum.
        /// </summary>
        private void UpdateEmbarkationState(bool userChangedVessel)
        {
            bool hasVessel = GetSelectedVesselId().HasValue;
            dateEmbarkation.Enabled = hasVessel;

            if (!hasVessel)
            {
                dateEmbarkation.EditValue = null;
            }
            else if (userChangedVessel && GetDate(dateEmbarkation) == null)
            {
                dateEmbarkation.EditValue = DateTime.Today;
            }
        }

        private void lookupVessel_EditValueChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                UpdateEmbarkationState(true);
            }
        }

        // ------------------------------------------------------------------
        // Dodavanje novog ranga / broda gumbom "+"
        // ------------------------------------------------------------------

        private void lookupRank_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Plus)
            {
                return;
            }

            string name = AskForName("Naziv novog ranga:", "Novi rang");
            if (name == null)
            {
                return;
            }

            try
            {
                int id = LookupRepository.AddRank(name);
                _ranks = LookupRepository.GetRanks();
                BindRanks();
                lookupRank.EditValue = id;
                LookupsChanged = true;
            }
            catch (Exception ex)
            {
                ShowError("Dodavanje ranga nije uspjelo.", ex);
            }
        }

        private void lookupVessel_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Plus)
            {
                return;
            }

            string name = AskForName("Naziv novog broda:", "Novi brod");
            if (name == null)
            {
                return;
            }

            try
            {
                int id = LookupRepository.AddVessel(name);
                _vessels = LookupRepository.GetVessels();

                // ponovno punjenje popisa ne smije obrisati već upisani datum ukrcaja
                _loading = true;
                try
                {
                    BindVessels();
                    lookupVessel.EditValue = id;
                }
                finally
                {
                    _loading = false;
                }
                UpdateEmbarkationState(true);
                LookupsChanged = true;
            }
            catch (Exception ex)
            {
                ShowError("Dodavanje broda nije uspjelo.", ex);
            }
        }

        /// <summary>Otvara jednostavan dijalog za unos naziva; vraća null ako korisnik odustane ili ne upiše ništa.</summary>
        private static string AskForName(string prompt, string title)
        {
            string name = XtraInputBox.Show(prompt, title, string.Empty);
            if (name == null)
            {
                return null;
            }

            name = name.Trim();
            if (name.Length == 0)
            {
                return null;
            }

            if (name.Length > 100)
            {
                XtraMessageBox.Show("Naziv može imati najviše 100 znakova.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return name;
        }

        // ------------------------------------------------------------------
        // Spremanje
        // ------------------------------------------------------------------

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            _seafarer.FirstName = textFirstName.Text.Trim();
            _seafarer.LastName = textLastName.Text.Trim();
            _seafarer.DateOfBirth = GetDate(dateOfBirth).Value;
            _seafarer.Nationality = comboNationality.Text.Trim();
            _seafarer.Email = textEmail.Text.Trim().Length == 0 ? null : textEmail.Text.Trim();
            _seafarer.RankId = GetSelectedRankId().Value;
            _seafarer.VesselId = GetSelectedVesselId();
            _seafarer.EmbarkationDate = _seafarer.VesselId.HasValue ? GetDate(dateEmbarkation) : null;

            try
            {
                if (_seafarer.Id == 0)
                {
                    _seafarer.Id = SeafarerRepository.Insert(_seafarer);
                }
                else
                {
                    SeafarerRepository.Update(_seafarer);
                }
            }
            catch (Exception ex)
            {
                ShowError("Spremanje nije uspjelo.", ex);
                return;
            }

            SavedSeafarerId = _seafarer.Id;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>Provjerava unos; kraj svakog neispravnog polja prikazuje ikonu s porukom i vraća false.</summary>
        private bool ValidateInput()
        {
            errorProvider.ClearErrors();
            var errors = new List<KeyValuePair<Control, string>>();

            if (textFirstName.Text.Trim().Length == 0)
            {
                errors.Add(new KeyValuePair<Control, string>(textFirstName, "Ime je obavezno."));
            }

            if (textLastName.Text.Trim().Length == 0)
            {
                errors.Add(new KeyValuePair<Control, string>(textLastName, "Prezime je obavezno."));
            }

            DateTime today = DateTime.Today;
            DateTime? birthDate = GetDate(dateOfBirth);
            if (birthDate == null)
            {
                errors.Add(new KeyValuePair<Control, string>(dateOfBirth, "Datum rođenja je obavezan."));
            }
            else if (birthDate.Value > today)
            {
                errors.Add(new KeyValuePair<Control, string>(dateOfBirth, "Datum rođenja ne može biti u budućnosti."));
            }
            else if (GetAge(birthDate.Value, today) < MinimumAge)
            {
                errors.Add(new KeyValuePair<Control, string>(dateOfBirth, "Pomorac mora imati najmanje " + MinimumAge + " godina."));
            }

            if (comboNationality.Text.Trim().Length == 0)
            {
                errors.Add(new KeyValuePair<Control, string>(comboNationality, "Nacionalnost je obavezna."));
            }

            string email = textEmail.Text.Trim();
            if (email.Length > 0 && !EmailRegex.IsMatch(email))
            {
                errors.Add(new KeyValuePair<Control, string>(textEmail, "E-mail adresa nije ispravna (npr. ime.prezime@example.com)."));
            }

            if (GetSelectedRankId() == null)
            {
                errors.Add(new KeyValuePair<Control, string>(lookupRank, "Odaberite rang."));
            }

            if (GetSelectedVesselId().HasValue)
            {
                DateTime? embarkation = GetDate(dateEmbarkation);
                if (embarkation == null)
                {
                    errors.Add(new KeyValuePair<Control, string>(dateEmbarkation, "Unesite datum ukrcaja."));
                }
                else if (birthDate != null && embarkation.Value <= birthDate.Value)
                {
                    errors.Add(new KeyValuePair<Control, string>(dateEmbarkation, "Datum ukrcaja mora biti nakon datuma rođenja."));
                }
            }

            if (errors.Count == 0)
            {
                ShowMessage("* obavezna polja", false);
                return true;
            }

            foreach (KeyValuePair<Control, string> error in errors)
            {
                errorProvider.SetError(error.Key, error.Value);
            }

            ShowMessage(errors[0].Value, true);
            errors[0].Key.Focus();
            return false;
        }

        private static int GetAge(DateTime birthDate, DateTime today)
        {
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        private void ShowMessage(string text, bool isError)
        {
            labelMessage.Text = text;
            labelMessage.Appearance.ForeColor = isError ? Color.Firebrick : SystemColors.GrayText;
            labelMessage.Appearance.Options.UseForeColor = true;
        }

        private void ShowError(string message, Exception ex)
        {
            XtraMessageBox.Show(this, message + "\n\n" + ex.Message, "Crewman - greška",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
