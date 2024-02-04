using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;

namespace PROWeb.Components.Common
{
    public partial class ExpansionPanel : PROComponentBase
    {
        const string _arrowDown = "k-i-arrow-chevron-down";
        const string _arrowUp = "k-i-arrow-chevron-up";

        [Parameter]
        public RenderFragment? Content { get; set; }

        [Parameter]
        public RenderFragment? Header { get; set; }

        [Parameter]
        public bool IsExpanded { get; set; } = true;

        [Parameter]
        public bool IsExpanderVisible { get; set; } = true;

        [Parameter]
        public RenderFragment? @ToolBar { get; set; }

        protected string? HideContentClass { get; set; }

        protected string? ArrowClass { get; set; } = _arrowDown;

        public void OnExpand()
        {
            IsExpanded = !IsExpanded;
            UpdateExpandState();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UpdateExpandState();
        }

        private void UpdateExpandState() 
        {
            HideContentClass = IsExpanded ? null : "hide-content";
            ArrowClass = IsExpanded ? _arrowDown : _arrowUp;
        }
    }
}
