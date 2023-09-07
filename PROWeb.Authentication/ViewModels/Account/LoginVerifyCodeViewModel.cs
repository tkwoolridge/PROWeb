using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Authentication.ViewModels.Account
{
    public class LoginVerificationCodeViewModel : SlimViewModelBase
    {
        [Display(Name = "Verification Code")]
        public string? VerificationCode { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? Email { get; set; }

        public bool ResendCode { get; set; }
    }
}
