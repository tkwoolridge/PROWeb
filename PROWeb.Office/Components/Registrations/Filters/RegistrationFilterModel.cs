using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Office.Components.Registrations.Filters
{
    public class RegistrationFilterModel : SlimViewModelBase
    {
        private int _registryYear;

        public int RegistryYear
        {
            get => _registryYear;
            set => RaiseAndSetIfChanged(ref _registryYear, value);
        }

        private string? _firstName;

        [Display(Name = "First Name:")]
        public string? FirstName
        {
            get => _firstName;
            set => RaiseAndSetIfChanged(ref _firstName, value);
        }

        private string? _lastName;

        [Display(Name = "Last Name:")]
        public string? LastName
        {
            get => _lastName;
            set => RaiseAndSetIfChanged(ref _lastName, value);
        }

        private string? _middleName;

        [Display(Name = "Middle Name:")]
        public string? MiddleName
        {
            get => _middleName;
            set => RaiseAndSetIfChanged(ref _middleName, value);
        }

        private string? _maidenName;

        [Display(Name = "Maiden Name:")]
        public string? MaidenName
        {
            get => _maidenName;
            set => RaiseAndSetIfChanged(ref _maidenName, value);
        }

        private bool? _isEligible;

        [Display(Name = "Eligible:")]
        public bool? IsEligible
        {
            get => _isEligible;
            set => RaiseAndSetIfChanged(ref _isEligible, value);
        }

        private DateTime? _dateOfBirth;

        [Display(Name = "DOB:")]
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => RaiseAndSetIfChanged(ref _dateOfBirth, value);
        }

        private int? _ageFrom;

        [Display(Name = "Age From:")]
        public int? AgeFrom
        {
            get => _ageFrom;
            set => RaiseAndSetIfChanged(ref _ageFrom, value);
        }

        private int? _ageTo;

        [Display(Name = "Age To:")]
        public int? AgeTo
        {
            get => _ageTo;
            set => RaiseAndSetIfChanged(ref _ageTo, value);
        }

        private string? _phone;

        [Display(Name = "Phone:")]
        public string? Phone
        {
            get => _phone;
            set => RaiseAndSetIfChanged(ref _phone, value);
        }

        private int? _assessmentNo;


        [Display(Name = "Assessment No:")]
        public int? AssessmentNo
        {
            get => _assessmentNo;
            set => RaiseAndSetIfChanged(ref _assessmentNo, value);
        }

        private string? _streetName;

        [Display(Name = "Street Name:")]
        public string? StreetName
        {
            get => _streetName;
            set => RaiseAndSetIfChanged(ref _streetName, value);
        }

        private string? _houseNo;

        [Display(Name = "House No:")]
        public string? HouseNo
        {
            get => _houseNo;
            set => RaiseAndSetIfChanged(ref _houseNo, value);
        }

        private int? _constituencyNo;

        [Display(Name = "Constituency:")]
        public int? ConstituencyNo
        {
            get => _constituencyNo;
            set => RaiseAndSetIfChanged(ref _constituencyNo, value);
        }

        private int? _parishNo;

        [Display(Name = "Parish:")]
        public int? ParishNo
        {
            get => _parishNo;
            set => RaiseAndSetIfChanged(ref _parishNo, value);
        }

        private string? _postalCode;

        [Display(Name = "Postal Code:")]
        public string? PostalCode
        {
            get => _postalCode;
            set => RaiseAndSetIfChanged(ref _postalCode, value);
        }

        private List<int>? _flags;

        public List<int>? Flags
        {
            get => _flags;
            set => RaiseAndSetIfChanged(ref _flags, value);
        }
    }
}
