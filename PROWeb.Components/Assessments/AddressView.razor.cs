using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Assessments.Contexts;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Common.Views;
using System.Diagnostics;
using System.Linq.Expressions;

namespace PROWeb.Components.Assessments
{
    public abstract class AddressViewBase<TAddressViewModel> : PROEditableView<TAddressViewModel>
        where TAddressViewModel : SlimViewModelBase
    {
    }

    public abstract partial class AddressView<TAddressViewModel> : AddressViewBase<TAddressViewModel> where TAddressViewModel : SlimViewModelBase
    {
        internal AddressContext<TAddressViewModel> Context { get; set; } = new();

        private readonly Expression<Func<TAddressViewModel, int?>>? _assessmentNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _address1Path;
        private readonly Expression<Func<TAddressViewModel, string?>>? _houseNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _address2Path;
        private readonly Expression<Func<TAddressViewModel, string?>>? _postalCodePath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _parishNamePath;
        private readonly Expression<Func<TAddressViewModel, int?>>? _constituencyNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _constituencyNamePath;
        private readonly Expression<Func<TAddressViewModel, bool?>>? _isBogusNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _bogusNoPath;
        private readonly Expression<Func<TAddressViewModel, int?>>? _bogusConstituencyNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _bogusConstituencyNamePath;

        public AddressView(
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
            Expression<Func<TAddressViewModel, string?>>? bogusConstituencyNamePath = null
            )
        {
            _assessmentNoPath = assessmentNoPath;
            _address1Path = address1Path;
            _houseNoPath = houseNoPath;
            _address2Path = address2Path;
            _postalCodePath = postalCodePath;
            _parishNamePath = parishNamePath;
            _constituencyNoPath = constituencyNoPath;
            _constituencyNamePath = constituencyNamePath;
            _isBogusNoPath = isBogusNoPath;
            _bogusNoPath = bogusNoPath;
            _bogusConstituencyNoPath = bogusConstituencyNoPath;
            _bogusConstituencyNamePath = bogusConstituencyNamePath;
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
                 _assessmentNoPath,
                _address1Path,
                _houseNoPath,
                _address2Path,
                _postalCodePath,
                _parishNamePath,
                _constituencyNoPath,
                _constituencyNamePath,
                _isBogusNoPath,
                _bogusNoPath,
                _bogusConstituencyNoPath,
                _bogusConstituencyNamePath
                );
            }
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(Context);
        }

        protected AssessmentRegistryDialog? AssessmentRegistryDialogRef { get; set; }

        protected void OnUpdate()
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            AssessmentRegistryDialogRef.Show();
        }

        protected void OnAssessmentSelectionConfirm(AssessmentViewModel assessment)
        {
            UpdateAddress(assessment);
        }

        public void UpdateAddress(AssessmentViewModel assessment)
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            Context.AssessmentNo = assessment.AssessmentNo;
            Context.Address1 = assessment.Address1;
            Context.Address2 = assessment.Address2;
            Context.HouseNo = assessment.HouseNo;
            Context.PostalCode = assessment.PostalCode;
            Context.ParishName = assessment.ParishName;
            Context.ConstituencyNo = assessment.ConstituencyNo;
            Context.ConstituencyName = assessment.ConstituencyName;

            EditContext?.NotifyFieldChanged(new FieldIdentifier(Context, nameof(Context.AssessmentNo)));

            StateHasChanged();
        }
    }
}
