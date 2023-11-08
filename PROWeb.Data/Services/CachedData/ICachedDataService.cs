namespace PROWeb.Data.Services.CachedData
{
    public interface ICachedDataService
    {
        int RegistrationYear { get; set; }

        Task PreloadCachedDataAsync(DataContext context);
    }
}