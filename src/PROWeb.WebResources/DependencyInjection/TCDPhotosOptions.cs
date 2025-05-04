namespace PROWeb.WebResources.DependencyInjection
{
    public class TCDPhotosOptions
    {
        public const string SectionName = "TCDPhotos";

        public string? FTPServer { get; set; }

        public string? Path { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
