#nullable disable
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Models
{
    public class RegistrationStatus
    {
        [Key]
        public int RegistrationStatusId { get; set; }

        [StringLength(50)]
        [Required]
        public string StatusDescription { get; set; }
    }
}
