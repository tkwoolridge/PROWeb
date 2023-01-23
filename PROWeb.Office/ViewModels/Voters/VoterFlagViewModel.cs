using PROWeb.Common.ViewModels;

namespace PROWeb.Office.ViewModels.Voters
{
    public class VoterFlagViewModel : SlimViewModelBase
    {
        public int FlagId { get; set; }

        public string? FlagDescription { get; set; }
    }
}
