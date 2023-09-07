using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Authentication.Models;

namespace PROWeb.Authentication.Extensions
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebAuthetnticationModule(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "Identity",
                areaName: "Identity",
                pattern: "Identity/{controller=Sales}/{action=Index}"
            );
        }

        public static void AddPROWebAuthetnticationModule<TIdentityDbContext, TUser>(this WebApplicationBuilder builder) 
            where TUser : IdentityUser, IPROUser
            where TIdentityDbContext : IdentityDbContext<TUser>
        {
            var services = builder.Services;

            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<TUser>>();


            builder.Services.AddDbContext<TIdentityDbContext>();

            builder.Services.AddIdentity<TUser, IdentityRole>(options =>
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
            .AddEntityFrameworkStores<TIdentityDbContext>()
            .AddTokenProvider<EmailTokenProvider<TUser>>("Email");

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
