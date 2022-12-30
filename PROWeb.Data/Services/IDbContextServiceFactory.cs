using Microsoft.EntityFrameworkCore;

namespace PROWeb.Data.Services
{
    public interface IDbContextServiceFactory<TService, TDataContext>
        where TService : DbContextService<TDataContext>
        where TDataContext : DbContext
    {
        TService CreateService();
    }
}