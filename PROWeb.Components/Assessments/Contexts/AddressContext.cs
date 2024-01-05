using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PROWeb.Components.Assessments.Contexts
{
    internal class AddressContext<TAddressViewModel> : ViewModelContext<TAddressViewModel> where TAddressViewModel : SlimViewModelBase
    {
        private int? _assessmentNo;

        [Required(ErrorMessage = "Assessment no is required!")]
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

        private double? _longitude;

        public double? Longitude
        {
            get => _longitude;
            set => RaiseAndSetIfChanged(ref _longitude, value);
        }

        private double? _latitude;

        public double? Latitude
        {
            get => _latitude;
            set => RaiseAndSetIfChanged(ref _latitude, value);
        }

        private bool? _isBogus;

        public bool? IsBogus
        {
            get => _isBogus;
            set => RaiseAndSetIfChanged(ref _isBogus, value);
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
            Expression<Func<TAddressViewModel, double?>>? longitudePath = null,
            Expression<Func<TAddressViewModel, double?>>? latitudePath = null,
            Expression<Func<TAddressViewModel, bool?>>? isBogusPath = null)
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
                AddBinding(Model?.Bind(this, longitudePath, c => c.Longitude, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, latitudePath, c => c.Latitude, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, isBogusPath, c => c.IsBogus, StrongBindingMode.TwoWay));
            }
        }
    }
}
