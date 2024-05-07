using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Common.Layouts;
using PROWeb.Data.Models.Reports;
using PROWeb.Data.Services.CachedData;
using PROWeb.Office.Shared.Registry.ViewModels;
using Telerik.Reporting.Processing;
using Telerik.Reporting;
using Microsoft.JSInterop;
using PROWeb.Components.Voters.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public partial class CertificatesDialog : PROComponent
    {
        private int? _certificationDocumentId;

        private ReportInfo? _report;
        private VoterViewModel? _voter;

        [Inject]
        protected ICachedDataService CachedDataService { get; set; } = null!;

        [Inject]
        private IJSRuntime _js { get; set; } = null!;

        [Inject]
        protected IWebHostEnvironment _environment { get; set; } = null!;

        [Parameter]
        public string ReportsFolder { get; set; } = "/";

        protected string? Title { get; set; }

        protected List<string>? Signatories { get; set; }

        protected List<CertificationDocumentViewModel>? CertificationDocuments { get; set; }

        protected int? CertificationDocumentId 
        { 
            get => _certificationDocumentId;
            set
            {
                _certificationDocumentId = value;
                OnCertificationDocumenChanged(_certificationDocumentId);
            }  
        }

        protected string? Signatory { get; set; }

        protected DialogLayout? WindowRef { get; set; }
        
        protected bool ShowTitle { get; private set; }
        
        protected bool ShowCertificates { get; private set; }
        
        protected bool ShowSignatory { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            CertificationDocuments = CachedDataService.CertificationDocuments.Adapt<List<CertificationDocumentViewModel>>();
            CertificationDocumentId = 1;
            Signatories = CachedDataService.Signatories;
            Signatory = CachedDataService.Signatories.FirstOrDefault();
        }

        protected async Task OnConfirm()
        {
            if(_report is { } report)
            {
                await ExportReportAsync(report, "PDF");
            }
        }

        private string GetReportsPath(ReportInfo report)
        {
            string reportsRoot = Path.Combine(_environment.WebRootPath, ReportsFolder);
            return Path.Combine(reportsRoot, report?.FileName!);
        }

        private async Task ExportReportAsync(ReportInfo report, string format)
        {
            var reportPackager = new ReportPackager();

            string reportPath = GetReportsPath(report) ?? "/";

            Telerik.Reporting.Report reportInstance;

            using (var reportStream = File.OpenRead(reportPath))
            {
                reportInstance = reportPackager.Unpackage(reportStream);
            }

            foreach(var parameter in report.Parameters)
            {
                if (reportInstance.ReportParameters[parameter.Name] is { } rParameter)
                {
                    rParameter.Value = GetParameterValue(parameter.Name);
                }
                else
                {
                    reportInstance.ReportParameters.Add(new Telerik.Reporting.ReportParameter(parameter.Name, parameter.Type, GetParameterValue(parameter.Name)));
                }
            }

            ReportProcessor reportProcessor = new ReportProcessor();

            InstanceReportSource instanceReportSource = new InstanceReportSource
            {
                ReportDocument = reportInstance
            };

            try
            {
                RenderingResult result = reportProcessor.RenderReport(format.ToUpper(), instanceReportSource, null);

                await DownloadFileFromStreamAsync(result.DocumentBytes, $"{report.Name}.{format.ToLower()}");
            }
            catch(Exception exception)
            {
                var s = exception.Message;
            }
        }

        private object? GetParameterValue(string? name)
        {
            return name switch
            {
                "CertificationDocumentId" => CertificationDocumentId,
                "VoterId" => _voter?.VoterId,
                "Signatory" => Signatory,
                "Title" => Title,
                _ => null
            };
        }

        private async Task DownloadFileFromStreamAsync(byte[] content, string fileName)
        {
            using var streamRef = new DotNetStreamReference(stream: new MemoryStream(content));

            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }

        private void OnCertificationDocumenChanged(int? certificationDocumentId)
        {
            Title = CertificationDocuments?.First(c => c.Id == certificationDocumentId).Name;
        }

        public void Show(ReportInfo report, VoterViewModel voter)
        {
            ShowTitle =
            ShowCertificates =
            ShowSignatory = false;
            Title = null;

            _report = report;
            _voter = voter;

            foreach (var parameter in report.Parameters)
            {
                switch(parameter.Name)
                {
                    case "Title":
                        {
                            ShowTitle = true;
                            break;
                        }
                    case "CertificationDocumentId":
                        {
                            ShowCertificates = true;
                            OnCertificationDocumenChanged(CertificationDocumentId);
                            break;
                        }
                    case "Signatory":
                        {
                            ShowSignatory = true;
                            break;
                        }
                }
            }

            WindowRef?.Show();
        }
    }
}
