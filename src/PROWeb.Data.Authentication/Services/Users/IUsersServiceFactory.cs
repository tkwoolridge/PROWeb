using PROWeb.Common.Data;

namespace PROWeb.Data.Authentication.Services.Users
{
    public interface IUsersServiceFactory : IDbContextServiceFactory<IUsersService, IdentityDataContext>
    {
    }
}
