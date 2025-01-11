using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Navigation.ViewModels
{
    public class MenuViewModel : SlimViewModelBase
    {
        public List<MenuItemViewModel> MenuItems { get; set; } = null!;
    }
}
