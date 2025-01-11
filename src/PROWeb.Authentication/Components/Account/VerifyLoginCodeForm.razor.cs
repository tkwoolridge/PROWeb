using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using PROWeb.Authentication.Properties;
using PROWeb.Authentication.Services;
using PROWeb.Authentication.ViewModels.Account;
using PROWeb.Common.Components;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Authentication.Components.Account
{
    public partial class VerifyLoginCodeForm : PROComponent
    {
        [Inject]
        protected SignInManager<PROUser> SignInManager { get; set; } = default!;

        [SupplyParameterFromForm(FormName = "loginVerifyCode")]
        protected LoginVerificationCodeViewModel Model { get; set; } = new();

        [Inject]
        internal IdentityRedirectManager RedirectManager { get; set; } = default!;

        [Inject]
        protected ISendVerificationMessagesService SendVerificationCodeService { get; set; } = default!;

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        [SupplyParameterFromQuery]
        private bool RememberMe { get; set; }

        private string? ErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected async Task ValidateCode()
        {
            if (Model?.VerificationCode is not { } code)
            {
                return;
            }

            var result = await SignInManager.TwoFactorSignInAsync("Email", code, RememberMe, Model.RememberMe);

            if (result.Succeeded)
            {
                RedirectManager.RedirectTo(ReturnUrl ?? "/");
            }
            else
            {
                ErrorMessage = Messages.InvalidVerificationCode;
            }
        }

        protected async Task ResendCode()
        {
            await SendVerificationCodeService.SendVerificationCodeMessageAsync();
        }
    }
}
