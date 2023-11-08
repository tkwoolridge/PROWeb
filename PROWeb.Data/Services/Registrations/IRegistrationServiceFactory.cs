using PROWeb.Common.Data;

namespace PROWeb.Data.Services.Registrations
{
    public interface IRegistrationServiceFactory : IDbContextServiceFactory<IRegistrationService, DataContext>
    {
    }
}