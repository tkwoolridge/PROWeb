using PROWeb.Common.ViewModels;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Person;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Components.ViewModels.Voter
{
    public class VoterViewModel : ViewModelBase, IPersonDetailsViewModel, IAddressViewModel, IContactInfoViewModel, IPersonFlagsViewModel, IPersonDocumentsViewModel<VoterDocumentViewModel>
    {
        public int VoterId { get; set; }

        public int RegistryYear { get; set; }

        public int? BirthID { get; set; }

        public int? ImmigrationID { get; set; }

        public string? Title { get; set; }

        public char? Initial { get; set; }

        [Required(ErrorMessage = "Voter name is requred!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Voter last name is requred!")]
        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? MaidenName { get; set; }

        public char Gender { get; set; }

        [Required(ErrorMessage = "DOB is required!")]
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

        [Required (ErrorMessage = "Voter email is required!")]
        [EmailAddress(ErrorMessage = "Voter email is not in correct format!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Voter email is required")]
        [Phone(ErrorMessage = "Contact phone is not in correct format!")]
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

        public List<VoterFlagViewModel> VoterFlags { get; set; } = null!;

        public List<int> VoterFlagsValues { get; set; } = null!;

        public List<VoterDocumentViewModel> Documents { get; set; } = null!;
    }
}
