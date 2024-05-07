using PROWeb.Common.ViewModels;
using PROWeb.Office.Shared.Office.ViewModels;

namespace PROWeb.Office.Shared.Office.Views.Components.Contexts
{
    public abstract class OfficeBaseContext : ViewModelContext<OfficeViewModel>
    {
        public abstract void Bind(OfficeViewModel model);
    }
}
