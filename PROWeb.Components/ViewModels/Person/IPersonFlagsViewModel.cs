using PROWeb.Components.ViewModels.Voter;

namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonFlagsViewModel<TPersonFlagViewModel>
        where TPersonFlagViewModel : IPersonFlagViewModel
    {
        bool? CommonwealthCitizen { get; set; }

        DateTime? BermudianStatusGranted { get; set; }

        bool? RegisteredAsElector { get; set; }

        bool? IsBermudianStatusGranted { get; set; }

        List<TPersonFlagViewModel> PersonFlags { get; set; }

        List<int> PersonFlagsValues { get; set; }
    }
}
