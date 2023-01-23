using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Navigation
{
    public class MenuViewModel : SlimViewModelBase
    {
        public List<MenuItemViewModel> MenuItems { get; set; } = null!;
    }
}
