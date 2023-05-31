using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.Registry
{
    public partial class Grid : PROListComponent<FilterModel, ListVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        protected override async Task<IList<ListVoterViewModel>> GetDataAsync(FilterModel filter)
        {
            using (var service = _votersServiceFactory.CreateService())
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
