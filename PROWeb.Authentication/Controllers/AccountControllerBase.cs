
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROWeb.Authentication.Models;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Extensions;
using PROWeb.Data.Services.Logging;
using ILogger = Serilog.ILogger;

namespace PROWeb.Authentication.Controllers
{
    [Area("Identity")]
    public abstract class AccountControllerBase<TUser> : Controller
        where TUser : IdentityUser, IPROUser
    {
        private const string AreaPath = "~/Identity/Account";

        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger _logger;
        private readonly IActivityLogService _activityLog;

        protected AccountControllerBase(
            SignInManager<TUser> signInManager,
            UserManager<TUser> userManager,
            ILogger logger,
            IActivityLogService activityLog)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _activityLog = activityLog;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();


                if (User.Identity?.Name is { } userName &&
                    await _userManager.FindByNameAsync(userName) is { } user)
                {
                    await _activityLog.LogUserActivity(userName, Messages.UserLoggedOutMessage);

                    await _userManager.UpdateSecurityStampAsync(user);
                }
            }

            return LocalRedirect(AreaPath + "/Login");
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl = GetReturnUrl(returnUrl);

            if (IsRequestAuthenticated())
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return View(new LoginViewModel());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
                    ModelState.AddModelError(string.Empty, Messages.InvalidLoginAttemptMessage);
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(userName, password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    await _activityLog.LogUserActivity(user.UserName, Messages.UserLoggedInMessage);
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage(AreaPath + "/LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.Warning(Messages.UserAccountLockedOutMessage);

                    //TODO: create page.
                    return LocalRedirect(AreaPath + "/Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Messages.InvalidLoginAttemptMessage);
                    return View(model);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }
        }

        protected string GetReturnUrl(string? returnUrl)
        {
            return string.IsNullOrWhiteSpace(returnUrl) ? Url.Content("~/") : returnUrl;
        }

        private bool IsRequestAuthenticated()
        {
            return Request.HttpContext.User?.Identity?.IsAuthenticated == true;
        }
    }
}
