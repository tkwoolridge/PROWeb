using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Navigation.ViewModels;
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

        private string? Page { get; set; }

        TelerikDrawer<MenuItemViewModel>? DrawerRef { get; set; }

        List<MenuItemViewModel>? NavigablePages { get; set; }

        protected bool IsDrawerHidden = false;

        protected override void OnInitialized()
        {
            MenuViewModel? menu = _navigationService.GetMenu()?.Adapt<MenuViewModel>();

            NavigablePages = menu?.MenuItems;

            SetCurrentPage();

            base.OnInitialized();
        }

        public void SetCurrentPage()
        {
            Page = "/" + string.Concat(_navigationManager.Uri.Split("//")[1].Split("/").Skip(1));
        }

        private bool IsAuthenticationPage(string page)
        {
            return page.Contains("Account", StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task OnNavigate(MenuItemViewModel item)
        {
            if (item.Page is { } page && DrawerRef is { } drawer)
            {
                if (!Uri.IsWellFormedUriString(page, UriKind.Relative))
                {
                    page = navigationManager.ToBaseRelativePath(page);
                }

                var returnUrl = new Uri(_navigationManager.Uri).LocalPath;

                page = page + $"?returnUrl={Uri.EscapeDataString(returnUrl)}";

                _navigationManager.NavigateTo(page, IsAuthenticationPage(page));
                await DrawerRef.ToggleAsync();

                SetCurrentPage();
            }
        }
    }
}
