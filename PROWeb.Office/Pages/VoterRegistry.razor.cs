using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Filters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Pages
{
    public partial class VoterRegistry : PRORegistryLayout<FilterModel, ListVoterViewModel>
    {
        private bool _gridSelected;

        private bool _simpleFilterSelected;

        private IList<ListVoterViewModel>? _data;
        private int _page;

        private bool GridSelected
        {
            get => _gridSelected;
            set
            {
                _gridSelected = value;

                ListTemplate = value ? Grid : List;
            }
        }

        private bool SimpleFilterSelected
        {
            get => _simpleFilterSelected;
            set
            {
                _simpleFilterSelected = value;

                FilterTemplate = value ? SimpleFilterTemplate : AdvancedFilterTemplate;
            }
        }

        protected RenderFragment? FilterTemplate;

        protected RenderFragment? ListTemplate;

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

        private void OnListDataChanged(object? sender, IList<ListVoterViewModel>? data)
        {
            _data = data;
        }

        protected override string PageTitle => "Registry";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SimpleFilterSelected = true;
            GridSelected = true;
        }
    }
}
