using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Authentication.ViewModels.Account
{
    public class LoginVerificationCodeViewModel : SlimViewModelBase
    {
        [Display(Name = "Verification Code")]
        [MinLength(6, ErrorMessage = "Verification code must be 6 digits!")]
        [Required(ErrorMessage = "Verification code is required!")]
        public string? VerificationCode { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
