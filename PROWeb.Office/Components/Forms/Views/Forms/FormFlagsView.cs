using PROWeb.Components.Person;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Office.ViewModels.Forms;

namespace PROWeb.Office.Components.Forms.Views.Forms
{
    public class FormFlagsView : FlagsView<RegistrationViewModel, VoterFlagViewModel>
    {
        protected override Task<IList<VoterFlagViewModel>?> GetFlagsAsync()
        {
           return Task.FromResult<IList<VoterFlagViewModel>?>(null);
        }

        public FormFlagsView() : base(
            v => v.Flags,
            v => v.CommonwealthCitizen,
            v => v.BermudianStatusGranted,
            v => v.RegisteredAsElector,
            v => v.IsBermudianStatusGranted,
            f => f.FlagId,
            f => f.FlagDescription)
        {
        }
    }
}
