namespace PROWeb.Data.Authentication.Models.Views
{
    public class PROUserView
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsActive { get; set; }

        public int? RoleId { get; set; }

        public string? RoleDescription { get; set; }
    }
}
