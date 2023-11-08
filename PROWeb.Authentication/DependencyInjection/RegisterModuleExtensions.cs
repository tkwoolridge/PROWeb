using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Data.Authentication;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Authentication.DependencyInjection
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebAuthenticationModule(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "Identity",
                areaName: "Identity",
                pattern: "Identity/{controller=Sales}/{action=Index}"
            );
        }

        public static void AddPROWebAuthenticationModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<PROUser>>();


            builder.Services.AddDbContext<IdentityDataContext>();

            builder.Services.AddIdentity<PROUser, IdentityRole>(options =>
            {
                // Password settings.
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<IdentityDataContext>()
            .AddTokenProvider<EmailTokenProvider<PROUser>>("Email");

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
            });


            services.AddControllersWithViews();

            services.AddKendo();
        }
    }
}
