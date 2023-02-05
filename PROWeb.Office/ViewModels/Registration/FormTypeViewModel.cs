using PROWeb.Common.ViewModels;

namespace PROWeb.Office.ViewModels.Registration
{
    public class FormTypeViewModel :SlimViewModelBase
    {
        public int? FormTypeId { get; set; }

        public string? FormName { get; set; }
    }
}
