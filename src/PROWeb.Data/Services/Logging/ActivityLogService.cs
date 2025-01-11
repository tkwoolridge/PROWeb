using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;
using PROWeb.Data.Extensions;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Logging
{
    #region Service Factory

    public class ActivityLogServiceFactory : DataContextServiceFactory<IActivityLogService>, IActivityLogServiceFactory
    {
        public ActivityLogServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override IActivityLogService CreateService()
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

        public async Task LogUserActivity(string userName, string activityDescription)
        {
            ActivityLog activityLog = new ActivityLog()
            {
                Description = activityDescription,
                UserName = userName,
                TypeId = (int)ActivityLogTypes.UserActivity,
                LogDate = DateTime.Now
            };

            Context.ActivityLogs.Add(activityLog);
            await Context.SaveChangesAsync();
        }
    }
}
