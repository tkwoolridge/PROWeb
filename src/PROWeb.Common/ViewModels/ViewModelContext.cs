using PROWeb.Common.StrongBindings;
using System.Reactive.Disposables;

namespace PROWeb.Common.ViewModels
{
    public abstract class ViewModelContext<TViewModel> : ViewModelContextBase<TViewModel> where TViewModel : SlimViewModelBase
    {
        private readonly Dictionary<string, IStrongBindingPath> _propertyPaths = new Dictionary<string, IStrongBindingPath>();

        public Dictionary<string, IStrongBindingPath>.KeyCollection Properties => _propertyPaths.Keys;

        private readonly CompositeDisposable _bindingsDisposable = new CompositeDisposable();

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

        public override object? GetValue(string name)
        {
            if (GetPropertyPath(name) is { } path)
            {
                return path.ReadProperty(this);
            }

            return null;
        }

        public override void SetValue(string name, object? value)
        {
            if (GetPropertyPath(name) is { } path)
            {
                path.WriteProperty(this, value);
            }
        }
        private IStrongBindingPath? GetPropertyPath(string name)
        {
            if (_propertyPaths.TryGetValue(name, out IStrongBindingPath? path))
            {
                return path;
            }

            return null;
        }
    }
}
