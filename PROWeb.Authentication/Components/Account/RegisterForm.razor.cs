using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PROWeb.Authentication.Components.Account.Configuration;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.Services;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Components;
using PROWeb.Data.Authentication.Models;
using PROWeb.Data.Authentication.Models.Enums;
using Serilog;

namespace PROWeb.Authentication.Components.Account
{
    public partial class RegisterForm : PROComponent
    {
        [Inject]
        protected ILogger Logger { get; set; } = default!;

        [Inject]
        protected UserManager<PROUser> UserManager { get; set; } = default!;

        [Inject]
        protected RoleManager<PRORole> RoleManager { get; set; } = default!;

        [Inject]
        internal IdentityRedirectManager RedirectManager { get; set; } = default!;

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        [SupplyParameterFromForm]
        private RegisterViewModel Model { get; set; } = new();

        private string? ErrorMessage { get; set; }

        private string? SuccessMessage { get; set; }

        [CascadingParameter]
        private HttpContext HttpContext { get; set; } = default!;

        [Inject]
        protected ISendVerificationMessagesService SendVerificationCodeServiceAsync { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (HttpMethods.IsGet(HttpContext.Request.Method))
            {
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }
        }

        protected async Task NewRegistration()
        {
            if (Model.Email is { } email && await UserManager.FindByEmailAsync(email) != null)
            {
                ErrorMessage = string.Format(Messages.UserAlreadyRegisteredMessage, email);

                return;
            }

            if (Model.Password is { } password)
            {
                PROUser user = Model.Adapt<PROUser>();

                user.UserName = Model.Email?.Split('@')[0];
                user.TwoFactorEnabled = true;

                var result = await UserManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    ErrorMessage = Messages.RegistrationFailedMessage;

                    return;
                }

                await UserManager.AddToRoleAsync(user, PROUserRoles.FrontOfficer.ToString());

                string link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{Constants.Pages.EmailConformation}" + "?email={0}&code={1}";

                await SendVerificationCodeServiceAsync.SendEmailConformationMessageAsync(user, link);

                SuccessMessage = Messages.ConformationEmailWasSentMessage;
            }
        }
    }
}
