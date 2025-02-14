using PROWeb.WebService.Services;

namespace PROWeb.WebService.Repositories.DependencyInjections
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddProRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddScoped<IProRepository, ProRepository>();

            return services;
        }
    }
}
