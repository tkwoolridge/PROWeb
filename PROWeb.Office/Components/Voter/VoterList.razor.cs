using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Services.Voters;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Components.Voter
{
    public partial class VoterList : PROListComponent<VoterFilterViewModel, TabedVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected int Page { get; set; }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            var item = args.Item as VoterViewModel;
        }

        protected override async Task<IList<TabedVoterViewModel>> OnFilterAsync(VoterFilterViewModel filter)
        {
            Page = 1;

            using (var service = _voterServiceFactory.CreateService())
            {
                return await service.GetVoters
                    (
                    filter.RegistryYear,
                    null,
                    filter.FirstName,
                    filter.LastName,
                    filter.MiddleName,
                    filter.MaidenName,
                    filter.IsEligible,
                    filter.DateOfBirth,
                    filter.AgeFrom,
                    filter.AgeTo,
                    filter.Phone,
                    filter.AssessmentNo,
                    filter.StreetName,
                    filter.HouseNo,
                    filter.ConstituencyNo,
                    filter.ParishNo,
                    filter.PostalCode)
                    .ProjectToListAsync<TabedVoterViewModel>();
            }
        }
    }
}
