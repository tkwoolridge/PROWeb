using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.Services;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Components;
using PROWeb.Data.Authentication.Models;
using Serilog;

namespace PROWeb.Authentication.Components.Account
{
    public partial class LoginForm : PROComponent
    {
        [Inject]
        protected SignInManager<PROUser> SignInManager { get; set; } = default!;

        [Inject]
        protected ILogger Logger { get; set; } = default!;

        [Inject]
        protected UserManager<PROUser> UserManager { get; set; } = default!;

        [Inject]
        protected ISendVerificationCodeService SendVerificationCodeService { get; set; } = default!;

        [Inject]         
        internal IdentityRedirectManager RedirectManager { get; set; } = default!;

        [CascadingParameter]
        private HttpContext HttpContext { get; set; } = default!;

        [SupplyParameterFromForm]
        private LoginViewModel Model { get; set; } = new();

        private string? ErrorMessage { get; set; }

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (HttpMethods.IsGet(HttpContext.Request.Method))
            {
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }
        }

        protected async Task HandleValidLogin()
        {
            if (Model.Email is not { } email ||
                  Model.Password is not { } password ||
                  await UserManager.FindByEmailAsync(email) is not { } user ||
                  user.UserName is not { } userName)
            {
                ErrorMessage = Messages.InvalidLoginAttemptMessage;

                return;
            }

            var result = await SignInManager.PasswordSignInAsync(user.UserName, Model.Password, Model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                Logger.Information(string.Format(Messages.UserLoggedInMessage), user.UserName);
                RedirectManager.RedirectTo(ReturnUrl);
            }
            else if (result.RequiresTwoFactor)
            {
                await SendVerificationCodeService.SendCodeAsync();

                RedirectManager.RedirectTo(
                    "Account/VerifyLoginCode",
                    new() { ["returnUrl"] = ReturnUrl, ["rememberMe"] = Model.RememberMe});
            }
            else if (result.IsLockedOut)
            {
                Logger.Warning(Messages.UserAccountLockedOutMessage);
                RedirectManager.RedirectTo("Account/Lockout");
            }
            else
            {
                ErrorMessage = Messages.InvalidLoginAttemptMessage;
            }
        }

        protected void HandleInvalidLogin()
        {

        }
    }
}
