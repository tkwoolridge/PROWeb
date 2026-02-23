namespace PROWeb.RestApi.Authentication
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}