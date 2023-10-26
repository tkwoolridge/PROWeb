using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Services.State;

namespace PROWeb.Office.Shared.Registrations.Views
{
    public partial class FormTabView : FormViewBase, IPROComponentWithState<int>
    {
        private int _tabIndex = 0;

        [Parameter]
        public bool Editable { get; set; }

        [Inject]
        protected IStateService<FormTabView, int> StateService { get; set; } = null!;

        protected string PersistenceKey => Model?.VoterId.ToString() ?? "0";

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
