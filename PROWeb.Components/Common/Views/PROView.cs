using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Layouts;

namespace PROWeb.Components.Common.Views
{
    public abstract class PROView<TViewModel> : PROComponent where TViewModel : SlimViewModelBase
    {
        private TViewModel? _model;

        [Parameter]
        public string? Title { get; set; }

        [CascadingParameter]
        public ViewsLayout<TViewModel>? ParentLayout { get; set; }

        [CascadingParameter]
        public TViewModel? Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnModelUpdate();
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ParentLayout is { } layout)
            {
                layout.AddView(this);
            }
        }

        protected virtual void OnModelUpdate()
        {
        }
    }
}
