using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Registrations.Views
{
    public class FormFlagsView : FlagsView<RegistrationViewModel, VoterFlagViewModel>
    {
        public FormFlagsView() : base(
            null,
            v => v.CommonwealthCitizen,
            v => v.BermudianStatusGranted,
            v => v.RegisteredAsElector,
            v => v.IsBermudianStatusGranted,
            v => v.WasBornIn,
            v => v.CountryId,
            f => f.FlagId,
            f => f.FlagDescription)
        {
        }
    }
}
