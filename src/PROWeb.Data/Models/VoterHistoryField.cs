using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class VoterHistoryField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int VoterId { get; set; }

        public int RegistryYear { get; set; }

        public DateTime Created { get; set; }

        public string Field { get; set; } = default!;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public VoterHistory VoterHistory { get; set; } = default!;
    }
}
