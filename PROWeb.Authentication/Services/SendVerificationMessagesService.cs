using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using PROWeb.Authentication.Models;
using PROWeb.Common.Helpers;
using PROWeb.Common.Transformations;
using PROWeb.Components.Services.Emails;
using PROWeb.Data.Authentication.Models;
using System.Text;

namespace PROWeb.Authentication.Services
{
    public class SendVerificationMessagesService(
        SignInManager<PROUser> signInManager,
        UserManager<PROUser> userManager,
        IWebHostEnvironment environment,
        EmailService emailService) : ISendVerificationMessagesService
    {
        public async Task SendVerificationCodeMessageAsync()
        {
            // Ensure the user has gone through the username & password screen first.
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user?.Email is { } email)
            {
                var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

                VerificationCodeEmail xml = new VerificationCodeEmail()
                {
                    Code = token
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

        public async Task SendEmailConformationMessageAsync(PROUser user, string link)
        {
            if (environment
                .WebRootFileProvider
                .GetFileInfo($"{RazorLibHelpers.GetWebRootPath()}/templates/UserEmailConformation.xslt")
                .PhysicalPath is { } xsltPath &&
                await userManager.GenerateEmailConfirmationTokenAsync(user) is { } code &&
                user.Email is { } email)
            {
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                UserEmailConformation xml = new UserEmailConformation()
                {
                    Link = string.Format(link, user.Email, code)
                };

                string html = ObjectToHtml.ToHtml(xml, xsltPath);

                await emailService.SendEmailAsync("PRO Email Conformation", html, email);
            }
        }

        public async Task SendResetPasswordMessageAsync(PROUser user, string link)
        {
            if (environment
                .WebRootFileProvider
                .GetFileInfo($"{RazorLibHelpers.GetWebRootPath()}/templates/ConfirmResetPasswordEmail.xslt")
                .PhysicalPath is { } xsltPath &&
                await userManager.GeneratePasswordResetTokenAsync(user) is { } code &&
                user.Email is { } email)
            {
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                ConfirmResetPasswordEmail xml = new ConfirmResetPasswordEmail()
                {
                    Link = string.Format(link, user.Email, code)
                };

                string html = ObjectToHtml.ToHtml(xml, xsltPath);

                await emailService.SendEmailAsync("PRO Reset Password", html, email);
            }
        }
    }
}
