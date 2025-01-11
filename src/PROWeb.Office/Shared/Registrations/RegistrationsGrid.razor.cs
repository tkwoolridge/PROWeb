using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.Extensions;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Shared.Registrations.Filters;
using PROWeb.Office.Shared.Registrations.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Registrations
{
    public partial class RegistrationsGrid : PROListComponent<RegistrationFilterModel, RegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        protected override async Task<IList<RegistrationViewModel>> GetDataAsync(RegistrationFilterModel filter)
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
                    .ProjectToListAsync<RegistrationViewModel>();
            }
        }

        protected void OnRowRenderHandler(GridRowRenderEventArgs row)
        {
            RegistrationViewModel? registration = row.Item as RegistrationViewModel;

            row.Class = registration?.RegistrationStatusId switch
            {
                (int)RegistrationStatuses.Approved => "form-row-approved",
                _ => registration?.FormTypeId == 1 ? "form1-row" : "form2-row"
            };
        }
    }
}
