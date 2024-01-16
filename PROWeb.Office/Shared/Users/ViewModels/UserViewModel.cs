using PROWeb.Common.ViewModels;

namespace PROWeb.Office.Shared.Users.ViewModels
{
    public class UserViewModel : SlimViewModelBase
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public int? RoleId { get; set; }

        public string? RoleDescription { get; set; }

        public bool? IsActive { get; set; }
    }
}
