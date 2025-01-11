using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Components.Reports.Configuration;
using PROWeb.Data.Mappings;
using PROWeb.Data.Services;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.CachedData;
using PROWeb.Data.Services.Configuration;
using PROWeb.Data.Services.EligiblePoll;
using PROWeb.Data.Services.Logging;
using PROWeb.Data.Services.Map;
using PROWeb.Data.Services.Office;
using PROWeb.Data.Services.Registrations;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Data.DependencyInjection
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebDataModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(DataMapper).Assembly);
            services.AddSingleton(config);

            services.AddDbContextFactory<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PROWebConnection")));

            //Register preload data service.
            services.AddHostedService<PreloadService>();

            //Register preload data service.
            services.AddSingleton<ICachedDataService, CachedDataService>();

            // Register navigation service.
            services.AddSingleton<INavigationService, NavigationService>();

            // Register reports service.
            services.AddSingleton<IReportsService, ReportsService>();

            // Register address service.
            services.AddSingleton<IAssessmentServiceFactory, AssessmentServiceFactory>();

            // Register voters service.
            services.AddSingleton<IVotersServiceFactory, VotersServiceFactory>();

            // Register voters service.
            services.AddSingleton<IMapServiceFactory, MapServiceFactory>();

            // Register registration service.
            services.AddSingleton<IRegistrationServiceFactory, RegistrationServiceFactory>();

            // Register candidates service.
            services.AddSingleton<IEligiblePollServiceFactory, EligiblePollServiceFactory>();

            // Register activity log.
            services.AddSingleton<IActivityLogServiceFactory, ActivityLogServiceFactory>();

            // Register activity log factory.
            services.AddSingleton<IActivityLogServiceFactory, ActivityLogServiceFactory>();

            // Register office service factory.
            services.AddSingleton<IOfficeServiceFactory, OfficeServiceFactory>();

            // Register activity log service.
            services.AddTransient<IActivityLogService, ActivityLogService>();
        }
    }
}
