using PROWeb.Data.Authentication.Models;

namespace PROWeb.Data.Authentication.Services.CachedData
{
    public interface ICachedIdentityDataService
    {
        List<PRORole> Roles { get; }

        Task PreloadCachedDataAsync(IdentityDataContext context);
    }
}
