using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
