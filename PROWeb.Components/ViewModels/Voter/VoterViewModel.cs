using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Voter
{
    public class VoterViewModel : ViewModelBase, IVoterDetailsViewModel, IVoterAssessmentViewModel, IVoterViewModel
    {
        public int VoterId { get; set; }

        public int RegistryYear { get; set; }

        public int? BirthID { get; set; }

        public int? ImmigrationID { get; set; }

        public string? Title { get; set; }

        public char? Initial { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? MaidenName { get; set; }

        public char Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AssessmentNo { get; set; }

        public string? Address1 { get; set; }

        public string? HouseNo { get; set; }

        public string? Address2 { get; set; }

        public string? PostalCode { get; set; }

        public string? ParishName { get; set; }

        public int ConstituencyNo { get; set; }

        public string? ConstituencyName { get; set; }

        public bool IsEligible { get; set; }

        public bool? IsBogusNo { get; set; }

        public string? BogusNo { get; set; }

        public int? BogusConstituencyNo { get; set; }

        public string? BogusConstituencyName { get; set; }

        public string? Email { get; set; }

        public string? ContactPhone { get; set; }

        public string? PhoneHome { get; set; }

        public string? PhoneWork { get; set; }

        public string? PhoneMobile { get; set; }

        public string? DriverLicense { get; set; }

        public string? Comment { get; set; }

        public bool? WasBornIn { get; set; }

        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public bool? CommonwealthCitizen { get; set; }

        public DateTime? BermudianStatusGranted { get; set; }

        public bool? RegisteredAsElector { get; set; }

        public bool? IsBermudianStatusGranted { get; set; }

        public DateTime LastUpdated { get; set; }

        public string? LastUpdatedBy { get; set; }

        public ICollection<VoterFlagViewModel> VoterFlags { get; set; } = null!;

        public ICollection<VoterDocumentViewModel> VoterDocuments { get; set; } = null!;
    }
}
