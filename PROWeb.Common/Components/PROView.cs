using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PROWeb.Common.Components
{
    public abstract class PROView<TViewModel> : PROComponent where TViewModel : class
    {
        [CascadingParameter]
        public EditContext? EditContext { get; set; }

        [CascadingParameter]
        public TViewModel Context { get; set; } = default!;

        protected bool Editable { get; set; }

        protected bool CanSave { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Context ??= EditContext?.Model as TViewModel ?? default!;

            if (EditContext != null)
            {
                EditContext.OnFieldChanged -= OnFieldChanged;
                EditContext.OnFieldChanged += OnFieldChanged;
                Editable = true;
            }
        }

        private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            bool isEditValid = IsEditValid();

            if (isEditValid != CanSave)
            {
                CanSave = isEditValid;
                StateHasChanged();
            }
        }

        protected bool IsEditValid()
        {
            return EditContext?.Validate() == true;
        }
    }
}
