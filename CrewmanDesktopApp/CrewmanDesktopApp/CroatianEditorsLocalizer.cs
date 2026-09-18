using DevExpress.XtraEditors.Localization;

namespace CrewmanDesktopApp
{
    /// <summary>
    /// Hrvatski tekstovi za ugrađene DevExpress elemente (gumbi u XtraMessageBox-u, kalendar u DateEdit-u).
    /// Sve ostalo ostaje na engleskom (DevExpress nema službeni hrvatski prijevod).
    /// </summary>
    public class CroatianEditorsLocalizer : Localizer
    {
        public static void Activate()
        {
            Localizer.Active = new CroatianEditorsLocalizer();
        }

        public override string GetLocalizedString(StringId id)
        {
            switch (id)
            {
                case StringId.XtraMessageBoxOkButtonText: return "U redu";
                case StringId.XtraMessageBoxCancelButtonText: return "Odustani";
                case StringId.XtraMessageBoxYesButtonText: return "Da";
                case StringId.XtraMessageBoxNoButtonText: return "Ne";
                case StringId.DateEditToday: return "Danas";
                case StringId.DateEditClear: return "Očisti";
                default: return base.GetLocalizedString(id);
            }
        }
    }
}
