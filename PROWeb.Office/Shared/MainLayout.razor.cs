using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Mapping;
using PROWeb.Components.ViewModels.Navigation;
using PROWeb.Data.Services.Configuration;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared
{
    public partial class MainLayout : PROLayout
    {
        [Inject]
        private INavigationService _navigationService { get; set; } = null!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        private string Page { get; set; }

        TelerikDrawer<MenuItemViewModel> DrawerRef { get; set; }

        List<MenuItemViewModel> NavigablePages { get; set; }

        protected override void OnInitialized()
        {
            var menu = _navigationService.GetMenu()?.Project<MenuViewModel>();

            NavigablePages = menu.MenuItems;

            SetCurrentPage();

            base.OnInitialized();
        }

        public void SetCurrentPage()
        {
            Page = "/" + string.Concat(_navigationManager.Uri.Split("//")[1].Split("/").Skip(1));
        }

        private async Task OnNavigate(MenuItemViewModel item)
        {
            _navigationManager.NavigateTo(item.Page);
            await DrawerRef.ToggleAsync();

            SetCurrentPage();
        }
    }
}
