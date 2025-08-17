using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Office
{
    #region Service Factory

    public class OfficeServiceFactory : DataContextServiceFactory<IOfficeService>, IOfficeServiceFactory
    {
        public OfficeServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override IOfficeService CreateService()
        {
            return new OfficeService(ContextFactory);
        }
    }

    #endregion

    public class OfficeService : DbContextService<DataContext>, IOfficeService
    {
        public OfficeService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<PROOffice?> GetOfficeAsync()
        {
            return await Context.PROOffices.OrderBy(o => o.Id).FirstOrDefaultAsync();
        }

        public IQueryable<ElectionType> GetElectionTypes()
        {
            return Context.ElectionTypes;
        }

        public async Task SaveOfficeAsync(PROOffice office)
        {
            Context.PROOffices.Attach(office);
            Context.PROOffices.Update(office);
            await Context.SaveChangesAsync();
        }
    }
}
