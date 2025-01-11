namespace PROWeb.Data.Services.Logging
{
    public interface IActivityLogServiceFactory
    {
        IActivityLogService CreateService();
    }
}