using Microsoft.EntityFrameworkCore;

namespace PROWeb.Data.Services
{
    public interface IDbContextServiceFactory<TService, TDataContext>
        where TService : IDbContextService<TDataContext>
        where TDataContext : DbContext
    {
        TService CreateService();
    }
}