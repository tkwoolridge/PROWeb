using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Components.EligiblePolls.ViewModels;
using PROWeb.Components.Person.Contexts;
using PROWeb.Components.Voters.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class ContactInfoViewBase<TContactInfoViewModel> : PROEditableView<TContactInfoViewModel> where TContactInfoViewModel : SlimViewModelBase
    {
    }

    public abstract partial class ContactInfoView<TContactInfoViewModel> : ContactInfoViewBase<TContactInfoViewModel> where TContactInfoViewModel : SlimViewModelBase
    {
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _emailPath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _contactPhonePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _phoneHomePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _phoneWorkPath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _phoneMobilePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _driverLicensePath;
        private readonly Expression<Func<TContactInfoViewModel, string?>>? _commentPath;

        internal ContactInfoContext<TContactInfoViewModel> ContactInfoContext { get; set; } = new();

        protected ContactInfoView(
            Expression<Func<TContactInfoViewModel, string?>>? emailPath = null,
            Expression<Func<TContactInfoViewModel, string?>>? contactPhonePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneHomePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneWorkPath = null,
            Expression<Func<TContactInfoViewModel, string?>>? phoneMobilePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? driverLicensePath = null,
            Expression<Func<TContactInfoViewModel, string?>>? commentPath = null)
        {
            _emailPath = emailPath;
            _contactPhonePath = contactPhonePath;
            _phoneHomePath = phoneHomePath;
            _phoneWorkPath = phoneWorkPath;
            _phoneMobilePath = phoneMobilePath;
            _driverLicensePath = driverLicensePath;
            _commentPath = commentPath;
        }

        internal void OnUpdateFromVoter(VoterViewModel voter)
        {
            voter.Adapt(ContactInfoContext);

            NotifyFieldsChanged();
        }

        public void UpdateFromVoter(VoterViewModel voter)
        {
            using (UndoService.CreateScope())
            {
                OnUpdateFromVoter(voter);
            }
        }

        internal void OnUpdateFromEligible(EligibleViewModel eligible)
        {
            ContactInfoContext.DriverLicense = eligible.DriverLicenseId;

            NotifyFieldsChanged();
        }

        public void UpdateFromEligible(EligibleViewModel eligible)
        {
            using (UndoService.CreateScope())
            {
                OnUpdateFromEligible(eligible);
            }
        }

        private void NotifyFieldsChanged()
        {
            EditContext?.NotifyFieldChanged(new FieldIdentifier(ContactInfoContext, nameof(ContactInfoContext.PhoneHome)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(ContactInfoContext, nameof(ContactInfoContext.PhoneWork)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(ContactInfoContext, nameof(ContactInfoContext.PhoneMobile)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(ContactInfoContext, nameof(ContactInfoContext.Email)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(ContactInfoContext, nameof(ContactInfoContext.DriverLicense)));
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(ContactInfoContext);
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            ContactInfoContext.UnBind();
            if (Model is { } model)
            {
                ContactInfoContext.Bind
                (
                model,
                 _emailPath,
                _contactPhonePath,
                _phoneHomePath,
                _phoneWorkPath,
                _phoneMobilePath,
                _driverLicensePath,
                _commentPath
                );
            }
        }
    }
}
