using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Components.Mapping;
using PROWeb.Components.Services.Emails;
using PROWeb.Components.Services.State;
using PROWeb.Components.Services.Undo;

namespace PROWeb.Components.DependencyInjection
{
    public static class ComponentsExtensions
    {
        public static void AddPROWebComponentsModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(ComponentsMapper).Assembly);
            services.AddSingleton(config);

            // Register state services.
            services.AddScoped(typeof(IStateService<>), typeof(StateService<>));

            // Register undo services.
            services.AddScoped(typeof(IUndoService<>), typeof(UndoService<>));

            //Register email service.
            services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<EmailService>();
            services.AddHostedService(serviceProvider => serviceProvider.GetService<EmailService>()!);
        }
    }
}
