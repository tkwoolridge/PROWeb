using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.EligiblePoll;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.EligiblePoll.Views
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
