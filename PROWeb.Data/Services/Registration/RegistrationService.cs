using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Extensions;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Registration
{
    #region Service Factory

    public class RegistrationServiceFactory : DbContextServiceFactory<RegistrationService, DataContext>, IRegistrationServiceFactory
    {
        public RegistrationServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override RegistrationService CreateService()
        {
            return new RegistrationService(ContextFactory);
        }
    }

    #endregion

    public class RegistrationService : DbContextService<DataContext>, IRegistrationService
    {
        public RegistrationService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public IQueryable<FormType> GetFormTypes()
        {
            return Context.FormTypes;
        }
    }
}
