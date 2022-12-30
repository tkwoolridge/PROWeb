using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components
{
    public class PROContentLayout : ComponentBase
    {
        protected bool IsBusy { get; set; }

        protected string? BusyMessage { get; set; }

        public EventHandler<bool>? BusyStateChanged { get; set; }

        public void SetBusyState(bool state, string message = "Loading...")
        {
            IsBusy = state;
            BusyMessage = message;
            BusyStateChanged?.Invoke(this, state);

            StateHasChanged();
        }
    }
}
