using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PROWeb.RestApi.Authentication;
using PROWeb.RestApi.Authentication.Configuration;
using PROWeb.RestApi.Authentication.Constants;
using System.Text;

namespace PROWeb.WebService.Authentication.DependencyInjection
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddApiKeyAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKeySettings = new ApiKeySettings();
            configuration.Bind(nameof(ApiKeySettings), apiKeySettings);

            services.AddSingleton(Options.Create(apiKeySettings));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiKeyConstants.Policy, policy =>
                {
                    policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
                    policy.Requirements.Add(new ApiKeyRequirement());
                });
            });

            services.AddScoped<IAuthorizationHandler, ApiKeyHandler>();

            return services;
        }
    }
}
