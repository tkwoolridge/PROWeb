using Newtonsoft.Json;
using PROWeb.Common.Converters;
using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class ContactInfoContext<TContactInfoViewModel> : BindableContext<TContactInfoViewModel> where TContactInfoViewModel : SlimViewModelBase
    {
        private string? _email;

        [EmailAddress(ErrorMessage = "Voter email is not in correct format!")]
        public string? Email
        {
            get => _email;
            set => RaiseAndSetIfChanged(ref _email, value.ToNullIfWhiteSpace());
        }

        private string? _contactPhone;

        [Phone(ErrorMessage = "Contact phone is not in correct format!")]
        public string? ContactPhone
        {
            get => _contactPhone;
            set => RaiseAndSetIfChanged(ref _contactPhone, value.ToNullIfWhiteSpace());
        }

        private string? _phoneHome;

        public string? PhoneHome
        {
            get => _phoneHome;
            set => RaiseAndSetIfChanged(ref _phoneHome, value.ToNullIfWhiteSpace());
        }

        private string? _phoneWork;

        public string? PhoneWork
        {
            get => _phoneWork;
            set => RaiseAndSetIfChanged(ref _phoneWork, value.ToNullIfWhiteSpace());
        }

        private string? _phoneMobile;

        public string? PhoneMobile
        {
            get => _phoneMobile;
            set => RaiseAndSetIfChanged(ref _phoneMobile, value.ToNullIfWhiteSpace());
        }

        private string? _driverLicense;

        public string? DriverLicense
        {
            get => _driverLicense;
            set => RaiseAndSetIfChanged(ref _driverLicense, value.ToNullIfWhiteSpace());
        }

        private string? _comment;

        public string? Comment
        {
            get => _comment;
            set => RaiseAndSetIfChanged(ref _comment, value.ToNullIfWhiteSpace());
        }

        private IDisposable? _emailBinding;
        private IDisposable? _contactPhoneBinding;
        private IDisposable? _phoneHomeBinding;
        private IDisposable? _phoneWorkBinding;
        private IDisposable? _phoneMobileBinding;
        private IDisposable? _driverLicenseBinding;
        private IDisposable? _commentBinding;

        public void Bind(
            TContactInfoViewModel model,
            Expression<Func<TContactInfoViewModel, string?>>? emailPath = null,
            Expression<Func<TContactInfoViewModel, string?>>? contactPhonePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneHomePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneWorkPath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneMobilePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? driverLicensePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? commentPath = null)
        { 
            Model = model;

            _emailBinding = Model?.Bind(this, emailPath, c => c.Email, StrongBindingMode.TwoWay);
            _contactPhoneBinding = Model?.Bind(this, contactPhonePath, c => c.ContactPhone, StrongBindingMode.TwoWay);
            _phoneHomeBinding = Model?.Bind(this, phoneHomePath, c => c.PhoneHome, StrongBindingMode.TwoWay);
            _phoneWorkBinding = Model?.Bind(this, phoneWorkPath, c => c.PhoneWork, StrongBindingMode.TwoWay);  
            _phoneMobileBinding = Model?.Bind(this, phoneMobilePath, c => c.PhoneMobile, StrongBindingMode.TwoWay);
            _driverLicenseBinding = Model?.Bind(this, driverLicensePath, c => c.DriverLicense, StrongBindingMode.TwoWay);
            _commentBinding = Model?.Bind(this, commentPath, c => c.Comment, StrongBindingMode.TwoWay);
        }

        public override void UnBind()
        {
            _emailBinding?.Dispose();
            _contactPhoneBinding?.Dispose();
            _phoneHomeBinding?.Dispose();
            _phoneWorkBinding?.Dispose();
            _phoneMobileBinding?.Dispose();
            _driverLicenseBinding?.Dispose();
            _commentBinding?.Dispose();
        }
    }
}
