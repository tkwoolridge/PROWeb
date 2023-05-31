using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.ViewModels.Voters;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Components.Voters
{
    public partial class VotersGrid : PROGridComponent<FilterModel, VoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        [Parameter]
        public EventCallback<VoterViewModel> VoterSelected { get; set; }

        [Parameter]
        public bool EnableSelection { get; set; }

        [Parameter]
        public bool ShowEligibleColumn { get; set; } = true;

        protected override void OnSelectionChanged(IEnumerable<VoterViewModel> selectedItems)
        {
            base.OnSelectionChanged(selectedItems);

            if (selectedItems.FirstOrDefault() is { } eligible)
            {
                VoterSelected.InvokeAsync(eligible);
            }
        }

        protected override async Task<IList<VoterViewModel>> GetDataAsync(FilterModel filter)
        {
            using (var service = _votersServiceFactory.CreateService())
            {
                var voters = await service.GetVoters
                    (
                        filter.RegistryYear, 
                        firstName: filter.FirstName, 
                        lastName: filter.LastName, 
                        dateOfBirth: filter.DateOfBirth
                    ).ProjectToListAsync<VoterViewModel>(Mapper);

                return voters;
            }
        }
    }
}
