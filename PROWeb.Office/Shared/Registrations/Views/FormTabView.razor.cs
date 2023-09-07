using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormTabView : PROCompositView<ListRegistrationViewModel>
    {
        [Parameter]
        public bool Editable { get; set; }

        [CascadingParameter]
        public ListRegistrationViewModel? Model { get; set; }

        protected int TabIndex
        {
            get => LayoutRef?.Model?.TabIndex ?? 0;
            set
            {
                if (LayoutRef?.Model is { } model)
                {
                    model.TabIndex = value;
                }
            }
        }

        protected override async Task SaveAsync(ListRegistrationViewModel model)
        {
            await Task.CompletedTask;
            //if (ViewsLayoutRef is { } layout)
            //{
            //    e.IsCancelled = !(await layout.OnSave());
            //}
        }

        protected void OnApprove()
        {
        }

        protected void OnReject()
        {
        }
    }
}
