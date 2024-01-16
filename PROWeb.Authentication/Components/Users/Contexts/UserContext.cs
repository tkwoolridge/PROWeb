using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PROWeb.Authentication.Components.Users.Contexts
{
    internal class UserContext<TUserViewModel> : ViewModelContext<TUserViewModel> where TUserViewModel : SlimViewModelBase
    {
        private string? _userName;

        public string? UserName
        {
            get => _userName;
            set => RaiseAndSetIfChanged(ref _userName, value.ToNullIfWhiteSpace());
        }

        private string? _email;

        [Required(ErrorMessage = "User email is required!")]
        [EmailAddress(ErrorMessage = "User email is not in correct format!")]
        public string? Email
        {
            get => _email;
            set => RaiseAndSetIfChanged(ref _email, value.ToNullIfWhiteSpace());
        }

        private string? _firstName;

        public string? FirstName
        {
            get => _firstName;
            set => RaiseAndSetIfChanged(ref _firstName, value.ToNullIfWhiteSpace());
        }

        private string? _lastName;

        public string? LastName
        {
            get => _lastName;
            set => RaiseAndSetIfChanged(ref _lastName, value.ToNullIfWhiteSpace());
        }

        private int? _roleId;

        public int? RoleId
        {
            get => _roleId;
            set => RaiseAndSetIfChanged(ref _roleId, value);
        }

        private bool? _isActive;

        public bool? IsActive
        {
            get => _isActive;
            set => RaiseAndSetIfChanged(ref _isActive, value);
        }

        public void Bind(
            TUserViewModel model,
            Expression<Func<TUserViewModel, string?>>? email = null,
            Expression<Func<TUserViewModel, string?>>? userName = null,
            Expression<Func<TUserViewModel, string?>>? firstNamePath = null,
            Expression<Func<TUserViewModel, string?>>? lastNamePath = null,
            Expression<Func<TUserViewModel, int?>>? roleId = null,
            Expression<Func<TUserViewModel, bool?>>? isActive = null)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model.Bind(this, roleId, c => c.RoleId, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, userName, c => c.UserName, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, firstNamePath, c => c.FirstName, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, lastNamePath, c => c.LastName, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, email, c => c.Email, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, isActive, c => c.IsActive, StrongBindingMode.TwoWay));
            }
        }
    }
}
