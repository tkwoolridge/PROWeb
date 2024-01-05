#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Assessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AssessmentNo { get; set; }

        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(10)]
        [Required]
        public string HouseNo { get; set; }

        [Required]
        [StringLength(255)]
        public string Address2 { get; set; }

        [StringLength(5)]
        [Required]
        public string PostalCode { get; set; }

        [Required]
        public int ConstituencyNo { get; set; }

        [Required]
        public Constituency Constituency { get; set; }

        [Required]
        public int ParishNo { get; set; }

        [Required]
        public bool IsBogus { get; set; }

        [Required]
        public Parish Parish { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        [ForeignKey("AssessmentNo")]
        public ICollection<Voter> Voters { get; set; }

        [ForeignKey("AssessmentNo")]
        public ICollection<Registration> Registrations { get; set; }

        public ICollection<Registration> OldRegistrations { get; set; }
    }
}
