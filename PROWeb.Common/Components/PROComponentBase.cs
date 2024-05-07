using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components
{
    public class PROComponentBase : ComponentBase
    {
        [Parameter]
        public string? Class { get; set; }
    }
}
