using Microsoft.EntityFrameworkCore;

namespace PROWeb.Data.Services
{
    public abstract class DbContextServiceFactory<TService, TDataContext> : IDbContextServiceFactory<TService, TDataContext>
        where TService : IDbContextService<TDataContext>
        where TDataContext : DbContext
    {
        protected readonly IDbContextFactory<TDataContext> ContextFactory = null!;

        public DbContextServiceFactory(IDbContextFactory<TDataContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public abstract TService CreateService();
    }
}
