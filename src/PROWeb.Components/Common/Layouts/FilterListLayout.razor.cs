using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Components.Common.Layouts
{
    public partial class FilterListLayout : PROContentLayout
    {
        [Parameter]
        public RenderFragment? Filter { get; set; }

        [Parameter]
        public RenderFragment? Toolbox { get; set; }

        [Parameter]
        public RenderFragment? List { get; set; }

        [Parameter]
        public RenderFragment? Footer { get; set; }
    }
}
