using PROWeb.Common.Components;
using PROWeb.Components.Assessments;
using PROWeb.Components.ViewModels.Assessment;
using System.Diagnostics;

namespace PROWeb.Components.Person
{
    public class AddressViewBase<TAddressViewModel> : PROView<TAddressViewModel>
        where TAddressViewModel : class, IAddressViewModel
    {
        protected AssessmentRegistryDialog? AssessmentRegistryDialogRef { get; set; }

        protected void OnUpdate()
        {
            Debug.Assert(AssessmentRegistryDialogRef != null);

            AssessmentRegistryDialogRef.Show();
        }

        protected void OnAssessmentSelectionConfirm(AssessmentViewModel assessment)
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

            StateHasChanged();
        }
    }
}
