using PROWeb.RestApi.Authentication.Models;

namespace PROWeb.WebService.Repositories
{
    public interface IUsersRepository
    {
        ServiceUser? FindUser(string? userName);
    }
}