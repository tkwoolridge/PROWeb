using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Components.Configurations;
using PROWeb.Components.Mapping;
using PROWeb.Components.Person.MiddleWares;
using PROWeb.Components.Reports.Services;
using PROWeb.Components.Services.Emails;
using PROWeb.Components.Services.State;
using PROWeb.Components.Services.Undo;
using PROWeb.Data.Services.Configuration;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

namespace PROWeb.Components.DependencyInjection
{
    public static class RegisterModuleExtensions
    {
        public static void AddPROWebComponentsModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(ComponentsMapper).Assembly);
            services.AddSingleton(config);

            // Register navigation service.
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddControllers().AddNewtonsoftJson();

            // Register state services.
            services.AddScoped(typeof(IStateService<,>), typeof(StateService<,>));

            // Register undo services.
            services.AddScoped(typeof(IUndoService<>), typeof(UndoService<>));

            //Register email service.
            services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<EmailService>();
            services.AddHostedService(serviceProvider => serviceProvider.GetService<EmailService>()!);

            IConfiguration ResolveConfiguration(IWebHostEnvironment? environment)
            {
                var reportingConfigFileName = Path.Combine(environment?.ContentRootPath ?? String.Empty, "appsettings.json");
                return new ConfigurationBuilder()
                    .AddJsonFile(reportingConfigFileName, true)
                    .Build();
            }

            services.AddScoped<IReportSourceResolver, ReportSourceResolver>();
            services.AddSingleton<IReportServiceConfiguration>(sp =>
               new ReportServiceConfiguration
               {
                   ReportingEngineConfiguration = ResolveConfiguration(sp.GetService<IWebHostEnvironment>()),
                   HostAppId = "PROWeb.Office",
                   Storage = new FileStorage(),
                   ReportSourceResolver = sp.GetService<IReportSourceResolver>(),
                   ReportSharingTimeout = 1400
               });

            services.Configure<TCDSettings>(builder.Configuration.GetSection(nameof(TCDSettings)));
        }

        public static void AddPROWebComponentsEndpoints(this WebApplication app)
        {
            app.MapControllers();
            //app.UseMiddleware<HandleTCDPhotosMiddleWare>();
        }
    }
}
