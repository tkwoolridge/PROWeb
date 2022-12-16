#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class VoterDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoterDocumentId { get; set; }

        [Required]
        public int VoterId { get; set; }

        [Required]
        public int RegistryYear { get; set; }

        public Voter Voter { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        [StringLength(255)]
        public string DocumentName { get; set; }

        [StringLength(500)]
        public string DocumentDescription { get; set; }

        [Required]
        public string ExportFormat { get; set; }

        [Required]
        public byte[] Content { get; set; }
    }
}
