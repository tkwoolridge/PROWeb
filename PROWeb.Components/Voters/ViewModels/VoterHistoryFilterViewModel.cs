using PROWeb.Common.ViewModels;

namespace PROWeb.Components.Voters.ViewModels
{
    public class VoterHistoryFilterViewModel : SlimViewModelBase
    {
        public List<VoterHistoryViewModel>? Histories{ get; set; }
    }
}
