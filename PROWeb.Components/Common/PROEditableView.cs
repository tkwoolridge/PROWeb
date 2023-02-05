using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Layouts;

namespace PROWeb.Components.Common
{
    public abstract class PROEditableView<TViewModel> : PROView<TViewModel> where TViewModel : SlimViewModelBase
    {
        public EditContext? EditContext { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();
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
