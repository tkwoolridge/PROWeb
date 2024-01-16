// Ignore Spelling: Preload

using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Data.Authentication.Services.CachedData
{
    public class CachedIdentityDataService : ICachedIdentityDataService
    {
        public List<PRORole> Roles { get; private set; } = Enumerable.Empty<PRORole>().ToList();

        public async Task PreloadCachedDataAsync(IdentityDataContext context)
        {
            await PreloadIdentityData(context);
        }

        private async Task PreloadIdentityData(IdentityDataContext context)
        {
            Roles = await context.Roles.ToListAsync();
        }
    }
}
