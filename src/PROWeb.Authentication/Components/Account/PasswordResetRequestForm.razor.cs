using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PROWeb.Authentication.Components.Account.Configuration;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.Services;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Components;
using PROWeb.Data.Authentication.Models;
using Serilog;

namespace PROWeb.Authentication.Components.Account
{
    public partial class PasswordResetRequestForm : PROComponent
    {
        [Inject]
        protected ILogger Logger { get; set; } = default!;

        [Inject]
        protected UserManager<PROUser> UserManager { get; set; } = default!;

        [Inject]
        internal IdentityRedirectManager RedirectManager { get; set; } = default!;

        [Inject]
        protected ISendVerificationMessagesService SendVerificationCodeServiceAsync { get; set; } = default!;

        [CascadingParameter]
        private HttpContext HttpContext { get; set; } = default!;

        [SupplyParameterFromForm]
        private PasswordResetRequestViewModel Model { get; set; } = new();

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        private string? ErrorMessage { get; set; }

        private string? SuccessMessage { get; set; }

        private async Task SendResetPasswordRequest()
        {
            if (Model?.Email is { } email && await UserManager.FindByEmailAsync(email) is { } user)
            {
                string link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{Constants.Pages.PasswordReset}" + "?email={0}&code={1}";

                await SendVerificationCodeServiceAsync.SendResetPasswordMessageAsync(user, link);

                SuccessMessage = string.Format(Messages.PasswordResetLinkWasSentMessage, email);
            }
            else
            {
                ErrorMessage = string.Format(Messages.UserNotFoundMessage, Model?.Email);
            }
        }
    }
}
