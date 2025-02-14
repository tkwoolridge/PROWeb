using PROWeb.WebService.Models;

namespace PROWeb.WebService.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ServiceUser user);
    }
}