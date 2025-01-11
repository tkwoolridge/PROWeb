namespace PROWeb.Common.ViewModels
{
    public class ContextChangingEventArgs : EventArgs
    {
        public string PropertyName { get; set; }

        public object? OldPropertyValue { get; set; }

        public ContextChangingEventArgs(string propertyName, object? oldPropertyValue)
        {
            PropertyName = propertyName;
            OldPropertyValue = oldPropertyValue;
        }
    }
}
