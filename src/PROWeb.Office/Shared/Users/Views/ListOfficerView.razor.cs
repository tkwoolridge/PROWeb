using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Data.Authentication.Models.Views;
using PROWeb.Data.Authentication.Services.Users;
using PROWeb.Office.Shared.Users.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Users.Views
{
    public partial class ListOfficerView : PROCompositeView<UserViewModel>
    {
        [Inject]
        private IUsersServiceFactory _usersServiceFactory { get; set; } = null!;

        [Parameter]
        public bool Editable { get; set; }

        public async Task OnEditAsync(ListViewCommandEventArgs e)
        {
            bool result = await OnSaveAsync();

            e.IsCancelled = !result;
        }

        protected override async Task SaveAsync(UserViewModel model)
        {
            using (var service = _usersServiceFactory.CreateService())
            {
                var user = model.Adapt<PROUserView>();

                await service.UpdateUser(user);
            }
        }
    }
}
