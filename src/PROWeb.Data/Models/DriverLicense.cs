#nullable disable
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public int AssessmentNo { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }
    }
}
