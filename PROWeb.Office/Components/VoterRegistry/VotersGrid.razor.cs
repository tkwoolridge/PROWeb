using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Components.VoterRegistry
{
    public partial class VotersGrid : PROListComponent<FilterModel, ListVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected override async Task<IList<ListVoterViewModel>> OnFilterAsync(FilterModel filter)
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
                    .ProjectToListAsync<ListVoterViewModel>(Mapper);
            }
        }
    }
}
