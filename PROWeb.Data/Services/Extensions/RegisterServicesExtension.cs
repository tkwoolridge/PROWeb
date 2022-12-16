using Microsoft.Extensions.DependencyInjection;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.Configuration;
using PROWeb.Data.Services.Logging;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Data.Services.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void AddPROWebServices(this IServiceCollection services)
        {
            //Register preload data service.
            services.AddHostedService<PreloadService>();
            
            // Register navigation service.
            services.AddSingleton<INavigationService, NavigationService>();

            // Register address service.
            services.AddSingleton<IAssessmentServiceFactory, AssessmentServiceFactory>();

            // Register voters service.
            services.AddSingleton<IVotersServiceFactory, VotersServiceFactory>();

            // Register activity log.
            services.AddSingleton<IActivityLogServiceFactory, ActivityLogServiceFactory>();
        }
    }
}
