namespace PROWeb.Data.Models
{
    public class VoterHistory
    {
        public int RegistryYear { get; set; }

        public int VoterId { get; set; }

        public DateTime Created { get; set; }

        public string UserName { get; set; } = default!;

        public int? RegistrationId { get; set; }

        public Voter? Voter { get; set; }

        public ICollection<VoterHistoryField>? Fields { get; set; }
    }
}
