using PROWeb.Common.ViewModels;
using System.Reactive.Disposables;

namespace PROWeb.Components.Services.Undo
{
    public class UndoService<TViewModel> : IUndoService<TViewModel> where TViewModel : SlimViewModelBase
    {
        private readonly Stack<UndoGroup> _undoes = new Stack<UndoGroup>();
        private UndoGroup? _undoGroup = null;
        private bool _suspendUndo;

        public IReadOnlyList<UndoAction>? Next
        {
            get
            {
                if(_undoes.TryPop(out UndoGroup? group))
                {
                    return group?.Actions;
                }

                return null; 
            }
        }

        public bool HasActions => _undoes.Count > 0 || _undoGroup?.Actions.Count > 0;

        public IDisposable CreateScope()
        {
            _undoGroup = new UndoGroup();

            return Disposable.Create(this, o =>
            {
                o.AddGroup(_undoGroup);
                o._undoGroup = null;
            });
        }

        public IDisposable SuspendUndo() 
        {
            _suspendUndo = true;

            return Disposable.Create(this, d => d._suspendUndo = false);
        }

        public void AddAction(UndoAction action)
        {
            if (_suspendUndo)
            {
                return;
            }

            if (_undoGroup is not { } group)
            {
                _undoes.Push(new UndoGroup(action));

                return;
            }

            group.AddAction(action);
        }

        private void AddGroup(UndoGroup group)
        {
            if(_suspendUndo)
            {
                return;
            }

            _undoes.Push(group);
        }

        public void Reset()
        {
            _undoes.Clear();
        }

        private class UndoGroup
        {
            private readonly List<UndoAction> _actions = new List<UndoAction>();

            public UndoGroup()
            {
            }

            public UndoGroup(UndoAction action)
            {
                _actions.Add(action);
            }

            public IReadOnlyList<UndoAction> Actions { get => _actions; }

            public void AddAction(UndoAction action)
            {
                _actions.Add(action);
            }
        }
    }

    public record UndoAction
    (
        string Name,
        object? Value
    );
}
