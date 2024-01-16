using PROWeb.Common.ViewModels;

namespace PROWeb.Authentication.Components.Users.ViewModels
{
    public class UserRoleViewModel : SlimViewModelBase
    {
        public int? Id { get; set; }

        public string? Description { get; set; }
    }
}
