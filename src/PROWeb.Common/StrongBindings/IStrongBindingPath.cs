namespace PROWeb.Common.StrongBindings
{
    public interface IStrongBindingPath
    {
        string PropertyName { get; }

        object? ReadProperty(object source);

        void WriteProperty(object source, object? value);
    }
}