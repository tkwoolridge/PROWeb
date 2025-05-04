namespace PROWeb.WebResources.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddWebResourcesOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WebResourcesOptions>(configuration.GetSection(WebResourcesOptions.SectionName));
            return services;
        }

        public static IServiceCollection AddTCDPhotosOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TCDPhotosOptions>(configuration.GetSection(TCDPhotosOptions.SectionName));
            return services;
        }
    }
}
