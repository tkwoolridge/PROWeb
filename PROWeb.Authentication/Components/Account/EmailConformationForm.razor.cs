using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using PROWeb.Authentication.Properties;
using PROWeb.Common.Components;
using PROWeb.Data.Authentication.Models;
using System.Text;

namespace PROWeb.Authentication.Components.Account
{
    public partial class EmailConformationForm : PROComponent
    {
        private string? ErrorMessage { get; set; }

        private string? SuccessMessage { get; set; }

        [Inject]
        protected UserManager<PROUser> UserManager { get; set; } = default!;

        [SupplyParameterFromQuery]
        private string? Code { get; set; }

        [SupplyParameterFromQuery]
        private string? Email { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Email is { } email &&
               Code is not null &&
               Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code)) is { } code &&
               await UserManager.FindByEmailAsync(email) is { } user &&
               await UserManager.ConfirmEmailAsync(user, code) is { Succeeded: true }
               )
            {
                SuccessMessage = Messages.EmailVerifiedMessage;
            }
            else
            {
                ErrorMessage = Messages.EmailConformationFailedMessage;
            }
        }
    }
}
