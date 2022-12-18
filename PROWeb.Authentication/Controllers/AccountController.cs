using Kendo.Mvc.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Attributes;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Logging;
using ILogger = Serilog.ILogger;

namespace PROWeb.Authentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<PROUser> _signInManager;
        private readonly UserManager<PROUser> _userManager;
        private readonly ILogger _logger;
        private readonly IActivityLogService _activityLog;

        public AccountController(
            SignInManager<PROUser> signInManager,
            UserManager<PROUser> userManager,
            ILogger logger,
            IActivityLogService activityLog)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _activityLog = activityLog;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[SetTempDataModelState]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl = GetReturnUrl(returnUrl);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (model.Email is not { } email ||
                  model.Password is not { } password ||
                  await _userManager.FindByEmailAsync(email) is not { } user ||
                  user.UserName is not { } userName)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(userName, password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    await _activityLog.LogUserActivity(user, "User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.Warning("User account locked out.");

                    //TODO: set correct page.
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
            }
            catch (Exception exception)
            {
                //_logger.Error(exception);
                throw;
            }
        }

        protected string GetReturnUrl(string returnUrl)
        {
            return string.IsNullOrWhiteSpace(returnUrl) ? Url.Content("~/") : returnUrl;
        }

        private bool IsRequestAuthenticated()
        {
            return Request.HttpContext.User?.Identity?.IsAuthenticated == true;
        }
    }
}
