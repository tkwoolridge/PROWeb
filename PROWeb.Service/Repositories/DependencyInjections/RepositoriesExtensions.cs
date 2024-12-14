using PROWeb.Service.Services;

namespace PROWeb.Service.Repositories.DependencyInjections
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
