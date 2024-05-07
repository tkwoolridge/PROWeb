using PROWeb.Data.Models.Reports;
using PROWeb.Data.Services.Configuration;

namespace PROWeb.Components.Reports.Configuration
{
    public interface IReportsService : IConfigServiceBase
    {
        ReportsConfig? GetReports();

        ReportsConfig? GetCertificates();
    }
}
