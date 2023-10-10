using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.DependencyInjection;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Shared.Registrations.Filters;
using PROWeb.Office.Shared.Registrations.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Registrations
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
                    .ProjectToListAsync<ListRegistrationViewModel>();
            }
        }

        protected void OnRowRenderHandler(GridRowRenderEventArgs row)
        {
            ListRegistrationViewModel? registration = row.Item as ListRegistrationViewModel;

            row.Class = registration?.FormTypeId == 1 ? "form1-row" : "form2-row";
        }
    }
}
