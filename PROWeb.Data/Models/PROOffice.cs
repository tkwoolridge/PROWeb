#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class PROOffice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PROOfficeId { get; set; }

        [Required]
        [StringLength(255)]
        public string GeneralName { get; set; }

        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        [StringLength(255)]
        public string Address3 { get; set; }

        [StringLength(255)]
        public string Address4 { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string PO1 { get; set; }

        [StringLength(50)]
        public string PO2 { get; set; }

        [StringLength(50)]
        public string PO3 { get; set; }

        [Required]
        public int YearEndMonth { get; set; }

        [Required]
        public int YearEndDay { get; set; }

        [StringLength(150)]
        public string Website { get; set; }

        [Required]
        public int ElectionYear { get; set; }

        [Required]
        public DateTime NextElectionsDate { get; set; }

        [Required]
        [StringLength(150)]
        public DateTime NextAdvancedPollDate { get; set; }

        [StringLength(150)]
        [Required]
        public string Email { get; set; }
    }
}
