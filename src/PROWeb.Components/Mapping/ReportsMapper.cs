using Mapster;
using PROWeb.Data.Models.Reports;
using Telerik.DataSource;

namespace PROWeb.Components.Mapping
{
    public class ReportsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ReportFilter, FilterDescriptor>()
                .Map(d => d.Member, s => s.Name).TwoWays()
                .Map(d => d.Operator, s => (int)s.Operator)
                .Map(d => d.MemberType, s => s.FilterType);

            config.NewConfig<FilterDescriptor, ReportFilter>()
                .Map(d => d.Name, s => s.Member).TwoWays()
                .Map(d => d.Operator, s => (int)s.Operator)
                .Map(d => d.FilterType, s => s.MemberType);

            config.NewConfig<ReportFilterGroup, CompositeFilterDescriptor>()
                .Map(d => d.LogicalOperator, s => (int)s.LogicalOperator)
                .AfterMapping((s, d) =>
                {
                    var filters = new FilterDescriptorCollection();

                    foreach (var filter in s.Filters)
                    {
                        if (filter is ReportFilterGroup group)
                        {
                            filters.Add(group.Adapt<CompositeFilterDescriptor>());
                        }
                        else
                        {
                            filters.Add(filter.Adapt<FilterDescriptor>());
                        }
                    }

                    d.FilterDescriptors = filters;
                });

            config.NewConfig<CompositeFilterDescriptor, ReportFilterGroup>()
                .Map(d => d.LogicalOperator, s => (int)s.LogicalOperator)
                .AfterMapping((s, d) =>
                {
                    foreach (var filter in s.FilterDescriptors)
                    {
                        if (filter is CompositeFilterDescriptor group)
                        {
                            d.Filters.Add(group.Adapt<ReportFilterGroup>());
                        }
                        else
                        {
                            d.Filters.Add(filter.Adapt<ReportFilter>());
                        }
                    }
                });
        }
    }
}
