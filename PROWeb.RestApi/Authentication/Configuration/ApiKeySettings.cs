namespace PROWeb.RestApi.Authentication.Configuration
{
    public class ApiKeySettings
    {
        public static string SectionName { get; } = "ApiKeySettings";

        public string? ApiKey { get; set; }
    }
}
