using Microsoft.EntityFrameworkCore;

namespace PROWeb.Data.Services.CachedData
{
    public class CachedDataService : ICachedDataService
    {
        public int RegistrationYear { get; set; }

        public async Task PreloadCachedDataAsync(DataContext context)
        {
            await PreloadOfficeDataAsync(context);
        }

        private async Task PreloadOfficeDataAsync(DataContext context)
        {
            var office = await context.PROOffices.FirstOrDefaultAsync();

            RegistrationYear = office?.ElectionYear ?? DateTime.Now.Year;
        }
    }
}
