using PROWeb.Authentication.Components.Users;
using PROWeb.Office.Shared.Users.ViewModels;

namespace PROWeb.Office.Shared.Users.Views
{
    public class OfficerView : UserView<UserViewModel>
    {
        public OfficerView() : base(
            u => u.Email, 
            u => u.UserName, 
            u => u.FirstName, 
            u => u.LastName,
            u => u.RoleId,
            u => u.IsActive)
        {
        }
    }
}
