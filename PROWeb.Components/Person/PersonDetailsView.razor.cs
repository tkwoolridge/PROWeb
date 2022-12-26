using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.Person
{
    public class PersonDetailsViewBase<TPersonViewModel> : PROView<TPersonViewModel> where TPersonViewModel : class, IPersonDetailsViewModel
    {
    }
}
