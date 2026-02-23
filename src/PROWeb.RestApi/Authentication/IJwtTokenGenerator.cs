using PROWeb.RestApi.Authentication.Models;

namespace PROWeb.RestApi.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ServiceUser user);
    }
}