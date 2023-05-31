using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Office.Components.Forms.Views
{
    public partial class FormView : PROComponent
    {
        [Parameter]
        public EventHandler? Approve { get; set; }

        [Parameter]
        public EventHandler? Reject { get; set; }

        [Parameter]
        public EventHandler? Undo { get; set; }

        [Parameter]
        public EventHandler? Save { get; set; }

        [Parameter]
        public RenderFragment? Form { get; set; }

        [Parameter]
        public string? FormDescription { get; set; }

        [Parameter]
        public string? FormName { get; set; }

        public void OnApprove()
        {
            Approve?.Invoke(this, EventArgs.Empty);
        }

        public void OnReject()
        {
            Reject?.Invoke(this, EventArgs.Empty);
        }

        public void OnUndo()
        {
            Undo?.Invoke(this, EventArgs.Empty);
        }

        public void OnSave()
        {
            Save?.Invoke(this, EventArgs.Empty);
        }
    }
}
