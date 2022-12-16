#nullable disable

using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Models
{
    public class AssessmentFlag
    {
        [Key]
        public int AssessmentFlagId { get; set; }

        [Required]
        [StringLength(50)]
        public string FlagDescription { get; set; }

        [Required]
        [StringLength(10)]
        public string Short { get; set; }

        public bool Active { get; set; }
    }
}
