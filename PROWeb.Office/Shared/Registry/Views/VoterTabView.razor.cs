using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Components.Services.State;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Office.Shared.Registry.Views
{
    public partial class VoterTabView : PROCompositeView<VoterViewModel>, IPROComponentWithState<int>
    {
        private int _tabIndex = 0;

        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        [Inject]
        protected IStateService<VoterTabView, int> StateService { get; set; } = null!;

        protected string PersistenceKey => LayoutRef?.Model?.VoterId.ToString() ?? "0";

        [Parameter]
        public bool Editable { get; set; }

        protected int TabIndex
        {
            get
            {
                _tabIndex = PersistState ? StateService?.GetState(PersistenceKey) ?? 0 : _tabIndex;

                return _tabIndex;
            }
            set
            {
                if (_tabIndex != value)
                {
                    _tabIndex = value;
                    if (PersistState)
                    {
                        StateService.SetState(PersistenceKey, _tabIndex);
                    }
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public bool PersistState { get; set; } = true;


        protected override async Task SaveAsync(VoterViewModel model)
        {
            Voter? voter = LayoutRef?.Model?.Adapt<Voter>();

            if (voter == null) { return; }

            using (var service = _voterServiceFactory.CreateService())
            {
                await service.UpdateVoter(voter, "pro1");
            }
        }

        public void ResetState()
        {
            if (PersistState)
            {
                StateService.Clear();
            }
        }
    }
}
