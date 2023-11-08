using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROWeb.Authentication.Controllers;
using PROWeb.Components.Services.Emails;
using PROWeb.Data.Authentication.Models;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Logging;
using ILogger = Serilog.ILogger;

namespace PROWeb.Office.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : AccountControllerBase
    {
        public AccountController(
            IWebHostEnvironment environment,
            EmailService emailService,
            SignInManager<PROUser> signInManager,
            UserManager<PROUser> userManager,
            ILogger logger,
            IActivityLogService activityLog) : base(environment, emailService, signInManager, userManager, logger, activityLog)
        {
        }
    }
}
