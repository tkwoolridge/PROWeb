using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using Telerik.Blazor;

namespace PROWeb.Components.Common.Layouts
{
    public partial class ViewsLayout<TViewModel> : PROContentLayout where TViewModel : SlimViewModelBase
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; } = null!;

        [CascadingParameter]
        public TViewModel? Model { get; set; }

        [Parameter]
        public RenderFragment<ViewsLayout<TViewModel>>? Header { get; set; }

        [Parameter]
        public RenderFragment<TViewModel>? Views { get; set; }

        [Parameter]
        public RenderFragment<ViewsLayout<TViewModel>>? Footer { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        public event EventHandler<ContextChangedEventArgs>? ContextChanged;

        internal IList<PROView<TViewModel>> ViewsList { get; set; } = new List<PROView<TViewModel>>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        internal void OnContextChanged(object? sender, ContextChangedEventArgs e)
        {
            ContextChanged?.Invoke(sender, e);
        }

        internal void AddView(PROView<TViewModel> view)
        {
            ViewsList.Add(view);
        }
    }
}
