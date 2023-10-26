namespace PROWeb.Common.ViewModels
{
    public class ContextChangedEventArgs : EventArgs
    {
        public string PropertyName { get; set; }

        public object? PropertyValue { get; set; }

        public ContextChangedEventArgs(string propertyName, object? propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}