using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;

namespace PROWeb.Data.Services
{
    public abstract class DataContextServiceFactory<TService> : DbContextServiceFactory<TService, DataContext> where TService : IDbContextService<DataContext>
    {
        protected DataContextServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public abstract override TService CreateService();
    }
}
