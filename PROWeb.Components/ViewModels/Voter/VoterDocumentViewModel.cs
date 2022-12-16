using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Voter
{
    public class VoterDocumentViewModel : ViewModelBase
    {
        public int VoterDocumentId { get; set; }

        public int VoterId { get; set; }

        public int RegistryYear { get; set; }

        public DateTime DocumentDate { get; set; }

        public string? DocumentName { get; set; }

        public string? DocumentDescription { get; set; }

        public string? ExportFormat { get; set; }
    }
}
