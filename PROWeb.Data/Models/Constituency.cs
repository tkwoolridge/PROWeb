#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Constituency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConstituencyNo { get; set; }

        [Required]
        [StringLength(50)]
        public string BogusNo { get; set; }

        [StringLength(50)]
        [Required]
        public string ConstituencyName { get; set; }

        [ForeignKey("ConstituencyNo")]
        public ICollection<Assessment> Assessments { get; set; }

        public ICollection<Voter> Voters { get; set; }

        public ICollection<Registration> Registrations { get; set; }

        public ICollection<Registration> OldRegistrations { get; set; }
    }
}
