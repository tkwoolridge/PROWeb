using Microsoft.AspNetCore.Components;
using PROWeb.Authentication.Components.Users.ViewModels;
using PROWeb.Common.Extensions;
using PROWeb.Components.Common;
using PROWeb.Data.Authentication.Services.Users;
using PROWeb.Office.Shared.Users.ViewModels;

namespace PROWeb.Office.Shared.Users
{
    public partial class UsersGrid : PROGridComponent<UserFilterViewModel, UserViewModel>
    {
        [Inject]
        private IUsersServiceFactory _usersServiceFactory { get; set; } = null!;

        [Parameter]
        public EventCallback<UserViewModel> UserSelected { get; set; }

        protected override void OnSelectionChanged(IEnumerable<UserViewModel> selectedItems)
        {
            base.OnSelectionChanged(selectedItems);

            if (selectedItems.FirstOrDefault() is { } user)
            {
                UserSelected.InvokeAsync(user);
            }
        }

        protected async override Task<IList<UserViewModel>> GetDataAsync(UserFilterViewModel filter)
        {
            using (var service = _usersServiceFactory.CreateService())
            {
                IList<UserViewModel> data = await service.GetUsers
                    (
                        filter.UserName,
                        filter.FirstName,
                        filter.LastName
                    ).ProjectToListAsync<UserViewModel>();

                return data;
            }
        }
    }
}
