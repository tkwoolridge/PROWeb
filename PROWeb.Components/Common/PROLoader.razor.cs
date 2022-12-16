using Microsoft.AspNetCore.Components;

namespace PROWeb.Components.Common
{
    public partial class PROLoader : ComponentBase
    {
        [Parameter]
        public string BusyMessage { get; set; } = "Loading...";
    }
}
