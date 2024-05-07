using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Services.Undo
{
    public interface IUndoService<TViewModel> where TViewModel : SlimViewModelBase
    {
        IReadOnlyList<UndoAction>? Next { get; }

        void AddAction(UndoAction action);

        IDisposable CreateScope();

        IDisposable SuspendUndo();

        void Reset();

        bool HasActions { get; }
    }
}