#nullable disable

using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Models
{
    public class OldPROUser
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

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
