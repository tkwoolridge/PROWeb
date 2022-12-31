using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;

namespace PROWeb.Components.Layouts
{
    public abstract partial class ViewsLayout<TViewModel> : PROContentLayout where TViewModel : class
    {
        private TViewModel? _model;
        private EditContext? _editContext;

        [CascadingParameter]
        public EditContext? EditContext 
        { 
            get => _editContext; 
            set
            {
                if(_editContext != value)
                {
                    _editContext = value;
                    OnEditContextUpdate();
                }
            }
        }

        [CascadingParameter]
        public TViewModel? Model 
        { 
            get => _model;
            set 
            {
                if(_model != value)
                {
                    _model = value;
                    OnModelUpdate();
                }
            }
        }

        [Parameter]
        public PROViewContext<TViewModel>? ViewContext { get; set; }

        [Parameter]
        public RenderFragment? Views { get; set; }

        public bool Editable { get; set; }

        public bool CanSave { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
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

        private void OnEditContextUpdate()
        {
            if (EditContext is { } context && context.Model is TViewModel model)
            {
                Model = EditContext.Model as TViewModel;
                ViewContext = new PROViewContext<TViewModel>(model, context, true);

                EditContext.OnFieldChanged -= OnFieldChanged;
                EditContext.OnFieldChanged += OnFieldChanged;
                Editable = true;
            }
        }

        private void OnModelUpdate()
        {
            if (Model != null)
            {
                ViewContext = new PROViewContext<TViewModel>(Model);
            }
        }
    }
}
