using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using PROWeb.RestApi.Authentication.Configuration;
using PROWeb.RestApi.Authentication.Constants;

namespace PROWeb.RestApi.Authentication
{
    public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private readonly ApiKeySettings _settings;
        
        public ApiKeyHandler(IOptions<ApiKeySettings> options)
        {
            _settings = options.Value;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            if(context.Resource is not HttpContext httpContext)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            string apiKey = httpContext.Request.Headers[ApiKeyConstants.ApiKeyHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }


            if (!requirement.IsValidateApiKey(_settings, apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
