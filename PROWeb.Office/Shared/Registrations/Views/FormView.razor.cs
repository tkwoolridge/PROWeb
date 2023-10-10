using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormView : PROCompositView<RegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        [Parameter]
        public RegistrationViewModel? Model { get; set; } = new RegistrationViewModel();

        [Parameter]
        public RenderFragment? Form { get; set; }

        [Parameter]
        public string? FormDescription { get; set; }

        [Parameter]
        public string? FormName { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        public void OnUndo()
        {
        }

        public async Task OnApproveAsync()
        {
            await Task.CompletedTask;
        }

        public async Task OnRejectAsync()
        {
            await Task.CompletedTask;
        }

        protected override async Task SaveAsync(RegistrationViewModel model)
        {
            Registration? registration = LayoutRef?.Model?.Adapt<Registration>();

            if (registration == null) { return; }

            using (var service = _registrationServiceFactory.CreateService())
            {
                await service.UpdateRegistration(registration, "pro1");
            }
        }
    }
}
