using System.Runtime.CompilerServices;

namespace PROWeb.WebService.Services.DependencyInjections
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddProServices(this IServiceCollection services)
        {
            services.AddScoped<IProService, ProService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}
