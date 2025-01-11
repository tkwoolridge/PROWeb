namespace PROWeb.WebSiteService.Authentication
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}