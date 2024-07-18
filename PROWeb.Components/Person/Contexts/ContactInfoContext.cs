using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class ContactInfoContext<TContactInfoViewModel> : ViewModelContext<TContactInfoViewModel> where TContactInfoViewModel : SlimViewModelBase
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
        [Required(ErrorMessage = "Contact phone is required!")]
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

            using (SuspendSubscriptions())
            {
                AddBinding(Model?.Bind(this, emailPath, c => c.Email, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, contactPhonePath, c => c.ContactPhone, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, phoneHomePath, c => c.PhoneHome, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, phoneWorkPath, c => c.PhoneWork, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, phoneMobilePath, c => c.PhoneMobile, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, driverLicensePath, c => c.DriverLicense, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, commentPath, c => c.Comment, StrongBindingMode.TwoWay));
            }
        }
    }
}
