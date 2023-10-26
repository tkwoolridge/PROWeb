using Microsoft.AspNetCore.Components;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormView : FormViewBase
    {
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
    }
}
