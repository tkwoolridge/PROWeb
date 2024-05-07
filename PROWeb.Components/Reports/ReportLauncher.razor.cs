using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using PROWeb.Common.Components;
using PROWeb.Components.Reports.Configuration;
using PROWeb.Components.Reports.Extensions;
using PROWeb.Components.Reports.Filters;
using PROWeb.Components.Reports.Models;
using PROWeb.Data.Models.Reports;
using System.Collections;
using Telerik.DataSource;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace PROWeb.Components.Reports
{
    public partial class ReportLauncher : PROContentLayout
    {
        [Inject]
        private IReportsService _reportsService { get; set; } = null!;

        [Inject]
        protected IWebHostEnvironment _environment { get; set; } = null!;

        [Inject]
        private IJSRuntime _js { get; set; } = null!;

        [Parameter]
        public string? Connection { get; set; }

        [Parameter]
        public string ReportsFolder { get; set; } = "/";


        protected ReportViewerDialog? ReportViewerDialogRef { get; set; }

        protected ReportFilterView? ReportFilterRef { get; set; }

        protected int? SelectedReportId { get; set; }

        protected CompositeFilterDescriptor? SelectedFilter { get; set; }

        public List<ReportInfo>? Reports { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ReportsConfig? config = _reportsService.GetReports();

            Reports = config?.Reports;

            SelectedReportId = 1;
            UpdateReportFilters(SelectedReportId);
        }

        public void OnReportChanged(int? reportId)
        {
            SelectedReportId = reportId;
            UpdateReportFilters(reportId);
        }

        public ReportViewerDialog? FilterDialogRef { get; set; }


        private ReportDescriptor GetReportDescriptor()
        {
            var report = Reports?.FirstOrDefault(r => r.Id == SelectedReportId);
            var filters = SelectedFilter.Adapt<ReportFilterGroup>();

            string reportsRoot = Path.Combine(_environment.WebRootPath, ReportsFolder);
            string reportPath = Path.Combine(reportsRoot, report?.FileName!);

            return new ReportDescriptor
            {
                Name = report?.Name,
                Filters = filters,
                Connection = Connection,
                ReportPath = reportPath
            };
        }

        public void OnRunReport()
        {
            ReportDescriptor descrptor = GetReportDescriptor();

            ReportViewerDialogRef?.Show(descrptor);
        }

        public async Task OnExportToPDFAsync()
        {
            await ExportReportAsync("PDF");
        }

        public async Task OnExportToCSVAsync()
        {
            await ExportReportAsync("CSV");
        }

        public void OnResetFilter()
        {
            UpdateReportFilters(SelectedReportId);
        }

        private async Task ExportReportAsync(string format)
        {
            SetBusyState(true, $"Exporting to {format}. Please wait...");

            await Task.Delay(500);

            var reportPackager = new ReportPackager();
            var descriptor = GetReportDescriptor();

            string reportPath = descriptor.ReportPath ?? "/";

            Telerik.Reporting.Report reportInstance;

            using (var reportStream = File.OpenRead(reportPath))
            {
                reportInstance = reportPackager.Unpackage(reportStream);
            }

            if (reportInstance.DataSource is SqlDataSource source)
            {
                source.SetDataSource(descriptor);
                reportInstance.DataSource = source;
            }

            ReportProcessor reportProcessor = new ReportProcessor();

            InstanceReportSource instanceReportSource = new InstanceReportSource
            {
                ReportDocument = reportInstance
            };

            Hashtable deviceInfo = new Hashtable();

            if (format.Equals("CSV"))
            {
                deviceInfo.Add("NoStaticText", true);
            }

            RenderingResult result = reportProcessor.RenderReport(format, instanceReportSource, deviceInfo);

            await DownloadFileFromStreamAsync(result.DocumentBytes, $"{descriptor.Name}.{format.ToLower()}");

            SetBusyState(false);
        }

        private void UpdateReportFilters(int? reportId)
        {
            if (Reports?.FirstOrDefault(r => r.Id == reportId) is { } report)
            {
                var filter = report.Filters.Adapt<CompositeFilterDescriptor>();
                ReportFilterRef?.ResetFilterToDefault(filter);

                SelectedFilter = filter;
            }
        }

        private async Task DownloadFileFromStreamAsync(byte[] content, string fileName)
        {
            using var streamRef = new DotNetStreamReference(stream: new MemoryStream(content));

            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
