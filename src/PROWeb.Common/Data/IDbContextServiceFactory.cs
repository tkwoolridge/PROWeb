using Microsoft.EntityFrameworkCore;

namespace PROWeb.Common.Data
{
    public interface IDbContextServiceFactory<TService, TDataContext>
        where TService : IDbContextService<TDataContext>
        where TDataContext : DbContext
    {
        TService CreateService();
    }
}