using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Logging
{
    public interface IActivityLogService
    {
        IQueryable<ActivityLog> GetActivityLogs(int? logTypeId, DateTime? from, DateTime? to);
        Task LogUserActivity(PROUser user, string activityDescription);
    }
}