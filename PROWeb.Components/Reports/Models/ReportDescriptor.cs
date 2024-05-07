using PROWeb.Data.Models.Reports;

namespace PROWeb.Components.Reports.Models
{
    public class ReportDescriptor
    {
        public string? Name { get; set; }

        public string? ReportPath { get; set; }

        public string? Connection { get; set; }

        public ReportFilterGroup Filters { get; set; } = new ReportFilterGroup();
    }
}
