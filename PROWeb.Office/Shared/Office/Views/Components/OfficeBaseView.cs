using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Components.Common.Views;
using PROWeb.Office.Shared.Office.ViewModels;
using PROWeb.Office.Shared.Office.Views.Components.Contexts;

namespace PROWeb.Office.Shared.Office.Views
{
    public abstract class OfficeBaseView<TContext> : PROEditableView<OfficeViewModel> where TContext : OfficeBaseContext, new()
    {
        internal TContext OfficeContext { get; set; } = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Editable = true;
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            OfficeContext.UnBind();

            if (Model is { } model)
            {
                OfficeContext.Bind(model);
            }
        }

        protected override EditContext? GetEditContext()
        {
            return new EditContext(OfficeContext);
        }
    }
}
