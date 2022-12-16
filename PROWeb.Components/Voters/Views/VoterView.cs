using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.ViewModels.Voter;

namespace PROWeb.Components.Voters.Views
{
    public abstract class VoterView<TVoterViewModel> : PROComponent 
        where TVoterViewModel : IVoterViewModel
    {
        [Parameter]
        public TVoterViewModel DetailsContext { get; set; } = default!;

        [Parameter]
        public bool Editable { get; set; } = false;
    }
}
