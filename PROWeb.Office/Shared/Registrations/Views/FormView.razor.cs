using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormView : PROCompositView<RegistrationViewModel>
    {
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

        protected override Task SaveAsync(RegistrationViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
