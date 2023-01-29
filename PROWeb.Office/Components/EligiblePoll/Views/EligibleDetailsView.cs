using PROWeb.Components.Person;
using PROWeb.Office.ViewModels.EligiblePoll;
using System.Linq.Expressions;

namespace PROWeb.Office.Components.EligiblePoll.Views
{
    public abstract class EligibleDetailsView : DetailsView<EligibleViewModel>
    {
        protected EligibleDetailsView(
            Expression<Func<EligibleViewModel, int?>>? personIdPath = null,
            Expression<Func<EligibleViewModel, string?>>? titlePath = null,
            Expression<Func<EligibleViewModel, string?>>? firstNamePath = null,
            Expression<Func<EligibleViewModel, string?>>? lastNamePath = null,
            Expression<Func<EligibleViewModel, string?>>? middleNamePath = null,
            Expression<Func<EligibleViewModel, char?>>? genderPath = null,
            Expression<Func<EligibleViewModel, DateTime?>>? dateOfBirthPath = null)
            : base(personIdPath,
                  titlePath,
                  firstNamePath,
                  lastNamePath,
                  middleNamePath,
                  null,
                  genderPath,
                  dateOfBirthPath)
        {
            ShowMaidenName = false;
            RowCount = 5;
        }
    }
}
