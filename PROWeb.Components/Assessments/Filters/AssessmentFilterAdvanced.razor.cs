using Microsoft.AspNetCore.Components;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Components.Extensions;
using PROWeb.Data.Services.Assessments;

namespace PROWeb.Components.Assessments.Filters
{
    public partial class AssessmentFilterAdvanced : PROFilterComponentWithState<AssessmentFilterViewModel>
    {
        [Inject]
        private IAssessmentServiceFactory _assessmentsServiceFactory { get; set; } = null!;

        public IEnumerable<ConstituencyViewModel>? Constituencies { get; private set; }

        public IEnumerable<ParishViewModel>? Parishes { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            using (var assessments = _assessmentsServiceFactory.CreateService())
            {
                Constituencies = await assessments.GetConstituencies().ProjectToListAsync<ConstituencyViewModel>(Mapper);
                Parishes = await assessments.GetParishes().ProjectToListAsync<ParishViewModel>(Mapper);
            }

            ResetFilter();
        }
    }
}
