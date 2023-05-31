using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Components.Registrations.Filters;
using PROWeb.Office.ViewModels.Registrations;

namespace PROWeb.Office.Components.Registrations
{
    public partial class Grid : PROListComponent<RegistrationFilterModel, ListRegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        protected override async Task<IList<ListRegistrationViewModel>> GetDataAsync(RegistrationFilterModel filter)
        {
            using (var service = _registrationServiceFactory.CreateService())
            {
                return await service.GetRegistrations
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
                    .ProjectToListAsync<ListRegistrationViewModel>(Mapper);
            }
        }
    }
}
