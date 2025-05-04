using Microsoft.Extensions.Options;
using PROWeb.RestApi.WebResources.Configuration;
using PROWeb.RestApi.WebResources.Middlewares;

namespace PROWeb.RestApi.WebResources.DependencyInjection
{
    public static  class WebResourcesExtensions
    {
        public static IServiceCollection AddWebResources(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new WebResourcesSettings();
            configuration.Bind(WebResourcesSettings.SectionName, settings);
            services.AddSingleton(Options.Create(settings));

            services.AddHttpClient("WebResourcesClient", client =>
            {
                string baseUrl = settings.BaseUrl;

                client.BaseAddress = new Uri(baseUrl);
            });

            return services;
        }

        public static WebApplication UseWebResources(this WebApplication app)
        {
            app.UseMiddleware<HandlePhotoMiddleware>();
            return app;
        }
    }
}
