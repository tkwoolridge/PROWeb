using PROWeb.Data.Models;
using PROWeb.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace PROWeb.Data.Services.Logging
{
    #region Service Factory

    public class ActivityLogServiceFactory : DbContextServiceFactory<ActivityLogService, DataContext>, IActivityLogServiceFactory
    {
        public ActivityLogServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override ActivityLogService CreateService()
        {
            return new ActivityLogService(ContextFactory);
        }
    }

    #endregion Service Factory

    public class ActivityLogService : DbContextService<DataContext>, IActivityLogService
    {
        #region Base

        public ActivityLogService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        #endregion

        public IQueryable<ActivityLog> GetActivityLogs(int? logTypeId, DateTime? from, DateTime? to)
        {
            var query = Context.ActivityLogs
                .Where(l => l.LogDate >= from && l.LogDate <= to);

            query = query.WhereIfNotNull(logTypeId, l => l.TypeId == logTypeId);

            return query;
        }

        public async Task LogUserActivity(PROUser user, string activityDescription)
        {
            ActivityLog activityLog = new ActivityLog()
            {
                Description = activityDescription,
                UserName = user.UserName,
                TypeId = (int)ActivityLogTypes.UserActivity,
                LogDate = DateTime.Now
            };

            Context.ActivityLogs.Add(activityLog);
            await Context.SaveChangesAsync();
        }
    }
}
