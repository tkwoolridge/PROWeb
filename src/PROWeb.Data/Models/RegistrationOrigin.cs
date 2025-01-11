#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class RegistrationOrigin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegistrationOriginId { get; set; }

        [StringLength(50)]
        [Required]
        public string OriginDescription { get; set; }
    }
}
