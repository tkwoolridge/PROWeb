#nullable enable

using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Office.Voters
{
    public class VoterFlagsView : FlagsView<ListVoterViewModel, VoterFlagViewModel>
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
                return await service.GetVoterFlags().ProjectToListAsync<VoterFlagViewModel>(Mapper);
            }
        }

        public override void OnSave()
        {
            base.OnSave();
        }
    }
}
