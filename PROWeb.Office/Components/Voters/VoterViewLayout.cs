using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Layouts;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Office.Voters
{
    public class VoterViewLayout : ViewsLayout<ListVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        public override async Task SaveAsync()
        {
            Voter voter = Model.MapTo<Voter>(Mapper);

            using (var service = _voterServiceFactory.CreateService())
            {
                await service.UpdateVoter(voter, "pro1");
            }
        }
    }
}
