using Microsoft.AspNetCore.Components;
using PROWeb.Data.Models.Enums;
using System.Diagnostics;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormView : FormViewBase
    {
        [Parameter]
        public RenderFragment? Form { get; set; }

        [Parameter]
        public RenderFragment? FormFooter { get; set; }

        [Parameter]
        public string? FormDescription { get; set; }

        [Parameter]
        public string? FormName { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnLayoutUpdated()
        {
            base.OnLayoutUpdated();

            Debug.Assert(Model != null);

            Class = (FormTypes)Model.FormTypeId switch
            {
                FormTypes.Form1 => "form1-background",
                _ => "form2-background"
            };
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
