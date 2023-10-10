using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Services.Undo
{
    public class UndoService<TViewModel> : IUndoService<TViewModel> where TViewModel : SlimViewModelBase
    {
        private readonly Stack<UndoGroup> _undoes = new Stack<UndoGroup>();

        public IReadOnlyList<UndoAction>? Next
        {
            get
            {
                return _undoes.Pop()?.Actions;
            }
        }

        public IUndoContext NewScope()
        {
            return new UndoContext(this);
        }

        public void AddAction(UndoAction action)
        {
            _undoes.Push(new UndoGroup(action));
        }

        private void AddGroup(UndoGroup group)
        {
            _undoes.Push(group);
        }

        private class UndoContext : IUndoContext
        {
            private UndoService<TViewModel> _services;
            private UndoGroup _group = new UndoGroup();

            public UndoContext(UndoService<TViewModel> services)
            {
                _services = services;
            }

            public void AddAction(UndoAction action)
            {
                _group.AddAction(action);
            }

            public void Dispose()
            {
                _services.AddGroup(_group);
            }
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
