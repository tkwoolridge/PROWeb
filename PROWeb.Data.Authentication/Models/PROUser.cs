#nullable disable

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Authentication.Models
{
    public class PROUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public bool CanUseAdministration { get; set; }

        public bool CanChangeFormStatus { get; set; }

        public bool CanSearchRegistrySnapshots { get; set; }

        public bool CanRunReports { get; set; }

        public bool CanUpdateVoterRegistration { get; set; }
    }
}
