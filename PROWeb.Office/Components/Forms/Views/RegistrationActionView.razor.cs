using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.Extensions;
using PROWeb.Data.Services.Registration;
using PROWeb.Office.ViewModels.Registration;

namespace PROWeb.Office.Components.Forms.Views
{
    public readonly struct RegistrationAction
    {
        public int ActionId { get; }
        public string Action { get; }

        public RegistrationAction(int actionId, string action) : this()
        {
            ActionId = actionId;
            Action = action;
        }
    }


    public partial class RegistrationActionView : PROView<RegistrationWizardViewModel>
    {
        public IList<RegistrationAction>? RegistrationActions { get; set; }


        protected override void OnInitialized()
        {
            RegistrationActions = new List<RegistrationAction>()
            {
                new RegistrationAction(1, "Adress Change"),
                new RegistrationAction(2, "Name Change"),
            };
        }
    }
}
