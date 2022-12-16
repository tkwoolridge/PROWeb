#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Parish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParishNo { get; set; }

        [Required]
        [StringLength(50)]
        public string ParishName { get; set; }

        public bool Active { get; set; }

        [ForeignKey("ParishNo")]
        public ICollection<Assessment> Assessments { get; set; }
    }
}
