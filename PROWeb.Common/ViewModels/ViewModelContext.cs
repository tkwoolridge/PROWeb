using PROWeb.Common.StrongBindings;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace PROWeb.Common.ViewModels
{
    public abstract class ViewModelContext<TViewModel> : SlimViewModelBase where TViewModel : SlimViewModelBase
    {
        private readonly Dictionary<string, IStrongBindingPath> _propertyPaths = new Dictionary<string, IStrongBindingPath>();

        private TViewModel? _model;

        private readonly CompositeDisposable _bindingsDisposable = new CompositeDisposable();

        public Dictionary<string, IStrongBindingPath>.KeyCollection Properties => _propertyPaths.Keys;

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

        public ViewModelContext()
        {
            PropertyChanging += OnPropertyChanging;
            PropertyChanged += OnPropertyChanged;
        }

        public object? GetValue(string name)
        {
            if (GetPropertyPath(name) is { } path)
            {
                return path.ReadProperty(this);
            }

            return null;
        }

        public void SetValue(string name, object? value)
        {
            if (GetPropertyPath(name) is { } path)
            {
                path.WriteProperty(this, value);
            }
        }

        public void UnBind()
        {
            _bindingsDisposable.Clear();
            _propertyPaths.Clear();
        }

        protected void AddBinding(IStrongInstanceBinding? binding)
        {
            if (binding is null)
            {
                return;
            }

            var path = binding.TargetPath;
            _propertyPaths.TryAdd(path.PropertyName, path);
            _bindingsDisposable.Add(binding);
        }

        private IStrongBindingPath? GetPropertyPath(string name)
        {
            if (_propertyPaths.TryGetValue(name,out IStrongBindingPath? path))
            {
                return path;
            }

            return null;
        }

        protected void AddPropertyPath(IStrongBindingPath path)
        {
            _propertyPaths.TryAdd(path.PropertyName, path);
        }

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
