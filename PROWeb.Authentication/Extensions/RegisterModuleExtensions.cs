using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Data.Models;
using PROWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROWeb.Authentication.Controllers;

namespace PROWeb.Authentication.Extensions
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebAuthetnticationModule(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Navigation}/{action=Index}/{id?}");                    
        }

        public static void AddPROWebAuthetnticationModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.Services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PROWebIdentityConnection")));

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
            }).AddEntityFrameworkStores<IdentityDataContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
            });

            services.AddControllers();

            services.AddKendo();
        }
    }
}
