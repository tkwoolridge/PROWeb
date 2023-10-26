using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Components.EligiblePolls;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Person.Contexts;
using PROWeb.Components.Voters;
using PROWeb.Components.Voters.ViewModels;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace PROWeb.Components.Person
{
    public enum PopulateDetailsDialogs
    {
        EligiblePolls,
        Voters
    }

    public abstract class DetailsViewBase<TPersonViewModel> : PROEditableView<TPersonViewModel> where TPersonViewModel : SlimViewModelBase
    {
    }

    public abstract partial class DetailsView<TPersonViewModel> : DetailsViewBase<TPersonViewModel>
        where TPersonViewModel : SlimViewModelBase
    {
        [Parameter]
        public bool ShowMaidenName { get; set; }

        [Parameter]
        public int RowCount { get; set; }

        [Parameter]
        public PopulateDetailsDialogs PopulateDialog { get; set; } 

        [Parameter]
        public bool ShowPopulateDialogButton { get; set; } 

        protected EligiblePollsDialog? EligiblePollsDialogRef { get; set; }

        protected VoterRegistryDialog? VoterRegistryDialogRef { get; set; }

        internal DetailsContext<TPersonViewModel> DetailsContext { get; set; } = new();

        private string _viewClass => ShowMaidenName ? "person-details-view" : "person-details-view-no-maiden-name";
        private readonly Expression<Func<TPersonViewModel, int?>>? _personIdPath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _titlePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _firstNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _lastNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _middleNamePath;
        private readonly Expression<Func<TPersonViewModel, string?>>? _maidenNamePath;
        private readonly Expression<Func<TPersonViewModel, char?>>? _genderPath;
        private readonly Expression<Func<TPersonViewModel, DateTime?>>? _dateOfBirthPath;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            PopulateDialog = PopulateDetailsDialogs.EligiblePolls;
            ShowMaidenName = true;
            RowCount = 7;
        }

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
            return new EditContext(DetailsContext);
        }

        protected void OnUpdate()
        {
            switch(PopulateDialog)
            {
                case PopulateDetailsDialogs.EligiblePolls:
                    Debug.Assert(EligiblePollsDialogRef != null);
                    EligiblePollsDialogRef.Show();
                    break;
                case PopulateDetailsDialogs.Voters:
                    Debug.Assert(VoterRegistryDialogRef != null);
                    VoterRegistryDialogRef.Show();
                    break;
            }
            
        }

        protected virtual void OnUpdateFromEligible(EligibleViewModel eligible)
        {
            if (eligible.ImmigrationId != null)
            {
                DetailsContext.Gender = eligible.ImmigrationGender;
                DetailsContext.FirstName = eligible.ImmigrationFirstName;
                DetailsContext.LastName = eligible.ImmigrationLastName;
                DetailsContext.MiddleName = eligible.ImmigrationMiddleName;
                DetailsContext.DateOfBirth = eligible.ImmigrationDateOfBirth;
            }
            else if (eligible.BirthId != null)
            {
                DetailsContext.Gender = eligible.BirthGender;
                DetailsContext.FirstName = eligible.BirthFirstName;
                DetailsContext.LastName = eligible.BirthLastName;
                DetailsContext.MiddleName = eligible.BirthMiddleName;
                DetailsContext.DateOfBirth = eligible.BirthDateOfBirth;
            }
            else if (eligible.DriverLicenseId != null)
            {
                DetailsContext.Gender = eligible.DriverLicenseGender;
                DetailsContext.FirstName = eligible.DriverLicenseFirstName;
                DetailsContext.LastName = eligible.DriverLicenseLastName;
                DetailsContext.MiddleName = eligible.DriverLicenseMiddleName;
                DetailsContext.DateOfBirth = eligible.DriverLicenseDateOfBirth;
            }

            NotifyFieldsChanged();
        }

        protected virtual void UpdateFromEligible(EligibleViewModel eligible)
        {
            using (UndoService.CreateScope())
            {
                OnUpdateFromEligible(eligible);
            }
        }

        protected virtual void OnUpdateFromVoter(VoterViewModel voter)
        {
            voter.Adapt(DetailsContext);

            NotifyFieldsChanged();
        }

        protected virtual void UpdateFromVoter(VoterViewModel voter)
        {
            using (UndoService.CreateScope())
            {
                OnUpdateFromVoter(voter);
            }
        }

        private void NotifyFieldsChanged()
        {
            EditContext?.NotifyFieldChanged(new FieldIdentifier(DetailsContext, nameof(DetailsContext.Gender)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(DetailsContext, nameof(DetailsContext.FirstName)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(DetailsContext, nameof(DetailsContext.LastName)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(DetailsContext, nameof(DetailsContext.MiddleName)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(DetailsContext, nameof(DetailsContext.DateOfBirth)));
        }

        protected void OnEligibleSelectionConfirm(EligibleViewModel eligible)
        {
            UpdateFromEligible(eligible);

            StateHasChanged();
        }

        protected void OnVoterSelectionConfirm(VoterViewModel voter)
        {
            UpdateFromVoter(voter);

            StateHasChanged();
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            DetailsContext.UnBind();
            if (Model is { } model)
            {
                DetailsContext.Bind
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
