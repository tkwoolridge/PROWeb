using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Services.Undo;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations
{
    public partial class FormDialog : PROComponent
    {
        [Inject]
        protected IUndoService<RegistrationViewModel> UndoService { get; set; } = default!;

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
