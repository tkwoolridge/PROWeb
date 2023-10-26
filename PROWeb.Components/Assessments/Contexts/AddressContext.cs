using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Assessments.Contexts
{
    internal class AddressContext<TAddressViewModel> : ViewModelContext<TAddressViewModel> where TAddressViewModel : SlimViewModelBase
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
            set => RaiseAndSetIfChanged(ref _address1, value.ToNullIfWhiteSpace());
        }

        private string? _houseNo;

        public string? HouseNo
        {
            get => _houseNo;
            set => RaiseAndSetIfChanged(ref _houseNo, value.ToNullIfWhiteSpace());
        }

        private string? _address2;

        public string? Address2
        {
            get => _address2;
            set => RaiseAndSetIfChanged(ref _address2, value.ToNullIfWhiteSpace());
        }

        private string? _postalCode;

        public string? PostalCode
        {
            get => _postalCode;
            set => RaiseAndSetIfChanged(ref _postalCode, value.ToNullIfWhiteSpace());
        }

        private string? _parishName;

        public string? ParishName
        {
            get => _parishName;
            set => RaiseAndSetIfChanged(ref _parishName, value.ToNullIfWhiteSpace());
        }

        private string? _countryName;

        public string? CountryName
        {
            get => _countryName;
            set => RaiseAndSetIfChanged(ref _countryName, value);
        }

        private int? _constituencyNo;

        public int? ConstituencyNo
        {
            get => _constituencyNo;
            set => RaiseAndSetIfChanged(ref _constituencyNo, value);
        }

        private string? _constituencyName;

        public string? ConstituencyName
        {
            get => _constituencyName;
            set => RaiseAndSetIfChanged(ref _constituencyName, value.ToNullIfWhiteSpace());
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
            set => RaiseAndSetIfChanged(ref _bogusNo, value.ToNullIfWhiteSpace());
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
            set => RaiseAndSetIfChanged(ref _bogusConstituencyName, value.ToNullIfWhiteSpace());
        }

        public void Bind(
            TAddressViewModel model,
            Expression<Func<TAddressViewModel, int?>>? assessmentNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? address1Path = null,
            Expression<Func<TAddressViewModel, string?>>? houseNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? address2Path = null,
            Expression<Func<TAddressViewModel, string?>>? postalCodePath = null,
            Expression<Func<TAddressViewModel, string?>>? parishNamePath = null,
            Expression<Func<TAddressViewModel, int?>>? constituencyNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? constituencyNamePath = null,
            Expression<Func<TAddressViewModel, bool?>>? isBogusNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? bogusNoPath = null,
            Expression<Func<TAddressViewModel, int?>>? bogusConstituencyNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? bogusConstituencyNamePath = null)
        {
            Model = model;

            using (SuspendSubscriptions())
            {

                AddBinding(Model?.Bind(this, assessmentNoPath, c => c.AssessmentNo, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, address1Path, c => c.Address1, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, houseNoPath, c => c.HouseNo, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, address2Path, c => c.Address2, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, postalCodePath, c => c.PostalCode, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, parishNamePath, c => c.ParishName, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, constituencyNoPath, c => c.ConstituencyNo, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, constituencyNamePath, c => c.ConstituencyName, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, isBogusNoPath, c => c.IsBogusNo, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, bogusNoPath, c => c.BogusNo, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, bogusConstituencyNoPath, c => c.BogusConstituencyNo, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, bogusConstituencyNamePath, c => c.BogusConstituencyName, StrongBindingMode.TwoWay));
            }
        }
    }
}
