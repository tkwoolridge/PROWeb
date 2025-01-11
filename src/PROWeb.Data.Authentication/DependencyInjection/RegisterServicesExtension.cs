using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Data.Authentication;
using PROWeb.Data.Authentication.Services;
using PROWeb.Data.Authentication.Services.CachedData;
using PROWeb.Data.Authentication.Services.Users;

namespace PROWeb.Data.Services.Extensions
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebDataAuthenticationModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddDbContextFactory<IdentityDataContext>();

            // Register user service.
            services.AddScoped<IUsersServiceFactory, UsersServiceFactory>();

            //Register preload data service.
            services.AddHostedService<IdentityPreloadService>();

            //Register preload data service.
            services.AddSingleton<ICachedIdentityDataService, CachedIdentityDataService>();
        }
    }
}
