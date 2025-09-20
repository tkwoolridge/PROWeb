using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Services.Undo;
using PROWeb.Office.Registrations.Views;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class Form2View : PROComponent
    {
        [Inject]
        protected IUndoService<RegistrationViewModel> UndoService { get; set; } = default!;

        [Parameter]
        public EventCallback OnSave { get; set; }

        protected FormOldAddressView? OldAddressViewRef { get; set; }

        protected FormAddressView? AddressViewRef { get; set; }

        protected FormContactInfoView? ContactInfoViewRef { get; set; }

        protected FormFlagsView? FormFlagsViewRef { get; set; }

        [Parameter]
        public RegistrationViewModel? Model { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
