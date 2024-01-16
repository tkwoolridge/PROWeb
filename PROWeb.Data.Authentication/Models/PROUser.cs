using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Authentication.Models
{
    //[AspNetUserStore] AuthenticatorKey 5GQASF222TFVZOCLTB6JT3Z5N7PEEXFT
    public class PROUser : IdentityUser<int>
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public bool IsActive { get; set; }

        //public bool CanUseAdministration { get; set; }

        //public bool CanChangeFormStatus { get; set; }

        //public bool CanSearchRegistrySnapshots { get; set; }

        //public bool CanRunReports { get; set; }

        //public bool CanUpdateVoterRegistration { get; set; }
    }
}
