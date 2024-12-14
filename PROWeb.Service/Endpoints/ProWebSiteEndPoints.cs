using PROWeb.Service.Contracts.Responses;
using PROWeb.Service.Services;
using System.ComponentModel.Design;

namespace PROWeb.Service.Endpoints
{
    public static class ProWebSiteEndpoints
    {
        public static void MapProWebSiteEndpoints(this WebApplication app)
        {
            app.MapGet("/constituencies", GetContituencies.HandleAsync).WithName("GetConstituencies");
        }

        public class GetContituencies
        {
            public static async Task<IReadOnlyList<ConstituencyResponse>> HandleAsync(IProService service)
            {
                var result = await service.GetConstituenciesAsync();

                 return result;
             }
        }
    }
}
