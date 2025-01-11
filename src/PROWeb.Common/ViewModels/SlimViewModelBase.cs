using System.Collections;
using System.ComponentModel;

namespace PROWeb.Common.ViewModels
{
    /// <summary>
    /// Base class for view models with small footprint.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="ViewModelBase" /> it does not rely on ReactiveUI and provides a fast path
    /// for property subscriptions.
    /// </remarks>
    /// TODO: Add SourceGenerator to generate the validation boilerplate
    public class SlimViewModelBase : SlimReactiveObject, INotifyDataErrorInfo
    {
        private Dictionary<string, HashSet<ValidationError>>? _propertyErrors;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _propertyErrors?.Any(p => p.Value.Count > 0) ?? false;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_propertyErrors == null || propertyName == null)
            {
                //Just why in the world is the return value not nullable?!
                return Enumerable.Empty<object>();
            }

            return _propertyErrors.TryGetValue(propertyName, out var errorSet) ? errorSet : Enumerable.Empty<object>();
        }

        /// <summary>
        /// Clear all errors associated with a property.
        /// </summary>
        /// <param name="propertyName">The name of the affected property.</param>
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors != null && _propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// Clear a single error from the list.
        /// </summary>
        /// <param name="propertyName">The name of the affected property.</param>
        /// <param name="validationError">The validation error to clear.</param>
        public void ClearError(string propertyName, in ValidationError validationError)
        {
            if (_propertyErrors == null || validationError == ValidationError.None)
            {
                return;
            }

            if (_propertyErrors.TryGetValue(propertyName, out var errors)
                && errors.Remove(validationError))
            {
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// Add a validation error to the list.
        /// </summary>
        /// <param name="propertyName">The name of the affected property.</param>
        /// <param name="validationError">The validation error to add.</param>
        public void SetError(string propertyName, in ValidationError validationError)
        {
            if (validationError == ValidationError.None)
            {
                return;
            }

            if (_propertyErrors == null)
            {
                _propertyErrors = new Dictionary<string, HashSet<ValidationError>>
                {
                    [propertyName] = new()
                {
                    validationError
                }
                };

                OnErrorsChanged(propertyName);

                return;
            }

            if (!_propertyErrors.TryGetValue(propertyName, out var errors))
            {
                errors = _propertyErrors[propertyName] = new HashSet<ValidationError>();
            }

            if (errors.Add(validationError))
            {
                OnErrorsChanged(propertyName);
            }
        }

        internal void ActivateInternal()
        {
            OnActivated();
        }

        protected virtual void OnActivated()
        {
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }
}