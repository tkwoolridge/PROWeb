#nullable disable
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Models
{
    public class DriverLicense
    {
        [Key]
        public string DriverLicenseId { get; set; }

        [Required]
        public DateTime AuditDate { get; set; }

        [Required]
        public char LicenseType { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string MiddleName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public int AssessmentNo { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }
    }
}
