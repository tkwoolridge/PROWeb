namespace PROWeb.Components.Services.Undo
{
    public interface IUndoContext : IDisposable
    {
        void AddAction(UndoAction action);
    }
}