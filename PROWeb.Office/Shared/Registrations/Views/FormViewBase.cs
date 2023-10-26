using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Services.Registrations;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public class FormViewBase : PROCompositeView<RegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task OnApproveAsync()
        {
            if (Model?.VoterId == 0)
            {
                Model.RegistrationStatusId = (int)RegistrationStatuses.Approved;
                await SaveAsync(Model);
            }

            await Task.CompletedTask;
        }

        public async Task AddVoter(RegistrationViewModel model)
        {
            Voter? voter = model.Adapt<Voter>();

            if (voter == null) { return; }

            using (var service = _votersServiceFactory.CreateService())
            {
                await service.AddVoter(voter, "pro1");
            }
        }

        public async Task OnRejectAsync()
        {
            await Task.CompletedTask;
        }

        protected override async Task SaveAsync(RegistrationViewModel model)
        {
            Registration? registration = model.Adapt<Registration>();

            if (registration == null) { return; }

            using (var service = _registrationServiceFactory.CreateService())
            {
                await service.AddRegistration(registration, "pro1");
            }
        }
    }
}
