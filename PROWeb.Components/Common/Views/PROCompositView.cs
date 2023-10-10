using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Layouts;
using PROWeb.Components.Services.Undo;
using System.Diagnostics;
using Telerik.Blazor;

namespace PROWeb.Components.Common.Views
{
    public abstract class PROCompositView<TViewModel> : PROComponent 
        where TViewModel : SlimViewModelBase
    {
        [Inject]
        private IUndoService<TViewModel> _undoService { get; set; } = null!;

        [CascadingParameter]
        public DialogFactory Dialogs { get; set; } = null!;

        public bool CanSave { get; private set; }

        protected ViewsLayout<TViewModel>? LayoutRef { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Debug.Assert(LayoutRef != null);

            if (firstRender)
            {
                LayoutRef.FieldValueChanged -= OnFieldChanged;
                LayoutRef.FieldValueChanged += OnFieldChanged;
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        internal void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            CanSave = true;

            if(sender is EditContext eContext && eContext.Model is ViewModelContext<TViewModel> context)
            {
                var value = context.GetValue(e.FieldIdentifier.FieldName);
            }
            
            StateHasChanged();
        }

        protected async Task<bool> OnSaveAsync()
        {
            if (Validate() is { } errors)
            {
                await Dialogs.AlertAsync(string.Join('\n', errors.Select((e, i) => $"{i + 1}. {e}").ToList()).TrimEnd('\n'), "Validation Error");

                return false;
            }
            Debug.Assert(LayoutRef != null);

            foreach (var view in LayoutRef.ViewsList.OfType<PROEditableView<TViewModel>>())
            {
                view.OnSave();
            }

            LayoutRef.SetBusyState(true, "Saving...");

            Debug.Assert(LayoutRef?.Model != null);

            await SaveAsync(LayoutRef.Model);

            LayoutRef.SetBusyState(false);

            return true;
        }

        protected abstract Task SaveAsync(TViewModel model);

        public virtual List<string>? Validate()
        {
            List<string>? errors = new List<string>();

            Debug.Assert(LayoutRef != null);

            foreach (var view in LayoutRef.ViewsList.OfType<PROEditableView<TViewModel>>())
            {
                if (view.OnValidate() is { } viewErrors)
                {
                    errors?.AddRange(viewErrors);
                }
            }

            return errors.Any() ? errors : null;
        }
    }
}
