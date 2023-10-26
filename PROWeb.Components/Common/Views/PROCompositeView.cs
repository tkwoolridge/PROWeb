using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Layouts;
using PROWeb.Components.Services.Undo;
using System.Diagnostics;
using Telerik.Blazor;

namespace PROWeb.Components.Common.Views
{
    public abstract class PROCompositeView<TViewModel> : PROComponent 
        where TViewModel : SlimViewModelBase
    {
        private ViewsLayout<TViewModel>? _layoutRef;

        [Inject]
        private IUndoService<TViewModel> _undoService { get; set; } = null!;

        [CascadingParameter]
        public DialogFactory Dialogs { get; set; } = null!;

        public bool CanSave { get; private set; }

        public bool CanUndo { get; private set; }

        protected TViewModel? Model => LayoutRef?.Model;

        protected ViewsLayout<TViewModel>? LayoutRef
        {
            get => _layoutRef;
            set
            {
                if (value != _layoutRef)
                {
                    _layoutRef = value;
                    OnLayoutUpdated();
                }
            }
        }

        protected virtual void OnLayoutUpdated()
        {
            Debug.Assert(LayoutRef != null);

            LayoutRef.ContextChanged -= OnContextChanged;
            LayoutRef.ContextChanged += OnContextChanged;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _undoService.Reset();
        }

        internal void OnContextChanged(object? sender, ContextChangedEventArgs e)
        {
            CanSave = true;
            CanUndo = true;
            
            StateHasChanged();
        }

        protected abstract Task SaveAsync(TViewModel model);

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

        public void OnUndo()
        {
            Debug.Assert(LayoutRef != null);

            if(_undoService.Next is not { } actions)
            {
                return;
            }

            foreach (var view in LayoutRef.ViewsList.OfType<PROEditableView<TViewModel>>())
            {
                view.Undo(actions);
            }

            CanUndo = _undoService.HasActions;
        }

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
