using Microsoft.AspNetCore.Identity;
using PROWeb.Authentication.Controllers;
using PROWeb.Data.Services.Logging;
using PROWeb.Office.Areas.Identity.Models;
using ILogger = Serilog.ILogger;

namespace PROWeb.Office.Areas.Identity.Controllers
{
    public class AccountController : AccountControllerBase<PROUser>
    {
        public AccountController(
            SignInManager<PROUser> signInManager,
            UserManager<PROUser> userManager,
            ILogger logger,
            IActivityLogService activityLog) : base(signInManager, userManager, logger, activityLog)
        {
        }
    }
}
