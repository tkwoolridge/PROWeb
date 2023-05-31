using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.Configuration;
using PROWeb.Data.Services.EligiblePoll;
using PROWeb.Data.Services.Logging;
using PROWeb.Data.Services.Registrations;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Data.Services.Extensions
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebDataModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddDbContextFactory<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PROWebConnection")));

            //Register preload data service.
            services.AddHostedService<PreloadService>();

            // Register navigation service.
            services.AddSingleton<INavigationService, NavigationService>();

            // Register address service.
            services.AddSingleton<IAssessmentServiceFactory, AssessmentServiceFactory>();

            // Register voters service.
            services.AddSingleton<IVotersServiceFactory, VotersServiceFactory>();

            // Register registration service.
            services.AddSingleton<IRegistrationServiceFactory, RegistrationServiceFactory>();

            // Register candidates service.
            services.AddSingleton<IEligiblePollServiceFactory, EligiblePollServiceFactory>();

            // Register activity log.
            services.AddSingleton<IActivityLogServiceFactory, ActivityLogServiceFactory>();

            // Register activity log factory.
            services.AddSingleton<IActivityLogServiceFactory, ActivityLogServiceFactory>();

            // Register activity log service.
            services.AddTransient<IActivityLogService, ActivityLogService>();
        }
    }
}
