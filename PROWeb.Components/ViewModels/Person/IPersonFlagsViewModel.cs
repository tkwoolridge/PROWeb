namespace PROWeb.Components.ViewModels.Person
{
    public interface IPersonFlagsViewModel<TPersonFlagViewModel>
        where TPersonFlagViewModel : IPersonFlagViewModel
    {
        bool? CommonwealthCitizen { get; set; }

        DateTime? BermudianStatusGranted { get; set; }

        bool? RegisteredAsElector { get; set; }

        bool? IsBermudianStatusGranted { get; set; }

        List<TPersonFlagViewModel>? Flags { get; set; }
    }
}
