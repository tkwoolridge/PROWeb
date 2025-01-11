namespace PROWeb.WebSiteService.Authentication.Models
{
    public class JwtSettings
    {
        public static string SectionName { get; } = "JwtSettings";

        public string Secret { get; init; } = null!;

        public int ExpiryMinutes { get; init; }

        public string Issuer { get; init; } = null!;

        public string Audience { get; init; } = null!;

        public string UserName { get; init; } = null!;

        public string Password { get; init; } = null!;
    }
}
