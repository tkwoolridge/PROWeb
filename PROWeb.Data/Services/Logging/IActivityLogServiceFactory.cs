namespace PROWeb.Data.Services.Logging
{
    public interface IActivityLogServiceFactory
    {
        ActivityLogService CreateService();
    }
}