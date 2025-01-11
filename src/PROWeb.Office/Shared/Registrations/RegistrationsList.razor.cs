using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Services.State;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Shared.Registrations.Filters;
using PROWeb.Office.Shared.Registrations.ViewModels;
using PROWeb.Office.Shared.Registrations.Views;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Registrations
{
    public partial class RegistrationsList : PROListComponent<RegistrationFilterModel, RegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        [Inject]
        protected IStateService<FormTabView, int> FormTabStateService { get; set; } = null!;

        protected TelerikListView<RegistrationViewModel>? ListRef { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            PageChanged += OnPageChanged;
        }

        private void OnPageChanged(object? sender, int e)
        {
            FormTabStateService.Clear();
        }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            if (args.Item is RegistrationViewModel current &&
                Data?.FirstOrDefault(m => m.VoterId == current.VoterId) is { } previous &&
                Data?.IndexOf(previous) is { } index && index > -1)
            {
                Data?.RemoveAt(index);
                Data?.Insert(index, current);

                ListRef?.Rebind();
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
    }
}
