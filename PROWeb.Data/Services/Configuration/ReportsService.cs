using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using PROWeb.Components.Reports.Configuration;
using PROWeb.Data.Models.Reports;

namespace PROWeb.Data.Services.Configuration
{
    public class ReportsService : ConfigServiceBase<ReportsConfig>, IReportsService
    {
        public ReportsService(IWebHostEnvironment environment) : base(environment, "/reports/configuration")
        {
        }

        public ReportsConfig? GetReports()
        {
            return GetConfigByName("reports");
        }

        public ReportsConfig? GetCertificates()
        {
            return GetConfigByName("certificates");
        }

        protected override bool SkipFile(IFileInfo fileInfo)
        {
            if (Path.GetExtension(fileInfo.PhysicalPath) is { } extension && extension.Equals(".XML", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}
