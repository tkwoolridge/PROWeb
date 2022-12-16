#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class VoterFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoterFlagId { get; set; }

        [Required]
        [StringLength(150)]
        public string FlagDescription { get; set; }

        [StringLength(5)]
        public string Short { get; set; }

        public bool Ineligible { get; set; }

        public bool GraydOutInReports { get; set; }

        public bool Active { get; set; }

        public ICollection<Voter> Voters { get; set; }
    }
}
