using Mapster;
using Microsoft.AspNetCore.Components;
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
        internal AddressContext<TAddressViewModel> AddressContext { get; set; } = new();

        [Parameter]
        public bool ShowPopulateDialogButton { get; set; }

        protected AddressMarker? AddressMarker { get; set; }

        protected int TabIndex { get; set; }

        private readonly Expression<Func<TAddressViewModel, int?>>? _assessmentNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _address1Path;
        private readonly Expression<Func<TAddressViewModel, string?>>? _houseNoPath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _address2Path;
        private readonly Expression<Func<TAddressViewModel, string?>>? _postalCodePath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _parishNamePath;
        private readonly Expression<Func<TAddressViewModel, int?>>? _constituencyNoPath;
        private readonly Expression<Func<TAddressViewModel, double?>>? _longitudePath;
        private readonly Expression<Func<TAddressViewModel, double?>>? _latitudePath;
        private readonly Expression<Func<TAddressViewModel, string?>>? _constituencyNamePath;
        private readonly Expression<Func<TAddressViewModel, bool?>>? _isBogusPath;

        public AddressView(
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
            _assessmentNoPath = assessmentNoPath;
            _address1Path = address1Path;
            _houseNoPath = houseNoPath;
            _address2Path = address2Path;
            _postalCodePath = postalCodePath;
            _parishNamePath = parishNamePath;
            _constituencyNoPath = constituencyNoPath;
            _constituencyNamePath = constituencyNamePath;
            _longitudePath = longitudePath;
            _latitudePath = latitudePath;
            _isBogusPath = isBogusPath;
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            AddressContext.UnBind();
            if (Model is { } model)
            {
                AddressContext.Bind
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
                _longitudePath,
                _latitudePath,
                _isBogusPath
                );
            }

            TabIndex = 0;

            UpdateMap();
        }

        private void UpdateMap()
        {
            if (AddressContext.Longitude is not { } longitude || AddressContext.Latitude is not { } latitude)
            {
                return;
            }

            AddressMarker = new AddressMarker([latitude, longitude], $"{AddressContext.AssessmentNo}: {AddressContext.Address2} {AddressContext.HouseNo}");

            StateHasChanged();
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(AddressContext);
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
            UpdateMap();
        }

        public void OnUpdateAddress(AssessmentViewModel assessment)
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            assessment.Adapt(AddressContext);

            EditContext?.NotifyFieldChanged(new FieldIdentifier(AddressContext, nameof(AddressContext.AssessmentNo)));

            StateHasChanged();
        }

        public void UpdateAddress(AssessmentViewModel assessment)
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            using (UndoService.CreateScope())
            {
                OnUpdateAddress(assessment);
            }
        }
    }
}
