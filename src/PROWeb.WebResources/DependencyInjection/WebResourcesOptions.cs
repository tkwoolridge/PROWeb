namespace PROWeb.WebResources.DependencyInjection
{
    public class WebResourcesOptions
    {
        public const string SectionName = "WebResources";

        public string TCDPhotosPath { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
