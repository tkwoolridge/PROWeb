using PROWeb.Common.Data;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Data.Authentication.Services.Users
{
    public interface IUsersService : IDbContextService<IdentityDataContext>
    {
        Task<PROUser?> GetCurrentUser();
    }
}