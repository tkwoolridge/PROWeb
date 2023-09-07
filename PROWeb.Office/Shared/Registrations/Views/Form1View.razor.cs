using PROWeb.Common.Components;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class Form1View : PROComponent
    {
        protected FormAddressView? AddressViewRef { get; set; }

        //protected override void OnPageChanged(int page)
        //{
        //    if (page != 2)
        //    {
        //        return;
        //    }

        //    if (SelectedEligible is { } eligible)
        //    {
        //        Registration = Mapper.Map<RegistrationViewModel>(eligible);
        //    }

        //    if (SelectedAssessment is { } assessment)
        //    {
        //        Registration.Address1 = assessment.Address1;
        //        Registration.Address2 = assessment.Address2;
        //        Registration.HouseNo = assessment.HouseNo;
        //        Registration.ParishName = assessment.ParishName;
        //        Registration.PostalCode = assessment.PostalCode;
        //        Registration.AssessmentNo = assessment.AssessmentNo;
        //        Registration.ConstituencyNo = assessment.ConstituencyNo;
        //        Registration.ConstituencyName = assessment.ConstituencyName;
        //    }
        //}
    }
}
