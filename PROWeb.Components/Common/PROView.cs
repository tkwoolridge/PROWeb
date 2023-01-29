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
        public ViewsLayout<TViewModel>? ParentLayout { get; set; }

        public EditContext? EditContext { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        protected virtual void OnModelUpdate()
        {
            PrepareContext();
        }

        private void OnEditContextFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            ParentLayout?.OnFieldChanged(sender, e);
        }

        protected virtual EditContext? GetEditContext()
        {
            return null;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ParentLayout is { } layout)
            {
                layout.AddView(this);
                Editable = layout.Editable;
            }
        }

        private void PrepareContext()
        {
            if (EditContext is { } oldContext)
            {
                oldContext.OnFieldChanged -= OnEditContextFieldChanged;
            }

            EditContext = GetEditContext();

            if (EditContext is { } context)
            {
                context.OnFieldChanged += OnEditContextFieldChanged;
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
