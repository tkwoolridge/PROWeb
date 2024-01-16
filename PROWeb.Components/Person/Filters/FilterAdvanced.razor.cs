using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Contexts;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.CachedData;
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

        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = null!;

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
                Constituencies = await assessments.GetConstituencies().ProjectToListAsync<ConstituencyViewModel>();
                Parishes = await assessments.GetParishes().ProjectToListAsync<ParishViewModel>();
            }

            IList<TFlagViewModel>? flags = await GetFlags();

            Flags = flags?.Select(f => new FlagContext<TFlagViewModel>(f, _flagIdPath, _flagDescriptionPath)).ToList();

            RegistrationYears = _cachedDataService.RegistrationYears.Cast<object>();

            ResetFilter();
        }

        protected abstract Task<IList<TFlagViewModel>?> GetFlags();

        protected override void ResetFilter()
        {
            base.ResetFilter();

            Filter.RegistryYear = _cachedDataService.RegistrationYear;
        }
    }
}
