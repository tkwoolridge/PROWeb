using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Navigation;

namespace PROWeb.Components.Navigation
{
    public partial class Navigation : PROComponent
    {
        [Parameter]
        public IList<MenuItemViewModel>? Items { get; set; }

        [Parameter]
        public string? Page { get; set; }

        [Parameter]
        public MenuItemViewModel? SelectedItem { get; set; }

        [Parameter]
        public EventCallback<MenuItemViewModel> SelectedItemChanged { get; set; }

        private List<MenuItemViewModel> DrawerSource { get; set; } = Enumerable.Empty<MenuItemViewModel>().ToList();

        protected override void OnInitialized()
        {
            if (Items == null)
            {
                return;
            }

            SelectedItem ??= SelectCurrentPage(Items);
            DrawerSource = new List<MenuItemViewModel>(GetNavigationItems(Items));
        }

        private MenuItemViewModel? SelectCurrentPage(IList<MenuItemViewModel>? items)
        {
            if(items == null)
            {
                return null;
            }

            bool IsCurrentPage(MenuItemViewModel item)
            {
                return item.Page?.Equals(Page, StringComparison.CurrentCultureIgnoreCase) ?? false;
            }

            MenuItemViewModel?IterateItem (MenuItemViewModel item)
            {
                if (IsCurrentPage(item))
                {
                    item.IsExpanded = true;

                    return item;
                }

                if (item.MenuItems.Count > 0)
                {
                    foreach (var child in item.MenuItems)
                    {
                        if (IsCurrentPage(child))
                        {
                            item.IsExpanded = true;

                            return child;
                        }
                        else if (IterateItem(child) is { } current)
                        {
                            item.IsExpanded = true;

                            return current;
                        }
                    }
                }

                return null;
            }

            foreach(var item in items)
            {
                if(IterateItem(item) is { } current)
                {
                    return current;
                }
            }

            return null;
        }

        private void OnItemExpand(MenuItemViewModel item)
        {
            if (item.IsExpanded)
            {
                ItemColapse(item);
            }
            else
            {
                ItemExpand(item);

            }

            item.IsExpanded = !item.IsExpanded;
        }

        private void ItemExpand(MenuItemViewModel item)
        {
            int startIndex = DrawerSource.IndexOf(item);

            DrawerSource.InsertRange(startIndex + 1, GetNavigationItems(item.MenuItems, item.Level));
        }

        private async Task OnItemSelected(MenuItemViewModel item)
        {
            if(!string.IsNullOrWhiteSpace(item.Page))
            {
                SelectedItem = item;
                await SelectedItemChanged.InvokeAsync(SelectedItem);
            }
            else if(item.MenuItems.Any())
            {
                OnItemExpand(item);
            }
        }

        private void ItemColapse(MenuItemViewModel item)
        {
            int startIndex = DrawerSource.IndexOf(item);

            DrawerSource.RemoveRange(startIndex + 1, GetNavigationItemsCount(item));
        }

        private int GetNavigationItemsCount(MenuItemViewModel item)
        {
            int CountItems(MenuItemViewModel item)
            {
                if (!item.IsExpanded)
                {
                    return 0;
                }

                var items = item.MenuItems;
                int count = items.Count;

                foreach (var child in items)
                {
                    count += CountItems(child);
                }

                return count;
            }

            var items = item.MenuItems;
            int counter = items.Count;

            foreach (var child in items)
            {
                counter += CountItems(child);
            }

            return counter;
        }

        private IEnumerable<MenuItemViewModel> GetNavigationItems(IList<MenuItemViewModel> items, int level = 0)
        {
            IEnumerable<MenuItemViewModel> IterateMenu(MenuItemViewModel root, int level)
            {
                root.Level= level;

                yield return root;

                if (root.IsExpanded && root.MenuItems is { Count: > 0 } items)
                {
                    level++;

                    for (int i = 0; i < items.Count; i++)
                    {
                        var item = items[i];

                        foreach (var child in IterateMenu(item, level))
                        {
                            yield return child;
                        }
                    }
                }
            }

            level++;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                foreach (var child in IterateMenu(item, level))
                {
                    yield return child;
                }
            }
        }
    }
}
