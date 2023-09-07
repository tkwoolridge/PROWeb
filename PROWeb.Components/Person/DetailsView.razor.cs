using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Components.EligiblePolls;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Person.Contexts;
using System.Diagnostics;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class DetailsViewBase<TPersonViewModel> : PROEditableView<TPersonViewModel> where TPersonViewModel : SlimViewModelBase
    {
    }

    public abstract partial class DetailsView<TPersonViewModel> : DetailsViewBase<TPersonViewModel>
        where TPersonViewModel : SlimViewModelBase
    {
        [Parameter]
        public bool ShowMaidenName { get; set; } = true;

        [Parameter]
        public int RowCount { get; set; } = 7;

        protected EligiblePollsDialog? DetailsRegistryDialogRef { get; set; }

        internal DetailsContext<TPersonViewModel> Context { get; set; } = new();

        private string _viewClass => ShowMaidenName ? "person-details-view" : "person-details-view-no-maiden-name";
        private readonly Expression<Func<TPersonViewModel, int?>>? _personIdPath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _titlePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _firstNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _lastNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _middleNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _maidenNamePath;
        private readonly Expression<Func<TPersonViewModel, char?>>? _genderPath;
        private readonly Expression<Func<TPersonViewModel, DateTime?>>? _dateOfBirthPath;

        protected DetailsView(
            Expression<Func<TPersonViewModel, int?>>? personIdPath = null,
            Expression<Func<TPersonViewModel, string?>>? titlePath = null,
            Expression<Func<TPersonViewModel, string?>>? firstNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? lastNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? middleNamePath = null,
            Expression<Func<TPersonViewModel, string?>>? maidenNamePath = null,
            Expression<Func<TPersonViewModel, char?>>? genderPath = null,
            Expression<Func<TPersonViewModel, DateTime?>>? dateOfBirthPath = null)
        {
            _personIdPath = personIdPath;
            _titlePath = titlePath;
            _firstNamePath = firstNamePath;
            _lastNamePath = lastNamePath;
            _middleNamePath = middleNamePath;
            _maidenNamePath = maidenNamePath;
            _genderPath = genderPath;
            _dateOfBirthPath = dateOfBirthPath;
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(Context);
        }

        protected void OnUpdate()
        {
            Debug.Assert(DetailsRegistryDialogRef != null) ;

            DetailsRegistryDialogRef.Show();
        }

        public virtual void UpdateFromEligible(EligibleViewModel eligible)
        {
            Debug.Assert(DetailsRegistryDialogRef != null);

            if (eligible.ImmigrationId != null)
            {
                Context.Gender = eligible.ImmigrationGender;
                Context.FirstName = eligible.ImmigrationFirstName;
                Context.LastName = eligible.ImmigrationLastName;
                Context.MiddleName = eligible.ImmigrationMiddleName;
                Context.DateOfBirth = eligible.ImmigrationDateOfBirth;
            }
            else if (eligible.BirthId != null)
            {
                Context.Gender = eligible.BirthGender;
                Context.FirstName = eligible.BirthFirstName;
                Context.LastName = eligible.BirthLastName;
                Context.MiddleName = eligible.BirthMiddleName;
                Context.DateOfBirth = eligible.BirthDateOfBirth;
            }
            else if (eligible.DriverLicenseId != null)
            {
                Context.Gender = eligible.DriverLicenseGender;
                Context.FirstName = eligible.DriverLicenseFirstName;
                Context.LastName = eligible.DriverLicenseLastName;
                Context.MiddleName = eligible.DriverLicenseMiddleName;
                Context.DateOfBirth = eligible.DriverLicenseDateOfBirth;
            }

            EditContext?.NotifyFieldChanged(new FieldIdentifier(Context, nameof(Context.Gender)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(Context, nameof(Context.FirstName)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(Context, nameof(Context.LastName)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(Context, nameof(Context.MiddleName)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(Context, nameof(Context.DateOfBirth)));
        }

        protected void OnEligibleSelectionConfirm(EligibleViewModel eligible)
        {
            UpdateFromEligible(eligible);

            StateHasChanged();
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            Context.UnBind();
            if (Model is { } model)
            {
                Context.Bind
                (
                model,
                _personIdPath,
                _titlePath,
                _firstNamePath,
                _lastNamePath,
                _middleNamePath,
                _maidenNamePath,
                _genderPath,
                _dateOfBirthPath
                );
            }
        }
    }
}
