using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Authentication.ViewModels.Account
{
    public class LoginViewModel : SlimViewModelBase
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
