using System.Runtime.CompilerServices;

namespace PROWeb.WebSiteService.Services.DependencyInjections
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddProServices(this IServiceCollection services)
        {
            services.AddScoped<IProService, ProService>();

            return services;
        }
    }
}
