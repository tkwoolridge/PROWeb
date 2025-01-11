using System.ComponentModel;

namespace PROWeb.Common.ViewModels
{
    public abstract class ViewModelContextBase<TViewModel> : SlimViewModelBase where TViewModel : SlimViewModelBase
    {
        private TViewModel? _model;

        public TViewModel? Model
        {
            get => _model;
            set
            {
                _model = value;
            }
        }

        public event EventHandler<ContextChangingEventArgs>? Changing;

        public event EventHandler<ContextChangedEventArgs>? Changed;

        public ViewModelContextBase()
        {
            PropertyChanging += OnPropertyChanging;
            PropertyChanged += OnPropertyChanged;
        }

        public abstract object? GetValue(string name);

        public abstract void SetValue(string name, object? value);

        private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName is { } propertyName)
            {
                var value = GetValue(propertyName);

                Changing?.Invoke(this, new ContextChangingEventArgs(propertyName, value));
            }
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is { } propertyName)
            {
                var value = GetValue(propertyName);

                Changed?.Invoke(this, new ContextChangedEventArgs(propertyName, value));
            }
        }
    }
}
