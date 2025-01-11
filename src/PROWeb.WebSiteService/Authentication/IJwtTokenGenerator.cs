namespace PROWeb.WebSiteService.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken();
    }
}