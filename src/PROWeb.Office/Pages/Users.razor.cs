using Microsoft.AspNetCore.Components;
using PROWeb.Authentication.Components.Users.ViewModels;
using PROWeb.Components.Layouts;
using PROWeb.Office.Shared.Users.ViewModels;

namespace PROWeb.Office.Pages
{
    public partial class Users : PRORegistryLayout<UserFilterViewModel, UserViewModel>
    {
        private bool _gridSelected;

        protected RenderFragment? ListTemplate;

        private int _page;

        private IList<UserViewModel>? _data;

        private bool GridSelected
        {
            get => _gridSelected;
            set
            {
                _gridSelected = value;

                ListTemplate = value ? Grid : List;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            GridSelected = true;
        }

        protected override string PageTitle => "Users";

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            ListRef?.SetData(_data, _page);

            if (ListRef is { } list)
            {
                list.DataChanged -= OnListDataChanged;
                list.PageChanged -= OnListPageChanged;
                list.DataChanged += OnListDataChanged;
                list.PageChanged += OnListPageChanged;
            }
        }

        private void OnListPageChanged(object? sender, int page)
        {
            _page = page;
        }

        private void OnListDataChanged(object? sender, IList<UserViewModel>? data)
        {
            _data = data;
        }
    }
}
