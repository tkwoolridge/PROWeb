using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Layouts;
using PROWeb.Components.Services.Undo;
using System.Diagnostics;
using System.Reactive.Disposables;
using Telerik.Blazor;

namespace PROWeb.Components.Common.Views
{
    public abstract class PROCompositeView<TViewModel> : PROComponent
        where TViewModel : SlimViewModelBase
    {
        private ViewsLayout<TViewModel>? _layoutRef;

        [Inject]
        protected IUndoService<TViewModel> UndoService { get; set; } = null!;

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

            UndoService.Reset();
        }

        internal void OnContextChanged(object? sender, ContextChangedEventArgs e)
        {
            if (UndoService.HasActions)
            {
                SetCanSave(true);
                CanUndo = true;
            }

            StateHasChanged();
        }

        protected virtual void SetCanSave(bool canSave)
        {
            CanSave = canSave;
        }

        protected abstract Task SaveAsync(TViewModel model);

        protected async Task<bool> OnSaveAsync()
        {
            Debug.Assert(Model != null);

            if (!await ValidateModelAsync(true))
            {
                return false;
            }

            using (BusyScope("Saving..."))
            {
                await SaveAsync(Model);
            }

            UndoService.Reset();

            CanUndo = false;
            SetCanSave(false);

            return true;
        }

        protected IDisposable BusyScope(string message)
        {
            LayoutRef?.SetBusyState(true, message);

            return Disposable.Create(this, o =>
            {
                o.LayoutRef?.SetBusyState(false);
            });
        }

        public void OnUndo()
        {
            Debug.Assert(LayoutRef != null);

            if (UndoService.Next is not { } actions)
            {
                return;
            }

            foreach (var view in LayoutRef.ViewsList.OfType<PROEditableView<TViewModel>>())
            {
                view.Undo(actions);
            }

            CanUndo = UndoService.HasActions;
            SetCanSave(UndoService.HasActions);
        }

        public virtual async Task<bool> ValidateModelAsync(bool showMessage = false)
        {
            List<string> errors = new List<string>();

            Debug.Assert(LayoutRef != null);

            foreach (var view in LayoutRef.ViewsList.OfType<PROEditableView<TViewModel>>())
            {
                if (view.OnValidate() is { } viewErrors)
                {
                    errors.AddRange(viewErrors);
                }
            }

            var isValid = !errors.Any();

            if (!isValid && showMessage)
            {
                await Dialogs.AlertAsync(string.Join('\n', errors.Select((e, i) => $"{i + 1}. {e}").ToList()).TrimEnd('\n'), "Validation Error");
            }

            return isValid;
        }
    }
}
