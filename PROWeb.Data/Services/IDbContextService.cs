using Microsoft.EntityFrameworkCore;

namespace PROWeb.Data.Services
{
    public interface IDbContextService<TDataContext> : IDisposable where TDataContext : DbContext
    {
        Task SaveChangesToDatabaseAsync();
    }
}