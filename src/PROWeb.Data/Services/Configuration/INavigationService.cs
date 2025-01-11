using PROWeb.Data.Models.Navigation;

namespace PROWeb.Data.Services.Configuration
{
    public interface INavigationService : IConfigServiceBase
    {
        Menu? GetMenu();
    }
}