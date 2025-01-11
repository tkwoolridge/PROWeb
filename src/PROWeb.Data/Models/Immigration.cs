#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Immigration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImmigrationId { get; set; }

        public int? ImmigrationRecordId { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        public char Gender { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(255)]
        public string StatusDescription { get; set; }

        public DateTime? AuditChangeDate { get; set; }

        public DateTime AuditAddDate { get; set; }

        public DateTime? StatusAcquired { get; set; }

        public bool IsDeceased { get; set; }
    }
}
