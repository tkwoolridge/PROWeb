namespace PROWeb.RestApi.WebResources.Configuration
{
    public class WebResourcesSettings
    {
        public const string SectionName = "WebResources";
        
        public string BaseUrl { get; set; } = string.Empty;
        public string PROPhotosPath { get; set; } = string.Empty;
        public string TCDPhotosPath { get; set; } = string.Empty;
        public string? ApiKey { get; set; }
    }
}
