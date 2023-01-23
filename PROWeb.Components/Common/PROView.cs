using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;
using PROWeb.Components.Layouts;

namespace PROWeb.Components.Common
{
    public abstract class PROView<TViewModel> : PROComponent where TViewModel : class
    {
        private TViewModel? _model;

        [Parameter]
        public string? Title { get; set; }

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

        [CascadingParameter]
        public ViewsLayout<TViewModel>? Parent { get; set; }

        public EditContext? EditContext { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        protected virtual void OnModelUpdate()
        {
            if ((Editable || Parent?.Editable == true) && Model is { } model)
            {
                EditContext = GetEditContext();
            } 
        }
        
        protected virtual EditContext? GetEditContext()
        {
            return null;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Parent != null)
            {
                Parent.AddView(this);
            }
        }

        public virtual void OnSave()
        {
        }

        public IEnumerable<string>? OnValidate()
        {
            EditContext?.Validate();

            return EditContext?.GetValidationMessages();
        }
    }
}
