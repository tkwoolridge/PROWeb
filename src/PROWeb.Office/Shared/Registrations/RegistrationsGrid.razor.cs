using Mapster;
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
        [Parameter]
        public bool FormViewVisible { get; set; } = true;

        [Parameter]
        public bool FormViewCollapsed { get; set; } = true;

        [Parameter]
        public RegistrationViewModel? SelectedForm { get; set; }

        protected TelerikGrid<RegistrationViewModel>? GridRef { get; set; }

        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        private void OnSelectionChanged(RegistrationViewModel form)
        {
            SelectedForm = form.Adapt<RegistrationViewModel>();
            FormViewCollapsed = false;

            StateHasChanged();
        }

        private void OnFormSaved()
        {
            if (SelectedForm is { } current &&
                Data?.FirstOrDefault(m => m.VoterId == current.VoterId) is { } previous &&
                Data?.IndexOf(previous) is { } index && index > -1)
            {
                Data?.RemoveAt(index);
                Data?.Insert(index, current);

                GridRef?.Rebind();
            }
        }

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
