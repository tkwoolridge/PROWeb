using PROWeb.Components.ViewModels.Voter;

namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonFlagsViewModel
    {
        bool? CommonwealthCitizen { get; set; }

        DateTime? BermudianStatusGranted { get; set; }

        bool? RegisteredAsElector { get; set; }

        bool? IsBermudianStatusGranted { get; set; }

        List<VoterFlagViewModel> VoterFlags { get; set; }

        List<int> VoterFlagsValues { get; set; }
    }
}
