using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Authentication.Components.Users.Contexts;
using PROWeb.Authentication.Components.Users.ViewModels;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Data.Authentication.Services.CachedData;
using System.Linq.Expressions;

namespace PROWeb.Authentication.Components.Users
{
    public partial class UserView<TUserViewModel> : PROEditableView<TUserViewModel> where TUserViewModel : SlimViewModelBase
    {
        [Inject]
        public ICachedIdentityDataService _cachedIdentityDataService { get; set; } = null!;


        private readonly Expression<Func<TUserViewModel, string?>>? _email;
        private readonly Expression<Func<TUserViewModel, string?>>? _userName;
        private readonly Expression<Func<TUserViewModel, string?>>? _firstName;
        private readonly Expression<Func<TUserViewModel, string?>>? _lastName;
        private readonly Expression<Func<TUserViewModel, int?>>? _roleId;
        private readonly Expression<Func<TUserViewModel, bool?>>? _isActive;

        internal UserContext<TUserViewModel> UserContext { get; set; } = new();

        protected List<UserRoleViewModel>? Roles { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Roles = _cachedIdentityDataService.Roles.Adapt<List<UserRoleViewModel>>();
        }

        public UserView(
            Expression<Func<TUserViewModel, string?>>? email, 
            Expression<Func<TUserViewModel, string?>>? userName, 
            Expression<Func<TUserViewModel, string?>>? firstName, 
            Expression<Func<TUserViewModel, string?>>? lastName, 
            Expression<Func<TUserViewModel, int?>>? roleId,
            Expression<Func<TUserViewModel, bool?>>? isActive)
        {
            _email = email;
            _userName = userName;
            _firstName = firstName;
            _lastName = lastName;
            _roleId = roleId;
            _isActive = isActive;
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(UserContext);
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            UserContext.UnBind();
            if (Model is { } model)
            {
                UserContext.Bind
                (
                model,
                _email,
                _userName,
                _firstName,
                _lastName,
                _roleId,
                _isActive
                );
            }
        }
    }   
}
