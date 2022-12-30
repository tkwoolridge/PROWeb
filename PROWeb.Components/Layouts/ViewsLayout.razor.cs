using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;

namespace PROWeb.Components.Layouts
{
    public abstract partial class ViewsLayout<TViewModel> : PROContentLayout where TViewModel : class
    {
        [CascadingParameter]
        public EditContext? EditContext { get; set; }

        [CascadingParameter]
        public TViewModel? Model { get; set; }

        [Parameter]
        public PROViewContext<TViewModel>? ViewContext { get; set; }

        [Parameter]
        public RenderFragment? Views { get; set; }

        public bool Editable { get; set; }

        public bool CanSave { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Model != null)
            {
                ViewContext = new PROViewContext<TViewModel>(Model);
            }
            else if (EditContext is { } context && context.Model is TViewModel model)
            {
                Model = EditContext.Model as TViewModel;
                ViewContext = new PROViewContext<TViewModel>(model, context, true);

                EditContext.OnFieldChanged -= OnFieldChanged;
                EditContext.OnFieldChanged += OnFieldChanged;
                Editable = true;
            }
        }

        private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            CanSave = true;
            StateHasChanged();
        }

        public virtual bool OnSave()
        {
            if (EditContext == null || ViewContext == null || !Validate())
            {
                return false;
            }

            foreach (var view in ViewContext.Views)
            {
                view.OnSave();
            }

            Save();

            return true;
        }

        public abstract void Save();

        public bool Validate()
        {
            if (EditContext == null || ViewContext == null)
            {
                return false;
            }

            EditContext.Validate();

            List<string>? errors = EditContext.GetValidationMessages().ToList();

            foreach (var view in ViewContext.Views)
            {
                if (view.OnValidate() is { } viewErrors)
                {
                    errors ??= viewErrors;

                    errors?.AddRange(viewErrors);
                }
            }

            return errors != null;
        }
    }
}
