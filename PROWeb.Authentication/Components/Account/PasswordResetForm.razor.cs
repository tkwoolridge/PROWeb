using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.Services;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Components;
using PROWeb.Data.Authentication.Models;
using Serilog;
using System.Diagnostics;
using System.Text;

namespace PROWeb.Authentication.Components.Account
{
    public partial class PasswordResetForm : PROComponent
    {
        [Inject]
        protected ILogger Logger { get; set; } = default!;

        [Inject]
        protected UserManager<PROUser> UserManager { get; set; } = default!;

        [Inject]
        internal IdentityRedirectManager RedirectManager { get; set; } = default!;

        [Inject]
        protected ISendVerificationMessagesService SendVerificationCodeServiceAsync { get; set; } = default!;

        [SupplyParameterFromForm]
        private ResetPasswordViewModel Model { get; set; } = new();

        [CascadingParameter]
        private HttpContext HttpContext { get; set; } = default!;

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        [SupplyParameterFromQuery]
        private string? Email { get; set; }

        [SupplyParameterFromQuery]
        private string? Code { get; set; }

        private string? ErrorMessage { get; set; }

        private string? SuccessMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (HttpMethods.IsGet(HttpContext.Request.Method))
            {
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            Model.Email = Email;
        }

        protected async Task ResetPassword()
        {
            Debug.Assert(Model.Email != null);
            Debug.Assert(Model.Password != null);

            if (await UserManager.FindByEmailAsync(Model.Email) is not { } user ||
                Code is null ||
                Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code)) is not { } code ||
                Model.Password is not { } password
                )
            {
                ErrorMessage = string.Format(Messages.UserNotFoundMessage, Model.Email);

                return;
            }

            var result = await UserManager.ResetPasswordAsync(user, code, password);

            if (result.Succeeded)
            {
                SuccessMessage = string.Format(Messages.ResetUserPasswordSucceededMessage, Model.Email);
            }
            else
            {
                ErrorMessage = string.Format(Messages.RessetPasswordFailedMessage, Model.Email);
            }
        }
    }
}
