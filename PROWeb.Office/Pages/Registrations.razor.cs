using Microsoft.AspNetCore.Components;
using PROWeb.Components.Layouts;
using PROWeb.Office.Shared.Registrations.Filters;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Pages
{
    public partial class Registrations : PRORegistryLayout<RegistrationFilterModel, RegistrationViewModel>
    {
        private bool _gridSelected;

        private IList<RegistrationViewModel>? _data;
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

        private void OnListDataChanged(object? sender, IList<RegistrationViewModel>? data)
        {
            _data = data;
        }

        protected override string PageTitle => "Registrations";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            GridSelected = true;
        }
    }
}
