namespace PROWeb.Components.EligiblePolls.Views
{
    public class BirthDetailsView : EligibleDetailsView
    {
        public BirthDetailsView()
            : base(
                e => e.BirthId,
                null,
                e => e.BirthFirstName,
                e => e.BirthLastName,
                e => e.BirthMiddleName,
                e => e.BirthGender,
                e => e.BirthDateOfBirth)
        {
        }
    }
}
