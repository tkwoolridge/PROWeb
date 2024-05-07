using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace PROWeb.Components.Controllers
{
    [Authorize]
    [Route("api/reports-server")]
    public class ReportsServerController : ReportsControllerBase
    {
        public ReportsServerController(IReportServiceConfiguration reportServiceConfiguration) : base(reportServiceConfiguration)
        {
        }
    }
}
