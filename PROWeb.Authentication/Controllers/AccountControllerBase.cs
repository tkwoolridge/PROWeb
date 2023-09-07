
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROWeb.Authentication.Models;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Extensions;
using PROWeb.Common.Helpers;
using PROWeb.Common.Transformations;
using PROWeb.Data.Services.Logging;
using PROWeb.Components.Services.Emails;
using ILogger = Serilog.ILogger;

namespace PROWeb.Authentication.Controllers
{
    [Area("Identity")]
    public abstract class AccountControllerBase<TUser> : Controller
        where TUser : IdentityUser, IPROUser
    {
        private const string AreaPath = "/Identity/Account";

        private readonly IWebHostEnvironment _environment;
        private readonly EmailService _emailService;
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger _logger;
        private readonly IActivityLogService _activityLog;

        protected AccountControllerBase(
            IWebHostEnvironment environment,
            EmailService emailService,
            SignInManager<TUser> signInManager,
            UserManager<TUser> userManager,
            ILogger logger,
            IActivityLogService activityLog)
        {
            _environment = environment;
            _emailService = emailService;
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
                await _signInManager.ForgetTwoFactorClientAsync();

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
                var result = await _signInManager.PasswordSignInAsync(userName, password, model.RememberMe, lockoutOnFailure: true);

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("LoginVerifyCode", new { email, rememberMe = model.RememberMe, returnUrl } );
                }

                if (result.IsLockedOut)
                {
                    _logger.Warning(Messages.UserAccountLockedOutMessage);

                    //TODO: create page.
                    return LocalRedirect(AreaPath + "/Lockout");
                }
                else if (result.Succeeded)
                {
                    await _activityLog.LogUserActivity(user.UserName, Messages.UserLoggedInMessage);
                    return LocalRedirect(returnUrl);
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

        [AllowAnonymous]
        public async Task<IActionResult> LoginVerifyCode(string  email, bool rememberMe, string? returnUrl = null)
        {
            // make sure the user email is valid
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction("Error", new { code = "403" });

            // generate the 2fa token
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            //// send the user the 2fa token via email
            await SendVerificationCodeMessage(token, email);

            var model = new LoginVerificationCodeViewModel()
            {
                Email = email,
                RememberMe = rememberMe,
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginVerifyCode(LoginVerificationCodeViewModel model, string returnUrl)
        {
            if (model.ResendCode
                && model.Email is { } email &&
                await _userManager.FindByEmailAsync(model.Email) is { } user)
            {
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                await SendVerificationCodeMessage(token, email);

                model.ResendCode = false;

                return View(model);
            }

            model.ResendCode = false;

            if (!ModelState.IsValid || model.VerificationCode is not { } code)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorSignInAsync("Email", code, false, model.RememberMe);
            
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(model);
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

        private async Task SendVerificationCodeMessage(string code, string email)
        {
            VerificationCodeEmail xml = new VerificationCodeEmail()
            {
                Code = code
            };

            if(_environment
                .WebRootFileProvider
                .GetFileInfo($"{RazorLibHelpers.GetWebRootPath()}/templates/VerifyCodeEmail.xslt")
                .PhysicalPath is { } xsltPath)
            {
                string html = ObjectToHtml.ToHtml(xml, xsltPath);

                await _emailService.SendEmailAsync("PRO Verification Code", html, email);
            }
        }
    }
}
