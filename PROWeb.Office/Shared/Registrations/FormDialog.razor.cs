using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Services.Undo;
using PROWeb.Office.Registrations.Views;
using PROWeb.Office.Shared.Registrations.ViewModels;
using PROWeb.Office.Shared.Registrations.Views;

namespace PROWeb.Office.Shared.Registrations
{
    public partial class FormDialog : PROComponent
    {
        [Inject]
        protected IUndoService<RegistrationViewModel> UndoService { get; set; } = default!;

        protected FormOldAddressView? OldAddressViewRef { get; set; }

        protected FormAddressView? AddressViewRef { get; set; }

        protected FormContactInfoView? ContactInfoViewRef { get; set; }

        protected FormFlagsView? FormFlagsViewRef { get; set; }

        protected bool ShowDialog { get; set; }

        [Parameter]
        public RegistrationViewModel? Model { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public void OnSave() 
        { 
            ShowDialog = false;
        }

        public void Show()
        {
            ShowDialog = true;
        }

        protected void OnClose()
        {
            ShowDialog = false;
        }
    }
}
