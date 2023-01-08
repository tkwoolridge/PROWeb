using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Components.Person.Filters
{
    public abstract partial class PersonFilterAdvanced<TPersonFlagViewModel> : PersonFilterAdvancedBase<TPersonFlagViewModel>
        where TPersonFlagViewModel : ViewModelBase, IPersonFlagViewModel, new()
    {
    }

    public abstract class PersonFilterAdvancedBase<TPersonFlagViewModel> : PROFilterComponent<PersonFilterViewModel>
        where TPersonFlagViewModel : ViewModelBase, IPersonFlagViewModel, new()
    {
        [Inject]
        private IAssessmentServiceFactory _assessmentsServiceFactory { get; set; } = null!;

        public IEnumerable<ConstituencyViewModel>? Constituencies { get; private set; }

        public IEnumerable<ParishViewModel>? Parishes { get; private set; }

        public IList<TPersonFlagViewModel>? Flags { get; set; }

        public IEnumerable<object>? RegistrationYears { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            using (var assessments = _assessmentsServiceFactory.CreateService())
            {
                Constituencies = await assessments.GetConstituencies().ProjectToListAsync<ConstituencyViewModel>(Mapper);
                Parishes = await assessments.GetParishes().ProjectToListAsync<ParishViewModel>(Mapper);
            }

            Flags = await GetFlags();

            RegistrationYears = Enumerable.Range(DateTime.Now.Year - 1, 3).Cast<object>();

            ResetFilter();
        }

        protected abstract Task<IList<TPersonFlagViewModel>?> GetFlags();

        protected override void ResetFilter()
        {
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
