using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Services.CachedData;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Shared.Registrations.ViewModels;
using System.Diagnostics;
using PROWeb.Components.Voters;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormViewBase : VoterViewBase<RegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = default!;

        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = default!;

        [Parameter]
        public EventCallback Save { get; set; }

        public bool CanApprove { get; private set; }

        public bool CanReject { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            SetDefaultsAsync();
        }

        public async Task OnApproveAsync()
        {
            Debug.Assert(Model != null);

            if (!await ValidateModelAsync(true))
            {
                return;
            }

            var voter = Model.Adapt<VoterViewModel>();

            using (BusyScope("Approving form..."))
            {
                await SaveAsync(Model);

                if (voter.VoterId is not null)
                {
                    await UpdateVoterAsync(voter);
                }
                else
                {
                    await AddVoterAsync(voter);
                }

                Model.RegistrationStatusId = (int)RegistrationStatuses.Approved;
            }
        }

        public async Task OnRejectAsync()
        {
            Debug.Assert(Model != null);

            if (!await ValidateModelAsync(true))
            {
                return;
            }

            using (BusyScope("Rejecting form..."))
            {
                Model.RegistrationStatusId = (int)RegistrationStatuses.Rejected;
                await SaveAsync(Model);
            }
        }

        protected override void SetCanSave(bool canSave)
        {
            base.SetCanSave(canSave);

            CanApprove = canSave;
            CanReject = canSave;
        }

        protected override async Task SaveAsync(RegistrationViewModel model)
        {
            Debug.Assert(User?.UserName != null);

            model.LastUpdated = DateTime.UtcNow;
            model.LastUpdatedBy = User.UserName;

            Registration? registration = model.Adapt<Registration>();

            if (registration == null) { return; }

            using (var service = _registrationServiceFactory.CreateService())
            {
                if (registration.RegistrationId == 0)
                {
                    await service.AddRegistration(registration);
                }
                else
                {
                    await service.UpdateRegistration(registration);
                }
            }

            await Save.InvokeAsync();
        }

        private void SetDefaultsAsync()
        {
            Debug.Assert(Model != null);

            using (UndoService.SuspendUndo())
            {
                if (Model.RegistrationId is null)
                {
                    Model.LastUpdated = DateTime.UtcNow;
                    Model.LastUpdatedBy  = User?.UserName;
                    Model.CountryId = VoterConstants.BermudaCountryId;
                    Model.RegistryYear = _cachedDataService.RegistrationYear;
                }
            }
        }
    }
}
