using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Assessments.ViewModels
{
    public class AssessmentFilterViewModel : SlimViewModelBase
    {
        private int? _assessmentNo;

        public int? AssessmentNo
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

        private string? _address2;

        public string? Address2
        {
            get => _address2;
            set => RaiseAndSetIfChanged(ref _address2, value);
        }

        private string? _houseNo;

        public string? HouseNo
        {
            get => _houseNo;
            set => RaiseAndSetIfChanged(ref _houseNo, value);
        }

        private int? _parishNo;

        public int? ParishNo
        {
            get => _parishNo;
            set => RaiseAndSetIfChanged(ref _parishNo, value);
        }

        private string? _postalCode;

        public string? PostalCode
        {
            get => _postalCode;
            set => RaiseAndSetIfChanged(ref _postalCode, value);
        }

        private int? _constituencyNo;

        public int? ConstituencyNo
        {
            get => _constituencyNo;
            set => RaiseAndSetIfChanged(ref _constituencyNo, value);
        }

        private bool? _isBogusNo;

        public bool? IsBogusNo
        {
            get => _isBogusNo;
            set => RaiseAndSetIfChanged(ref _isBogusNo, value);
        }
    }
}
