using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterFlagsView : FlagsView<VoterViewModel, VoterFlagViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        public VoterFlagsView() : base(
            v => v.Flags,
            v => v.CommonwealthCitizen,
            v => v.BermudianStatusGranted,
            v => v.RegisteredAsElector,
            v => v.IsBermudianStatusGranted,
            f => f.FlagId,
            f => f.FlagDescription)
        {
        }


        protected override async Task<IList<VoterFlagViewModel>?> GetFlagsAsync()
        {
            using (var service = _voterServiceFactory.CreateService())
            {
                return await service.GetVoterFlags().ProjectToListAsync<VoterFlagViewModel>();
            }
        }

        public override void OnSave()
        {
            base.OnSave();
        }
    }
}
