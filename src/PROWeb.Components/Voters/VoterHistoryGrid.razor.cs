using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Common;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Components.Voters
{
    public partial class VoterHistoryGrid : PROGridComponent<VoterHistoryViewModel, VoterHistoryFieldViewModel>
    {
        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        protected override async Task<IList<VoterHistoryFieldViewModel>> GetDataAsync(VoterHistoryViewModel filter)
        {
            using (var service = _votersServiceFactory.CreateService())
            {
                var histories = await service.GetVoterHistoryFields
                    (
                        filter.RegistryYear,
                        filter.VoterId,
                        filter.Created
                    ).ProjectToListAsync<VoterHistoryFieldViewModel>();

                return histories;
            }
        }
    }
}
