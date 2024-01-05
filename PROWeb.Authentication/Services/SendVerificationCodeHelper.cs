using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using PROWeb.Authentication.Models;
using PROWeb.Common.Helpers;
using PROWeb.Common.Transformations;
using PROWeb.Components.Services.Emails;
using PROWeb.Data.Authentication.Models;

namespace PROWeb.Authentication.Services
{
    public class SendVerificationCodeService(
        SignInManager<PROUser> signInManager, 
        UserManager<PROUser> userManager, 
        IWebHostEnvironment environment, 
        EmailService emailService) : ISendVerificationCodeService
    {
        public async Task SendCodeAsync()
        {
            // Ensure the user has gone through the username & password screen first.
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user?.Email is { } email)
            {
                var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

                await SendVerificationCodeMessage(token, email);
            }
        }

        private async Task SendVerificationCodeMessage(string code, string email)
        {
            VerificationCodeEmail xml = new VerificationCodeEmail()
            {
                Code = code
            };

            if (environment
                .WebRootFileProvider
                .GetFileInfo($"{RazorLibHelpers.GetWebRootPath()}/templates/VerifyCodeEmail.xslt")
                .PhysicalPath is { } xsltPath)
            {
                string html = ObjectToHtml.ToHtml(xml, xsltPath);

                await emailService.SendEmailAsync("PRO Verification Code", html, email);
            }
        }
    }
}
