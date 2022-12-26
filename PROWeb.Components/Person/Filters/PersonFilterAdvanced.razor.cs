using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Components.Person.Filters
{
    public partial class PersonFilterAdvanced : PROFilterComponent<PersonFilterViewModel>
    {
        [Inject]
        private IAssessmentServiceFactory _assessmentsServiceFactory { get; set; } = null!;

        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        public IEnumerable<ConstituencyViewModel>? Constituencies { get; private set; }

        public IEnumerable<ParishViewModel>? Parishes { get; private set; }

        public IList<VoterFlagViewModel>? Flags { get; set; }

        public IEnumerable<object>? RegistrationYears { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            using (var assessments = _assessmentsServiceFactory.CreateService())
            {
                Constituencies = await assessments.GetConstituencies().ProjectToListAsync<ConstituencyViewModel>(Mapper);
                Parishes = await assessments.GetParishes().ProjectToListAsync<ParishViewModel>(Mapper);
            }

            using (var voters = _votersServiceFactory.CreateService())
            {
                Flags = await voters.GetVoterFlags().ProjectToListAsync<VoterFlagViewModel>(Mapper);
            }

            RegistrationYears = Enumerable.Range(DateTime.Now.Year - 1, 3).Cast<object>();

            ResetFilter();
        }

        protected override void ResetFilter()
        {
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
