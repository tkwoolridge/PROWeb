using PROWeb.Common.StrongBindings;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class AddressContext<TAddressViewModel> : BindableContext<TAddressViewModel> where TAddressViewModel : SlimViewModelBase
    {
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

        private IDisposable? _assessmentNoBinding;
        private IDisposable? _address1Binding;
        private IDisposable? _houseNoBinding;
        private IDisposable? _address2Binding;
        private IDisposable? _postalCodeBinding;
        private IDisposable? _parishNameBinding;
        private IDisposable? _constituencyNoBinding;
        private IDisposable? _constituencyNameBinding;
        private IDisposable? _isBogusNoBinding;
        private IDisposable? _bogusNoBinding;
        private IDisposable? _bogusConstituencyNoBinding;
        private IDisposable? _bogusConstituencyNameBinding;

        public void Bind(
            TAddressViewModel model,
            Expression<Func<TAddressViewModel, int>>? assessmentNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? address1Path = null,
            Expression<Func<TAddressViewModel, string?>>? houseNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? address2Path = null,
            Expression<Func<TAddressViewModel, string?>>? postalCodePath = null,
            Expression<Func<TAddressViewModel, string?>>? parishNamePath = null,
            Expression<Func<TAddressViewModel, int>>? constituencyNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? constituencyNamePath = null,
            Expression<Func<TAddressViewModel, bool?>>? isBogusNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? bogusNoPath = null,
            Expression<Func<TAddressViewModel, int?>>? bogusConstituencyNoPath = null,
            Expression<Func<TAddressViewModel, string?>>? bogusConstituencyNamePath = null)
        {
            Model = model;

            _assessmentNoBinding = Model?.Bind(this, assessmentNoPath, c => c.AssessmentNo, StrongBindingMode.TwoWay);
            _address1Binding = Model?.Bind(this, address1Path, c => c.Address1, StrongBindingMode.TwoWay);
            _houseNoBinding = Model?.Bind(this, houseNoPath, c => c.HouseNo, StrongBindingMode.TwoWay);
            _address2Binding = Model?.Bind(this, address2Path, c => c.Address2, StrongBindingMode.TwoWay);
            _postalCodeBinding = Model?.Bind(this, postalCodePath, c => c.PostalCode, StrongBindingMode.TwoWay);
            _parishNameBinding = Model?.Bind(this, parishNamePath, c => c.ParishName, StrongBindingMode.TwoWay);
            _constituencyNoBinding = Model?.Bind(this, constituencyNoPath, c => c.ConstituencyNo, StrongBindingMode.TwoWay);
            _constituencyNameBinding = Model?.Bind(this, constituencyNamePath, c => c.ConstituencyName, StrongBindingMode.TwoWay);
            _isBogusNoBinding = Model?.Bind(this, isBogusNoPath, c => c.IsBogusNo, StrongBindingMode.TwoWay);
            _bogusNoBinding = Model?.Bind(this, bogusNoPath, c => c.BogusNo, StrongBindingMode.TwoWay);
            _bogusConstituencyNoBinding = Model?.Bind(this, bogusConstituencyNoPath, c => c.BogusConstituencyNo, StrongBindingMode.TwoWay);
            _bogusConstituencyNameBinding = Model?.Bind(this, bogusConstituencyNamePath, c => c.BogusConstituencyName, StrongBindingMode.TwoWay);
        }

        public override void UnBind()
        {
            _assessmentNoBinding?.Dispose();
            _address1Binding?.Dispose();
            _houseNoBinding?.Dispose();
            _address2Binding?.Dispose();
            _postalCodeBinding?.Dispose();
            _parishNameBinding?.Dispose();
            _constituencyNoBinding?.Dispose();
            _constituencyNameBinding?.Dispose();
            _isBogusNoBinding?.Dispose();
            _bogusNoBinding?.Dispose();
            _bogusConstituencyNoBinding?.Dispose();
            _bogusConstituencyNameBinding?.Dispose();
            
        }
    }
}
