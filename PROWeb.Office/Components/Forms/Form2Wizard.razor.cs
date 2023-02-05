using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Data.Models.Enums;
using PROWeb.Office.Components.Forms.Views;
using PROWeb.Office.ViewModels.Registration;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Components.Forms
{
    public partial class Form2Wizard : PROComponent
    {
        protected int Page { get; set; }

        public string? SecundStepLabel { get; set; }

        protected RenderFragment? SecundStepView { get; set; }

        protected RegistrationWizardViewModel WizardModel { get; set; } = 
            new RegistrationWizardViewModel()
            {
                RegistrationActionId = 1
            };

        protected override void OnInitialized()
        {
            WizardModel.SubscribeFast(nameof(WizardModel.RegistrationActionId), OnActionChanged);

            base.OnInitialized();
        }

        private void OnActionChanged()
        {
            SecundStepLabel = WizardModel.RegistrationActionId switch
            {
                1 => "Address Chanage",
                _ => "Name Chanage"
            };

            SecundStepView = WizardModel.RegistrationActionId switch
            {
                1 => AddressTemplate,
                _ => NameChangeTemplate
            };

            StateHasChanged();
        }

        private void OnWizardFinish()
        {
        }

        public void OnRegistrationStepChange(WizardStepChangeEventArgs args)
        {
        }
    }
}
