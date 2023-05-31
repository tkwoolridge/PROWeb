using PROWeb.Common.ViewModels;
using PROWeb.Components.ViewModels.Voters;

namespace PROWeb.Office.ViewModels.EligiblePolls
{
    public class EligibleVoterViewModel : SlimViewModelBase
    {
        private int? _voterId;

        public int? VoterId
        {
            get => _voterId;
            set => RaiseAndSetIfChanged(ref _voterId, value);
        }

        private int _registryYear;

        public int RegistryYear
        {
            get => _registryYear;
            set => RaiseAndSetIfChanged(ref _registryYear, value);
        }

        private int? _birthId;

        public int? BirthId
        {
            get => _birthId;
            set => RaiseAndSetIfChanged(ref _birthId, value);
        }

        private int? _immigartionId;

        public int? ImmigrationId
        {
            get => _immigartionId;
            set => RaiseAndSetIfChanged(ref _immigartionId, value);
        }

        private string? _title;

        public string? Title
        {
            get => _title;
            set => RaiseAndSetIfChanged(ref _title, value);
        }

        private char? _initial;

        public char? Initial
        {
            get => _initial;
            set => RaiseAndSetIfChanged(ref _initial, value);
        }

        private string? _firstName;

        public string? FirstName
        {
            get => _firstName;
            set => RaiseAndSetIfChanged(ref _firstName, value);
        }

        private string? _lastName;

        public string? LastName
        {
            get => _lastName;
            set => RaiseAndSetIfChanged(ref _lastName, value);
        }

        private string? _middleName;

        public string? MiddleName
        {
            get => _middleName;
            set => RaiseAndSetIfChanged(ref _middleName, value);
        }

        private string? _maidenName;

        public string? MaidenName
        {
            get => _maidenName;
            set => RaiseAndSetIfChanged(ref _maidenName, value);
        }

        public string FullName => LastName + (!string.IsNullOrWhiteSpace(MiddleName) ? $" {MiddleName} " : "") + FirstName;

        private char? _gender;

        public char? Gender
        {
            get => _gender;
            set => RaiseAndSetIfChanged(ref _gender, value);
        }

        private DateTime? _dateOfBirth;

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => RaiseAndSetIfChanged(ref _dateOfBirth, value);
        }

        public int Age => DateTime.Now.Year - DateOfBirth?.Year ?? 0;

        public string Address => $"{HouseNo} {Address2}, {ParishName} {PostalCode}";

        private int _assessmentNo;

        public int AssessmentNo
        {
            get => _assessmentNo;
            set => RaiseAndSetIfChanged(ref _assessmentNo, value);
        }

        private string? _address1;

        public string? Address1
        {
            get => _address1;
            set => RaiseAndSetIfChanged(ref _address1, value);
        }

        private string? _houseNo;

        public string? HouseNo
        {
            get => _houseNo;
            set => RaiseAndSetIfChanged(ref _houseNo, value);
        }

        private string? _address2;

        public string? Address2
        {
            get => _address2;
            set => RaiseAndSetIfChanged(ref _address2, value);
        }

        private string? _postalCode;

        public string? PostalCode
        {
            get => _postalCode;
            set => RaiseAndSetIfChanged(ref _postalCode, value);
        }

        private string? _parishName;

        public string? ParishName
        {
            get => _parishName;
            set => RaiseAndSetIfChanged(ref _parishName, value);
        }

        private int _constituencyNo;

        public int ConstituencyNo
        {
            get => _constituencyNo;
            set => RaiseAndSetIfChanged(ref _constituencyNo, value);
        }

        private string? _constituencyName;

        public string? ConstituencyName
        {
            get => _constituencyName;
            set => RaiseAndSetIfChanged(ref _constituencyName, value);
        }

        private bool _isEligible;

        public bool IsEligible
        {
            get => _isEligible;
            set => RaiseAndSetIfChanged(ref _isEligible, value);
        }

        private bool? _isBogusNo;

        public bool? IsBogusNo
        {
            get => _isBogusNo;
            set => RaiseAndSetIfChanged(ref _isBogusNo, value);
        }

        private string? _bogusNo;

        public string? BogusNo
        {
            get => _bogusNo;
            set => RaiseAndSetIfChanged(ref _bogusNo, value);
        }

        private int? _bogusConstituencyNo;

        public int? BogusConstituencyNo
        {
            get => _bogusConstituencyNo;
            set => RaiseAndSetIfChanged(ref _bogusConstituencyNo, value);
        }

        private string? _bogusConstituencyName;

        public string? BogusConstituencyName
        {
            get => _bogusConstituencyName;
            set => RaiseAndSetIfChanged(ref _bogusConstituencyName, value);
        }

        private string? _email;

        public string? Email
        {
            get => _email;
            set => RaiseAndSetIfChanged(ref _email, value);
        }

        private string? _contactPhone;

        public string? ContactPhone
        {
            get => _contactPhone;
            set => RaiseAndSetIfChanged(ref _contactPhone, value);
        }

        private string? _phoneHome;

        public string? PhoneHome
        {
            get => _phoneHome;
            set => RaiseAndSetIfChanged(ref _phoneHome, value);
        }

        private string? _phoneWork;

        public string? PhoneWork
        {
            get => _phoneWork;
            set => RaiseAndSetIfChanged(ref _phoneWork, value);
        }

        private string? _phoneMobile;

        public string? PhoneMobile
        {
            get => _phoneMobile;
            set => RaiseAndSetIfChanged(ref _phoneMobile, value);
        }

        private string? _driverLicense;

        public string? DriverLicense
        {
            get => _driverLicense;
            set => RaiseAndSetIfChanged(ref _driverLicense, value);
        }

        private string? _comment;

        public string? Comment
        {
            get => _comment;
            set => RaiseAndSetIfChanged(ref _comment, value);
        }

        private bool? _wasBornIn;

        public bool? WasBornIn
        {
            get => _wasBornIn;
            set => RaiseAndSetIfChanged(ref _wasBornIn, value);
        }

        private int? _countryId;

        public int? CountryId
        {
            get => _countryId;
            set => RaiseAndSetIfChanged(ref _countryId, value);
        }

        private string? _countryName;

        public string? CountryName
        {
            get => _countryName;
            set => RaiseAndSetIfChanged(ref _countryName, value);
        }

        private bool? _commonwealthCitizen;

        public bool? CommonwealthCitizen
        {
            get => _commonwealthCitizen;
            set => RaiseAndSetIfChanged(ref _commonwealthCitizen, value);
        }

        private DateTime? _bermudianStatusGranted;

        public DateTime? BermudianStatusGranted
        {
            get => _bermudianStatusGranted;
            set => RaiseAndSetIfChanged(ref _bermudianStatusGranted, value);
        }

        private bool? _registeredAsElector;

        public bool? RegisteredAsElector
        {
            get => _registeredAsElector;
            set => RaiseAndSetIfChanged(ref _registeredAsElector, value);
        }

        private bool? _isBermudianStatusGranted;

        public bool? IsBermudianStatusGranted
        {
            get => _isBermudianStatusGranted;
            set => RaiseAndSetIfChanged(ref _isBermudianStatusGranted, value);
        }

        private List<VoterFlagViewModel>? _flags;

        public List<VoterFlagViewModel>? Flags
        {
            get => _flags;
            set => RaiseAndSetIfChanged(ref _flags, value);
        }

        private List<DocumentViewModel>? _documents;

        public List<DocumentViewModel>? Documents
        {
            get => _documents;
            set => RaiseAndSetIfChanged(ref _documents, value);
        }
    }
}
