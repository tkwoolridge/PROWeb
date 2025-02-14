namespace PROWeb.WebService.Authentication
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}