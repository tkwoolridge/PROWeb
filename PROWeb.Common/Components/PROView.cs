using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components
{
    public abstract class PROView<TViewModel> : PROComponent where TViewModel : class
    {
        private PROViewContext<TViewModel>? _viewContext;

        [CascadingParameter]
        public PROViewContext<TViewModel>? ViewContext 
        { 
            get => _viewContext; 
            set
            {
                if (_viewContext != value)
                {
                    _viewContext = value;
                    OnViewContextUpdate();
                }
            }       
        }

        protected virtual void OnViewContextUpdate()
        {
            Context = ViewContext?.Model as TViewModel ?? default!;
            Editable = ViewContext?.IsEditable == true;
        }

        [Parameter]
        public string? Title { get; set; }

        protected TViewModel Context { get; set; } = default!;

        protected bool Editable { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ViewContext != null)
            {
                ViewContext.AddView(this);
            }
        }

        public virtual void OnSave()
        {
        }

        public List<string>? OnValidate()
        {
            return null;
        }
    }
}
