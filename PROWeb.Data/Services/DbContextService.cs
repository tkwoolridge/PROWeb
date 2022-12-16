using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Data.Services
{
    public class DbContextService<TDataContext> : IDisposable where TDataContext : DbContext
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
