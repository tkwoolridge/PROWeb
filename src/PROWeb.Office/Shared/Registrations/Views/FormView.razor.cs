using Microsoft.AspNetCore.Components;
using PROWeb.Data.Models.Enums;
using PROWeb.Office.Registrations.Views;
using System.Diagnostics;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormView : FormViewBase
    {
        [Parameter]
        public RenderFragment? FormFooter { get; set; }

        public string? FormDescription { get; set; }

        public string? FormName { get; set; }

        protected FormOldAddressView? OldAddressViewRef { get; set; }

        protected FormAddressView? AddressViewRef { get; set; }

        protected FormContactInfoView? ContactInfoViewRef { get; set; }

        protected FormFlagsView? FormFlagsViewRef { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnLayoutUpdated()
        {
            base.OnLayoutUpdated();

            Debug.Assert(Model != null);

            switch ((FormTypes)Model.FormTypeId)
            {
                case FormTypes.Form1:
                    Class = "form1-background";
                    FormName = "FORM 1";
                    FormDescription = "NEW REGITRATION";
                    break;
                case FormTypes.Form2:
                    Class = "form2-background";
                    FormName = "FORM 2";
                    FormDescription = "CHANGE REGITRATION DETAILS";
                    break;
                // Add more cases as needed for other FormTypes
                default:
                    Class = "form2-background";
                    // Assign default properties here
                    break;
            }
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
