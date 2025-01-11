using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Authentication.ViewModels.Account
{
    public class PasswordResetRequestViewModel : SlimViewModelBase
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
