using PROWeb.WebSiteService.Services;

namespace PROWeb.WebSiteService.Repositories.DependencyInjections
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddProRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProRepository, ProRepository>();

            return services;
        }
    }
}
