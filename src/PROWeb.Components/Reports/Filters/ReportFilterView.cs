using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.Extensions;
using Telerik.DataSource;

namespace PROWeb.Components.Reports.Filters
{
    public abstract class ReportFilterView : PROComponent
    {
        protected string? ValidationMessage { get; set; }

        [Parameter]
        public CompositeFilterDescriptor? Filter { get; set; }

        [Parameter]
        public EventCallback<CompositeFilterDescriptor?> FilterChanged { get; set; }

        public virtual bool Validate()
        {
            return true;
        }

        protected async Task OnFilterUpdate()
        {
            if(Filter is null)
            {
                return;
            }

            OnBeforeFilterChange(Filter);

            await FilterChanged.InvokeAsync(Filter);
        }

        protected FilterDescriptor? Find(Func<FilterDescriptor, bool> predict)
        {
            return Find(predict, Filter);
        }

        protected FilterDescriptor? Find(Func<FilterDescriptor, bool> predict, CompositeFilterDescriptor? descriptor)
        {
            return descriptor?.Find<IFilterDescriptor>(
               (f) =>
               {
                   if (f is CompositeFilterDescriptor descriptor)
                   {
                       return descriptor.FilterDescriptors.ToList();
                   }

                   return null;
               },
               (f) =>
               {
                   return f is FilterDescriptor filter && predict(filter);
               }
               ) as FilterDescriptor;
        }

        protected virtual void OnBeforeFilterChange(CompositeFilterDescriptor descriptor)
        {
            Validate();
        }

        public virtual void ResetFilterToDefault(CompositeFilterDescriptor descriptor)
        {
        }
    }
}
