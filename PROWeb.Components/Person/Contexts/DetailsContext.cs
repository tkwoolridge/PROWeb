using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.Extensions;
using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class DetailsContext<TPersonViewModel> : BindableContext<TPersonViewModel> where TPersonViewModel : SlimViewModelBase
    {
        private int? _personId;

        public int? PersonId
        {
            get => _personId;
            set => RaiseAndSetIfChanged(ref _personId, value);
        }

        private string? _title;

        public string? Title
        {
            get => _title;
            set => RaiseAndSetIfChanged(ref _title, value.ToNullIfWhiteSpace());
        }

        private string? _firstName;

        [Required(ErrorMessage = "First name is requred!")]
        public string? FirstName
        {
            get => _firstName;
            set => RaiseAndSetIfChanged(ref _firstName, value.ToNullIfWhiteSpace());
        }

        private string? _lastName;

        [Required(ErrorMessage = "Last name is requred!")]
        public string? LastName
        {
            get => _lastName;
            set => RaiseAndSetIfChanged(ref _lastName, value.ToNullIfWhiteSpace());
        }

        private string? _maidenName;

        public string? MaidenName
        {
            get => _maidenName;
            set => RaiseAndSetIfChanged(ref _maidenName, value.ToNullIfWhiteSpace());
        }

        private string? _middleName;

        public string? MiddleName
        {
            get => _middleName;
            set => RaiseAndSetIfChanged(ref _middleName, value.ToNullIfWhiteSpace());
        }

        private char? _gender;

        public char? Gender
        {
            get => _gender;
            set => RaiseAndSetIfChanged(ref _gender, value);
        }

        private DateTime? _dateOfBirth;

        [Required(ErrorMessage = "DOB is required!")]
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => RaiseAndSetIfChanged(ref _dateOfBirth, value);
        }

        private IDisposable? _personIdBinding;
        private IDisposable? _titleIdBinding;
        private IDisposable? _firstNameBinding;
        private IDisposable? _lastNameBinding;
        private IDisposable? _middleNameBinding;
        private IDisposable? _maidenNameBinding;
        private IDisposable? _genderBinding;
        private IDisposable? _dateOfBirthBinding;

        public void Bind(
            TPersonViewModel? model,
            Expression<Func<TPersonViewModel, int?>>? personIdPath = null,
            Expression<Func<TPersonViewModel, string?>>? titlePath = null,
            Expression<Func<TPersonViewModel, string?>>? firstNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? lastNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? middleNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? maidenNamePath = null,
            Expression<Func<TPersonViewModel, char?>>? genderPath = null,
            Expression<Func<TPersonViewModel, DateTime?>>? dateOfBirthPath = null)
        {
            Model = model;

            _personIdBinding = Model?.Bind(this, personIdPath, c => c.PersonId, StrongBindingMode.TwoWay);
            _titleIdBinding = Model?.Bind(this, titlePath, c => c.Title, StrongBindingMode.TwoWay);
            _firstNameBinding = Model?.Bind(this, firstNamePath, c => c.FirstName, StrongBindingMode.TwoWay);
            _lastNameBinding = Model?.Bind(this, lastNamePath, c => c.LastName, StrongBindingMode.TwoWay);
            _middleNameBinding = Model?.Bind(this, middleNamePath, c => c.MiddleName, StrongBindingMode.TwoWay);
            _genderBinding = Model?.Bind(this, genderPath, c => c.Gender, StrongBindingMode.TwoWay);
            _maidenNameBinding = Model?.Bind(this, maidenNamePath, c => c.MaidenName, StrongBindingMode.TwoWay);
            _dateOfBirthBinding = Model?.Bind(this, dateOfBirthPath, c => c.DateOfBirth, StrongBindingMode.TwoWay);
        }

        public override void UnBind()
        {
            _personIdBinding?.Dispose();
            _titleIdBinding?.Dispose();
            _firstNameBinding?.Dispose();
            _lastNameBinding?.Dispose();
            _genderBinding?.Dispose();
            _dateOfBirthBinding?.Dispose();
            _middleNameBinding?.Dispose();
            _maidenNameBinding?.Dispose();
        }
    }
}
