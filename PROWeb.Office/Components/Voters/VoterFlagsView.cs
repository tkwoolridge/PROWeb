#nullable enable

using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Office.Voters
{
    public class VoterFlagsView : PersonFlagsView<ListVoterViewModel, VoterFlagViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;


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
