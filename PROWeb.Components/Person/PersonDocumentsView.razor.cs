using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.Person
{
    public class PersonDocumentsViewBase<TPersonDocumentsViewModel> : PROView<TPersonDocumentsViewModel> where TPersonDocumentsViewModel : class, IPersonDocumentsViewModel
    {
    }
}
