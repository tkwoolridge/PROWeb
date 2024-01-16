using PROWeb.Common.Data;
using PROWeb.Data.Authentication.Models;
using PROWeb.Data.Authentication.Models.Views;

namespace PROWeb.Data.Authentication.Services.Users
{
    public interface IUsersService : IDbContextService<IdentityDataContext>
    {
        Task<PROUser?> GetCurrentUser();

        IQueryable<PROUserView> GetUsers(
            string? userName = null,
            string? firstName = null,
            string? lastName = null);

        IQueryable<PRORole> GetRoles();

        Task UpdateUser(PROUserView userView);
    }
}