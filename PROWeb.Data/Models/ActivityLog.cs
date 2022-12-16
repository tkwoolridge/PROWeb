#nullable disable

using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Models
{
    public class ActivityLog
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public int TypeId { get; set; }

        public DateTime LogDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
