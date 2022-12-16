using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.Common
{
    public partial class ExpansionPanel : ComponentBase
    {
        const string ArrowDown = "k-i-arrow-chevron-down";
        const string ArrowUp = "k-i-arrow-chevron-up";

        [Parameter]
        public RenderFragment? Content { get; set; }

        [Parameter]
        public RenderFragment? Header { get; set; }

        [Parameter]
        public RenderFragment? @ToolBar { get; set; }

        protected string? HideContentClass { get; set; }

        protected string? ArrowClass { get; set; } = ArrowDown;

        public void OnExpand()
        {
            HideContentClass = string.IsNullOrEmpty(HideContentClass) ? "hide-content" : null;  
            ArrowClass = ArrowClass == ArrowDown ? ArrowUp: ArrowDown;
        }
    }
}
