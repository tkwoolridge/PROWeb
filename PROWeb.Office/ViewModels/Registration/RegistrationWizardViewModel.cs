using PROWeb.Common.ViewModels;

namespace PROWeb.Office.ViewModels.Registration
{
    public class RegistrationWizardViewModel : SlimViewModelBase
    {
        private int _registrationType;

        public int RegistrationType
        {
            get => _registrationType;
            set => RaiseAndSetIfChanged(ref _registrationType, value);
        }

        private int _registrationActionId;

        public int RegistrationActionId
        {
            get => _registrationActionId;
            set => RaiseAndSetIfChanged(ref _registrationActionId, value);
        }

        private string? _newLastName;

        public string? NewLastName
        {
            get => _newLastName;
            set => RaiseAndSetIfChanged(ref _newLastName, value);
        }
    }
}
