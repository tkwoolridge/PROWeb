using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Authentication.Models;
using PROWeb.Data.Authentication.Services.Users;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Services.Registrations;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Registrations.ViewModels;
using System.Diagnostics;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormViewBase : PROCompositeView<RegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = default!;

        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = default!;

        [Inject]
        private IUsersServiceFactory _userServiceFactory { get; set; } = default!;

        [Parameter]
        public EventCallback Save { get; set; }

        public bool CanApprove { get; private set; }

        public bool CanReject { get; private set; }

        protected PROUser? User { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetDefaultsAsync();

            await base.OnInitializedAsync();
        }

        public async Task OnApproveAsync()
        {
            Debug.Assert(Model != null);

            if (Model.VoterId is null)
            {
                Model.RegistrationStatusId = (int)RegistrationStatuses.Approved;
                await SaveAsync(Model);
            }

            await Task.CompletedTask;
        }

        public async Task OnRejectAsync()
        {
            await Task.CompletedTask;
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

            Registration? registration = model.Adapt<Registration>();

            if (registration == null) { return; }

            using (var service = _registrationServiceFactory.CreateService())
            {
                await service.AddRegistration(registration, User.UserName);
            }

            await Save.InvokeAsync();
        }

        private async Task SetDefaultsAsync()
        {
            using (var service = _userServiceFactory.CreateService())
            {
                User = await service.GetCurrentUser();

                Debug.Assert(Model != null);

                using (UndoService.SuspendUndo())
                {
                    if (Model.RegistrationId is null)
                    {
                        Model.CountryId = VoterConstants.BermudaCountryId;
                        // TODO: Get registration year from db.   
                        Model.RegistryYear = DateTime.Now.Year - 1;
                    }

                    Model.LastUpdated = DateTime.UtcNow;
                    Model.LastUpdatedBy = User?.UserName;
                }
            }
        }
    }
}
