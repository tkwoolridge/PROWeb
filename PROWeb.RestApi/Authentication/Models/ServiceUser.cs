using PROWeb.RestApi.Authentication.Models.Enums;

namespace PROWeb.RestApi.Authentication.Models
{
    public class ServiceUser
    {
        public string? Organization { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public ServiceUserRoles Role { get; set; }
    }
}
