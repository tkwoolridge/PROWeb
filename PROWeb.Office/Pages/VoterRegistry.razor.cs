using Microsoft.AspNetCore.Components;
using PROWeb.Components.Layouts;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Pages
{
    public partial class VoterRegistry : PRORegistryLayout<FilterModel, VoterViewModel>
    {
        private bool _gridSelected;

        private bool _simpleFilterSelected;

        private IList<VoterViewModel>? _data;

        public int FemaleCount { get; private set; }

        public int MaleCount { get; private set; }
        
        public int NoneCount { get; private set; }

        public int Total { get; private set; }

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

        private void OnListDataChanged(object? sender, IList<VoterViewModel>? data)
        {
            _data = data;

            FemaleCount = _data?.Count(v => v.Gender == 'F') ?? 0;
            MaleCount = _data?.Count(v => v.Gender == 'M') ?? 0;
            NoneCount = _data?.Count(v => v.Gender == 'N') ?? 0;
            Total = _data?.Count ?? 0;
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
