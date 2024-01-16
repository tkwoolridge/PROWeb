using PROWeb.Data.Authentication.Models;

namespace PROWeb.Authentication.Services
{
    public interface ISendVerificationMessagesService
    {
        Task SendVerificationCodeMessageAsync();

        Task SendEmailConformationMessageAsync(PROUser user, string link);

        Task SendResetPasswordMessageAsync(PROUser user, string link);
    }
}