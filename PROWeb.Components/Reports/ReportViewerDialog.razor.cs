using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Common.Layouts;
using PROWeb.Components.Reports.Models;
using System.Text.Json;
using Telerik.DataSource;
using Telerik.ReportViewer.BlazorNative;

namespace PROWeb.Components.Reports
{
    public partial class ReportViewerDialog : PROComponent
    {
        protected DialogLayout? WindowRef { get; set; }

        protected ReportSourceOptions? ReportSource { get; set; }

        [Parameter]
        public EventCallback<CompositeFilterDescriptor> RunReport { get; set; }

        [Parameter]
        public CompositeFilterDescriptor? Filter { get; set; } = new CompositeFilterDescriptor
        {
            FilterDescriptors = new FilterDescriptorCollection()
        };

        public void Show(ReportDescriptor report)
        {
            ReportSource = new ReportSourceOptions()
            {
                Report = JsonSerializer.Serialize(report)
            };

            WindowRef?.Show();
        }
    }
}
