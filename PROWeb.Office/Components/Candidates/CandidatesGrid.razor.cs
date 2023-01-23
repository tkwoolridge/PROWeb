using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Candidates;
using PROWeb.Office.ViewModels.Candidates;

namespace PROWeb.Office.Components.Candidates
{
    public partial class CandidatesGrid : PROListComponent<FilterModel, CandidateViewModel>
    {
        [Inject]
        private ICandidatesServiceFactory _candidatesServiceFactory { get; set; } = null!;

        protected override async Task<IList<CandidateViewModel>> OnFilterAsync(FilterModel filter)
        {
            Page = 1;

            using (var service = _candidatesServiceFactory.CreateService())
            {
                var candidates = await service.GetCandidatesAsync(filter.FirstName, filter.LastName, filter.DateOfBirth);
                return candidates.ProjectToList<Candidate, CandidateViewModel>(Mapper);
            }
        }
    }
}
