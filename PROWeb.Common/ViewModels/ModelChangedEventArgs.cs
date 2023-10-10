namespace PROWeb.Common.ViewModels
{
    public class ModelChangedEventArgs : EventArgs
    {
        public string PropertyName { get; set; }

        public object? PropertyValue { get; set; }

        public ModelChangedEventArgs(string propertyName, object? propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}