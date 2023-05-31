using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Office.Components.Forms.ViewModels;
using PROWeb.Office.ViewModels.EligiblePolls;

namespace PROWeb.Office.Components.Forms.Views
{
    public partial class EligibleSearchView : PROView<FormWizardViewModel>
    {
        [Parameter]
        public EventHandler<EligibleViewModel>? EligibleSelected { get; set; }

        private void OnEligibleSelected(EligibleViewModel eligible)
        {
            EligibleSelected?.Invoke(this, eligible);
        }
    }
}