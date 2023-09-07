using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Services.Emails;
using PROWeb.Components.Services.State;
using PROWeb.Components.Voters.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Components.Extensions
{
    public static class RegisterModuleExtension
    {
        public static void AddPROWebComponentsModule(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            // Register assessment filter state service.
            services.AddScoped<IStateService<AssessmentFilterViewModel>, StateService<AssessmentFilterViewModel>>();

            // Register voter filter state service.
            services.AddScoped<IStateService<FilterModel>, StateService<FilterModel>>();

            // Register assessments grid state service.
            services.AddScoped<IStateService<GridState<AssessmentViewModel>>, StateService<GridState<AssessmentViewModel>>>();

            // Register voters grid state service.
            services.AddScoped<IStateService<GridState<VoterViewModel>>, StateService<GridState<VoterViewModel>>>();

            //Register email service.
            services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<EmailService>();
            services.AddHostedService(serviceProvider => serviceProvider.GetService<EmailService>()!);
        }
    }
}
