using PROWeb.Common.ViewModels;

namespace PROWeb.Authentication.Components.Users.ViewModels
{
    public class UserFilterViewModel : SlimViewModelBase
    {
        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
