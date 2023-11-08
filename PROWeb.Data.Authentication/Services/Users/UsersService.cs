using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Data.Authentication.Services.Users
{
    #region Service Factory

    public class UsersServiceFactory : DbContextServiceFactory<IUsersService, IdentityDataContext>, IUsersServiceFactory
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UsersServiceFactory(AuthenticationStateProvider authenticationStateProvider, IDbContextFactory<IdentityDataContext> contextFactory) : base(contextFactory)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public override IUsersService CreateService()
        {
            return new UsersService(_authenticationStateProvider, ContextFactory);
        }
    }

    #endregion Service Factory


    public class UsersService : DbContextService<IdentityDataContext>, IUsersService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UsersService(AuthenticationStateProvider authenticationStateProvider, IDbContextFactory<IdentityDataContext> contextFactory) : base(contextFactory)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<PROUser?> GetCurrentUser()
        {
            var provider = await _authenticationStateProvider.GetAuthenticationStateAsync();

            if (provider?.User?.Identity?.Name is { } name)
            {
                return await Context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            }
            else
            {
                return null;
            }
        }
    }
}
