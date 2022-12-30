using Microsoft.AspNetCore.Components;

namespace PROWeb.Common.Components
{
    public abstract class PROView<TViewModel> : PROComponent where TViewModel : class
    {
        [CascadingParameter]
        public PROViewContext<TViewModel>? ViewContext { get; set; }

        [Parameter]
        public string? Title { get; set; }

        protected TViewModel Context { get; set; } = default!;

        protected bool Editable { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Context = ViewContext?.Model as TViewModel ?? default!;
            Editable = ViewContext?.IsEditable == true;

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
