using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Data.Models;
using PROWeb.Data.Services.EligiblePoll;
using PROWeb.Office.ViewModels.EligiblePolls;

namespace PROWeb.Office.Components.EligiblePolls
{
    public partial class EligibleGrid : PROListComponent<FilterModel, EligibleViewModel>
    {
        [Inject]
        private IEligiblePollServiceFactory _candidatesServiceFactory { get; set; } = null!;

        [Parameter]
        public EventCallback<EligibleViewModel> EligibleSelected { get; set; }

        [Parameter]
        public bool EnableSelection { get; set; }

        protected override void OnSelectionChanged(IEnumerable<EligibleViewModel> selectedItems)
        {
            base.OnSelectionChanged(selectedItems);

            if(selectedItems.FirstOrDefault() is { } eligible)
            {
                EligibleSelected.InvokeAsync(eligible);
            }
        }

        protected override async Task<IList<EligibleViewModel>> GetDataAsync(FilterModel filter)
        {
            using (var service = _candidatesServiceFactory.CreateService())
            {
                var candidates = await service.GetVoterCandidatesAsync(filter.FirstName, filter.LastName, filter.DateOfBirth);
                return candidates.ProjectToList<Eligible, EligibleViewModel>(Mapper);
            }
        }
    }
}
