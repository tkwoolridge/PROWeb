using Microsoft.AspNetCore.Hosting;
using PROWeb.Data.Models.Navigation;

namespace PROWeb.Data.Services.Configuration
{
    public class NavigationService : ConfigServiceBase<Menu>, INavigationService
    {
        public NavigationService(IWebHostEnvironment environment) : base(environment, "/navigation")
        {
        }

        public Menu? GetMenu()
        {
            return GetConfigByName("menu");
        }
    }
}
