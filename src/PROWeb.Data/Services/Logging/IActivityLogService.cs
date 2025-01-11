using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Logging
{
    public interface IActivityLogService : IDbContextService<DataContext>
    {
        IQueryable<ActivityLog> GetActivityLogs(int? logTypeId, DateTime? from, DateTime? to);

        Task LogUserActivity(string userName, string activityDescription);
    }
}