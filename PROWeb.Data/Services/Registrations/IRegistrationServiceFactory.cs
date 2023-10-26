using PROWeb.Data.Services.Voters;

namespace PROWeb.Data.Services.Registrations
{
    public interface IRegistrationServiceFactory : IDbContextServiceFactory<IRegistrationService, DataContext>
    {
    }
}