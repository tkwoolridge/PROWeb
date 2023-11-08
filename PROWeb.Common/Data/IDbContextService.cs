using Microsoft.EntityFrameworkCore;

namespace PROWeb.Common.Data
{
    public interface IDbContextService<TDataContext> : IDisposable where TDataContext : DbContext
    {
        Task SaveChangesToDatabaseAsync();
    }
}