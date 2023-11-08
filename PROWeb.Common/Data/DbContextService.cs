using Microsoft.EntityFrameworkCore;

namespace PROWeb.Common.Data
{
    public class DbContextService<TDataContext> : IDbContextService<TDataContext> where TDataContext : DbContext
    {
        private IDbContextFactory<TDataContext> _contextFactory;

        private TDataContext? _context;

        protected TDataContext Context => _context ??= _contextFactory.CreateDbContext();

        public DbContextService(IDbContextFactory<TDataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task SaveChangesToDatabaseAsync()
        {
            if (Context is { } context)
            {
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
