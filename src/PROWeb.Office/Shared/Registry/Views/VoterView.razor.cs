using PROWeb.Components.Voters.ViewModels;
using PROWeb.Components.Voters;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Reports.Configuration;
using PROWeb.Data.Models.Reports;

namespace PROWeb.Office.Shared.Registry.Views
{
    public partial class VoterView : VoterViewBase<VoterViewModel>
    {
        [Inject]
        protected IReportsService ReportsService { get; set; } = null!;

        protected VoterHistoryDialog? VoterHistoryDialogRef { get; set; }

        protected CertificatesDialog? @CertificatesDialogRef { get; set; }

        protected string PersistenceKey => LayoutRef?.Model?.VoterId.ToString() ?? "0";

        public List<ReportInfo>? Certificates { get; private set; }

        public int? SelectedCertificateId { get; set; } = 1;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Certificates = ReportsService.GetCertificates()?.Reports;
        }

        public void OnEdit()
        {
            ResetUndo();

            Editable = !Editable;
        }

        protected override async Task SaveAsync(VoterViewModel model)
        {
            await base.SaveAsync(model);

            Editable = false;
        }

        public void OnShowHistory()
        {
            VoterHistoryDialogRef?.Show();
        }

        public void OnRunCertificates()
        {
            if (Certificates?.First(r => r.Id == SelectedCertificateId) is { } certificate && Model is { } model)
            {
                CertificatesDialogRef?.Show(certificate, model);
            }
        }

    }
}
