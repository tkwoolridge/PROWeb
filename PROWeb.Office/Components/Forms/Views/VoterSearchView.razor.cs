using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Office.Components.Forms.ViewModels;

namespace PROWeb.Office.Components.Forms.Views
{
    public partial class VoterSearchView : PROView<FormWizardViewModel>
    {
        [Parameter]
        public EventHandler<VoterViewModel>? VoterSelected { get; set; }

        private void OnVoterSelected(VoterViewModel voter)
        {
            VoterSelected?.Invoke(this, voter);
        }
    }
}
