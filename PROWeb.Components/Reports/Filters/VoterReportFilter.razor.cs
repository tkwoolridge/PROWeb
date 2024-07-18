using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.CachedData;
using Telerik.Blazor.Components;
using Telerik.DataSource;

namespace PROWeb.Components.Reports.Filters
{
    public partial class VoterReportFilter : ReportFilterView
    {
        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = null!;

        protected List<ConstituencyViewModel>? Constituencies { get; set; }

        protected List<ParishViewModel>? Parishes { get; set; }

        protected List<FilterListOperator> TextOperators = new List<FilterListOperator>
        {
            new FilterListOperator { Operator = FilterOperator.StartsWith, Text = "Begins With" },
            new FilterListOperator { Operator = FilterOperator.EndsWith, Text = "Ends With" },
            new FilterListOperator { Operator = FilterOperator.Contains, Text = "Has" }
        };

        protected List<FilterListOperator> NumberOrDatesOperators = new List<FilterListOperator>
        {
            new FilterListOperator { Operator = FilterOperator.IsEqualTo, Text = "Equal" },
            new FilterListOperator { Operator = FilterOperator.IsGreaterThanOrEqualTo, Text = "Equal Or Greater Then" },
            new FilterListOperator { Operator = FilterOperator.IsLessThan, Text = "Less Then" },
            new FilterListOperator { Operator = FilterOperator.IsGreaterThan, Text = "Greater Then" },
            new FilterListOperator { Operator = FilterOperator.IsGreaterThanOrEqualTo, Text = "Equal Or Greater Then" },
            new FilterListOperator { Operator = FilterOperator.IsLessThanOrEqualTo, Text = "Equal Or Less Then" }
        };

        protected List<FilterListOperator> HouseNoOperators = new List<FilterListOperator>
        {
            new FilterListOperator { Operator = FilterOperator.IsEqualTo, Text = "Equal" }
        };

        protected List<FilterListOperator> RegistryYearOperators = new List<FilterListOperator>
        {
            new FilterListOperator { Operator = FilterOperator.IsEqualTo, Text = "Equal" }
        };

        protected FilterOperator DefaultTextOperator = FilterOperator.StartsWith;
        protected FilterOperator DefaultNumberAndDatesOperator = FilterOperator.IsEqualTo;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ResetFilterToDefault(Filter);

            Constituencies = _cachedDataService.Constituencies.Adapt<List<ConstituencyViewModel>>();
            Parishes = _cachedDataService.Parishes.Adapt<List<ParishViewModel>>();
        }

        public override void ResetFilterToDefault(CompositeFilterDescriptor? descriptor)
        {
            if (Find((f) => f.Member == nameof(VoterViewModel.RegistryYear), descriptor) is { } field)
            {
                field.Value = _cachedDataService.Office.ElectionYear;
            }
        }

        private void OnFilterValueChanged(FilterDescriptor filterDescriptor, int? newValue)
        {
            filterDescriptor.Value = newValue;
        }
    }
}
