using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.Person
{
    public abstract class ContactInfoViewBase<TContactInfoViewModel> : PROView<TContactInfoViewModel> where TContactInfoViewModel : class, IContactInfoViewModel
    {
    }
}
