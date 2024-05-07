using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Components.Common.Layouts
{
    public partial class DialogLayout : PROComponent
    {
        [Parameter]
        public bool ShowButtons { get; set; } = true;

        [Parameter]
        public RenderFragment? DialogContent { get; set; }

        [Parameter]
        public EventCallback Confirm { get; set; }

        [Parameter]
        public EventCallback Cancel { get; set; }

        [Parameter]
        public string Width { get; set; } = "600px";

        [Parameter]
        public string Height { get; set; } = "800px";

        [Parameter]
        public string Title { get; set; } = "Dialog Window";

        [Parameter]
        public string ConfirmCaption { get; set; } = "Confirm";

        [Parameter]
        public string CancelCaption { get; set; } = "Cancel";

        public bool ShowDialog { get; set; }

        public bool CanConfirm { get; set; } = true;

        protected async Task OnConfirm()
        {
            ShowDialog = false;

            await Confirm.InvokeAsync();
        }

        public void Show()
        {
            ShowDialog = true;

            StateHasChanged();
        }

        protected async Task OnCancel()
        {
            ShowDialog = false;

            await Cancel.InvokeAsync();
        }
    }
}
