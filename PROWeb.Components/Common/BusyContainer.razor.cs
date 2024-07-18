using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Components.Common
{
    public partial class BusyContainer : PROComponent
    {
        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public string? BusyMessage { get; set; }

        [Parameter]
        public RenderFragment? Header { get; set; }

        [Parameter]
        public RenderFragment? Content { get; set; }

        [Parameter]
        public RenderFragment? Footer { get; set; }

        public async Task SetBusyStateAsync(bool state, string message = "Exporting. Please wait...")
        {
            await Task.Delay(100);

            IsBusy = state;
            BusyMessage = message;

            StateHasChanged();
        }
    }
}
