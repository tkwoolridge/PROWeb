using PROWeb.WebService.Models;
using PROWeb.WebService.Resources;

namespace PROWeb.WebService.Repositories
{
    public interface IUsersRepository
    {
        ServiceUser? FindUser(string? userName);
    }
}