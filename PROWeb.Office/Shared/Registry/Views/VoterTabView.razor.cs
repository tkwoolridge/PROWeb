using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Services.State;
using PROWeb.Components.Voters;
using PROWeb.Components.Voters.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Registry.Views
{
    public partial class VoterTabView : VoterViewBase<VoterViewModel>, IPROComponentWithState<int>
    {
        private int _tabIndex = 0;

        [Inject]
        protected IStateService<VoterTabView, int> StateService { get; set; } = null!;

        protected VoterHistoryDialog? VoterHistoryDialogRef { get; set; }

        protected string PersistenceKey => LayoutRef?.Model?.VoterId.ToString() ?? "0";

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

        public async Task OnEditAsync(ListViewCommandEventArgs e)
        {
            bool result = await OnSaveAsync();

            e.IsCancelled = !result;
        }

        public void OnShowHistory()
        {
            VoterHistoryDialogRef?.Show();
        }

        public bool PersistState { get; set; } = true;

        public void ResetState()
        {
            if (PersistState)
            {
                StateService.Clear();
            }
        }
    }
}
