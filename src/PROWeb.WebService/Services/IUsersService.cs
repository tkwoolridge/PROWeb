using PROWeb.WebService.Models;

namespace PROWeb.WebService.Services
{
    public interface IUsersService
    {
        ServiceUser? FindByUserName(string? userName);
    }
}