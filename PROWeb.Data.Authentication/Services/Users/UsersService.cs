using Mapster;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;
using PROWeb.Data.Authentication.Models;
using PROWeb.Data.Authentication.Models.Views;
using PROWeb.Data.Extensions;
using System.Reactive.Linq;

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

    #endregion

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

        public IQueryable<PRORole> GetRoles()
        {
            return Context.Roles;
        }

        public IQueryable<PROUserView> GetUsers(
            string? userName = null,
            string? firstName = null,
            string? lastName = null)
        {
#nullable disable
            var users = Context.Users.Join(
                Context.UserRoles.Join(Context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => 
                new 
                {
                    ur.UserId,
                    ur.RoleId, 
                    r.Description 
                })
                , u => u.Id, r => r.UserId, (u,ur) => new PROUserView()
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    RoleId = ur.RoleId,
                    RoleDescription = ur.Description,
                    IsActive = u.IsActive
                })
                .WhereIfNotNull(userName, u => u.UserName.StartsWith(userName))
                .WhereIfNotNull(firstName, u => u.FirstName.StartsWith(firstName))
                .WhereIfNotNull(lastName, u => u.LastName.StartsWith(lastName));
#nullable enable
            return users;
        }

        public async Task UpdateUser(PROUserView userView)
        {
            var user = Context.Users.First(u => u.Id == userView.Id);

            userView.Adapt(user);

            Context.Users.Update(user);

            if(userView.RoleId is { } roleId)
            {
                Context.UserRoles.RemoveRange(Context.UserRoles.Where(ur => ur.UserId == userView.Id).ToArray());
                Context.UserRoles.Add(new IdentityUserRole<int>
                {
                    UserId = userView.Id,
                    RoleId = roleId
                });
            }

            await Context.SaveChangesAsync();
        }
    }
}
