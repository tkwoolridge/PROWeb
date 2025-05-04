using Microsoft.AspNetCore.Authorization;
using PROWeb.RestApi.Authentication.Configuration;

namespace PROWeb.RestApi.Authentication
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public bool IsValidateApiKey(ApiKeySettings settings, string userApiKey)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;
            string? apiKey = settings.ApiKey;
            if (apiKey == null || apiKey != userApiKey)
                return false;
            return true;
        }
    }
}
