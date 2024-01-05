namespace PROWeb.Authentication.Services
{
    public interface ISendVerificationCodeService
    {
        Task SendCodeAsync();
    }
}