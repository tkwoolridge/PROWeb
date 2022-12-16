#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class FormType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FormTypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string FormName { get; set; }
    }
}
