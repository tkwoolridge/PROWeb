namespace PROWeb.Components.EligiblePolls.Views
{
    public class ImmigrationDetailsView : EligibleDetailsView
    {
        public ImmigrationDetailsView()
            : base(
                e => e.ImmigrationId,
                null,
                e => e.ImmigrationFirstName,
                e => e.ImmigrationLastName,
                e => e.ImmigrationMiddleName,
                e => e.ImmigrationGender,
                e => e.ImmigrationDateOfBirth)
        {
        }
    }
}
