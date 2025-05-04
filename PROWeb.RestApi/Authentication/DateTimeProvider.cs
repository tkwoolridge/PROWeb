using PROWeb.RestApi.Authentication;

namespace PROWeb.WebService.Authentication
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
