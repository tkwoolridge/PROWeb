#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Birth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BirthId { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string MiddleName { get; set; }

        [Required]
        public char Gender { get; set; }
    }
}
