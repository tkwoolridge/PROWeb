using Microsoft.AspNetCore.Components;
using PROWeb.Authentication.Components.Users.ViewModels;
using PROWeb.Common.Components;
using PROWeb.Common.Extensions;
using PROWeb.Data.Authentication.Services.Users;
using PROWeb.Office.Shared.Users.ViewModels;
using Telerik.Blazor.Components;
namespace PROWeb.Office.Shared.Users
{
    public partial class UsersList : PROListComponent<UserFilterViewModel, UserViewModel>
    {
        [Inject]
        private IUsersServiceFactory _usersServiceFactory { get; set; } = null!;

        protected TelerikListView<UserViewModel>? ListRef { get; set; }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            if (args.Item is UserViewModel current &&
                Data?.FirstOrDefault(m => m.Id == current.Id) is { } previous &&
                Data?.IndexOf(previous) is { } index && index > -1)
            {
                Data?.RemoveAt(index);
                Data?.Insert(index, current);

                ListRef?.Rebind();
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
