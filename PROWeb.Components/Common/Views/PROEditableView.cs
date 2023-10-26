using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Services.Undo;
using System.Reactive.Disposables;

namespace PROWeb.Components.Common.Views
{
    public abstract class PROEditableView<TViewModel> : PROView<TViewModel> where TViewModel : SlimViewModelBase
    {
        public EditContext? EditContext { get; set; }

        [Inject]
        protected IUndoService<TViewModel> UndoService { get; set; } = null!;

        [Parameter]
        public bool Editable { get; set; }

        public ViewModelContext<TViewModel>? Context => EditContext?.Model as ViewModelContext<TViewModel>;

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();
            PrepareContext();
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
            if (Context is { } oldContext)
            {
                oldContext.Changing -= OnContextChanging;
                oldContext.Changed -= OnContextChanged;
            }

            EditContext = GetEditContext();

            if (Context is { } context)
            {
                context.Changing += OnContextChanging;
                context.Changed += OnContextChanged;
            }
        }

        private void OnContextChanging(object? sender, ContextChangingEventArgs e)
        {
            var action = new UndoAction(e.PropertyName, e.OldPropertyValue);

            UndoService.AddAction(action);

            //if (UndoService.UndoContext is { } context)
            //{
            //    context.AddAction(action);
            //}
            //else
            //{
            //    UndoService.AddAction(action);
            //}
        }

        private void OnContextChanged(object? sender, ContextChangedEventArgs e)
        {
            ParentLayout?.OnContextChanged(sender, e);
        }

        public void Undo(IReadOnlyList<UndoAction> actions)
        {
            if(EditContext?.Model is not ViewModelContext<TViewModel> context)
            {
                return;
            }

            using (SuspendContextChangingNotification())
            {
                foreach (var action in actions)
                {
                    if (context.Properties.Contains(action.Name))
                    {
                        context.SetValue(action.Name, action.Value);
                    }
                }
            }
        }

        private IDisposable SuspendContextChangingNotification()
        {
            if (Context is { } context)
            {
                context.Changing -= OnContextChanging;
            }

            return Disposable.Create(this, o => o.Context!.Changing += o.OnContextChanging);
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
