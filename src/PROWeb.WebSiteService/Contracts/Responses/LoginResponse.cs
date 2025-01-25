
namespace PROWeb.WebSiteService.Contracts.Responses
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public long Expires { get; set; }
    }
}
