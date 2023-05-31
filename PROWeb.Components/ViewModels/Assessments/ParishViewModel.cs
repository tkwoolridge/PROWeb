using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Assessments
{
    public class ParishViewModel : SlimViewModelBase
    {
        public int ParishNo { get; set; }

        public string? ParishName { get; set; }

        public bool Active { get; set; }
    }
}
