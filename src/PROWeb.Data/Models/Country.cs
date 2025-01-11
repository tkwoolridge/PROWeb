#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryId { get; set; }

        [Required]
        [StringLength(255)]
        public string CountryName { get; set; }

        public bool Active { get; set; }

        [ForeignKey("CountryId")]
        public ICollection<Voter> Voters { get; set; }

        [ForeignKey("CountryId")]
        public ICollection<Registration> Registrations { get; set; }
    }
}
