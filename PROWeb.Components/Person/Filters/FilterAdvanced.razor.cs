using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Contexts;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Data.Services.Assessments;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Filters
{
    public abstract class FilterAdvancedBase<TFlagViewModel> : PROFilterComponent<FilterModel>
        where TFlagViewModel : SlimViewModelBase, new()
    {
    }

    public abstract partial class FilterAdvanced<TFlagViewModel> : FilterAdvancedBase<TFlagViewModel>
        where TFlagViewModel : SlimViewModelBase, new()
    {
        private readonly Expression<Func<TFlagViewModel, int>>? _flagIdPath;
        private readonly Expression<Func<TFlagViewModel, string?>>? _flagDescriptionPath;

        [Inject]
        private IAssessmentServiceFactory _assessmentsServiceFactory { get; set; } = null!;

        protected FilterAdvanced(
            Expression<Func<TFlagViewModel, int>>? flagIdPath = null,
            Expression<Func<TFlagViewModel, string?>>? flagDescriptionPath = null
            )
        {
            _flagIdPath = flagIdPath;
            _flagDescriptionPath = flagDescriptionPath;
        }

        public IEnumerable<ConstituencyViewModel>? Constituencies { get; private set; }

        public IEnumerable<ParishViewModel>? Parishes { get; private set; }

        internal IList<FlagContext<TFlagViewModel>>? Flags { get; set; }

        public IEnumerable<object>? RegistrationYears { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            using (var assessments = _assessmentsServiceFactory.CreateService())
            {
                Constituencies = await assessments.GetConstituencies().ProjectToListAsync<ConstituencyViewModel>(Mapper);
                Parishes = await assessments.GetParishes().ProjectToListAsync<ParishViewModel>(Mapper);
            }

            IList<TFlagViewModel>? flags = await GetFlags();

            Flags = flags?.Select(f => new FlagContext<TFlagViewModel>(f, _flagIdPath, _flagDescriptionPath)).ToList();

            RegistrationYears = Enumerable.Range(DateTime.Now.Year - 1, 3).Cast<object>();

            ResetFilter();
        }

        protected abstract Task<IList<TFlagViewModel>?> GetFlags();

        protected override void ResetFilter()
        {
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
