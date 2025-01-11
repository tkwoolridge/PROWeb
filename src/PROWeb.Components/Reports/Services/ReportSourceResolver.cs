using PROWeb.Components.Reports.Extensions;
using PROWeb.Components.Reports.Models;
using Serilog;
using System.Diagnostics;
using System.Text.Json;
using Telerik.Reporting;
using Telerik.Reporting.Services;

namespace PROWeb.Components.Reports.Services
{
    public class ReportSourceResolver : IReportSourceResolver
    {
        private readonly ILogger _logger;
        private Report? _reportInstance;
        private ReportSource? _reportSourceInstance;

        public ReportSourceResolver(ILogger logger)
        {
            _logger = logger;
        }

        public ReportSource? Resolve(string report, OperationOrigin operationOrigin, IDictionary<string, object> currentParameterValues)
        {
            ReportDescriptor? descripter = JsonSerializer.Deserialize<ReportDescriptor>(report);

            Debug.Assert(descripter != null);

            string reportPath = descripter.ReportPath ?? "/";

            var reportPackager = new ReportPackager();

            try
            {
                if (operationOrigin == OperationOrigin.ResolveReportParameters)
                {
                    using (var reportStream = File.OpenRead(reportPath))
                    {
                        _reportInstance = reportPackager.Unpackage(reportStream);
                    }

                    _reportSourceInstance = new InstanceReportSource()
                    {
                        ReportDocument = _reportInstance,
                    };
                }

                if (operationOrigin == OperationOrigin.GenerateReportDocument &&
                    _reportInstance?.DataSource is SqlDataSource dataSource)
                {
                    dataSource.SetDataSource(descripter);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Report error");

                return null;
            }

            return _reportSourceInstance;
        }


    }
}
