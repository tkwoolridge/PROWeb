using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Navigation.ViewModels
{
    public class MenuItemViewModel : SlimViewModelBase
    {
        public List<MenuItemViewModel> MenuItems { get; set; } = null!;

        public SecurityRequirement? Requirement { get; set; }

        public int Level { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Page { get; set; }

        public bool IsExpanded { get; set; }
    }
}
