using PROWeb.RestApi.Authentication;
using Srl = Serilog;

namespace PROWeb.WebResources.Endpoints
{
    public static class WebResourcesEndpoints
    {
        public static void MapWebResourcesEndpoints(this WebApplication app)
        {
            var websiteGroup = app.MapGroup(string.Empty)
                .RequireAuthorization(p => p.Requirements.Add(new ApiKeyRequirement()));
        }
    }
}
