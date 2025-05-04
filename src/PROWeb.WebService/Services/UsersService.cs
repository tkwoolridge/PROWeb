using PROWeb.RestApi.Authentication.Models;
using PROWeb.WebService.Repositories;

namespace PROWeb.WebService.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public ServiceUser? FindByUserName(string? userName)
        {
            return _usersRepository.FindUser(userName);
        }
    }
}
