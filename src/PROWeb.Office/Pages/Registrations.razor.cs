using Microsoft.AspNetCore.Components;
using PROWeb.Components.Layouts;
using PROWeb.Data.Models.Enums;
using PROWeb.Office.Shared.Registrations.Filters;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Pages
{
    public partial class Registrations : PRORegistryLayout<RegistrationFilterModel, RegistrationViewModel>
    {
        private IList<RegistrationViewModel>? _data;

        public int ApprovedForm1Count { get; private set; }

        public int PendingForm1Count { get; private set; }

        public int Form1Total { get; private set; }

        public int ApprovedForm2Count { get; private set; }

        public int PendingForm2Count { get; private set; }

        public int Form2Total { get; private set; }

        public int Total { get; private set; }

        private int _page;

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

            ApprovedForm1Count = _data?.Count(r => r.RegistrationStatusId == (int)RegistrationStatuses.Approved && r.FormTypeId == (int)FormTypes.Form1) ?? 0;
            PendingForm1Count = _data?.Count(r => r.RegistrationStatusId == (int)RegistrationStatuses.Pending && r.FormTypeId == (int)FormTypes.Form1) ?? 0;
            Form1Total = _data?.Count(r => r.FormTypeId == (int)FormTypes.Form1) ?? 0;

            ApprovedForm2Count = _data?.Count(r => r.RegistrationStatusId == (int)RegistrationStatuses.Approved && r.FormTypeId == (int)FormTypes.Form2) ?? 0; ;
            PendingForm2Count = _data?.Count(r => r.RegistrationStatusId == (int)RegistrationStatuses.Pending && r.FormTypeId == (int)FormTypes.Form2) ?? 0;
            Form2Total = _data?.Count(r => r.FormTypeId == (int)FormTypes.Form2) ?? 0;

            Total = data?.Count ?? 0;
        }

        protected override string PageTitle => "Registrations";
    }
}
