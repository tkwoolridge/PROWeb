using PROWeb.Common.ViewModels;

namespace PROWeb.Components.ViewModels.Assessment
{
    public class AssessmentFlagViewModel : SlimViewModelBase
    {
        public int AssessmentFlagId { get; set; }

        public string? FlagDescription { get; set; }

        public string? Short { get; set; }

        public bool Active { get; set; }
    }
}
