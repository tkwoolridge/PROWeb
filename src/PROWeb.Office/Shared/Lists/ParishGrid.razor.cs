using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Assessments;
using PROWeb.Office.Shared.Lists.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Lists
{
    public partial class ParishGrid : PROGridComponent<ListFilterViewModel, ParishViewModel>
    {
        [Inject]
        protected IAssessmentServiceFactory AssessmentServiceFactory { get; set; } = null!;

        protected override async Task<IList<ParishViewModel>> GetDataAsync(ListFilterViewModel filter)
        {
            using (var service = AssessmentServiceFactory.CreateService())
            {
                return await service.GetParishes(parishName: filter.Description).ProjectToListAsync<ParishViewModel>();
            }
        }

        protected override async Task UpdateAsync(GridCommandEventArgs args)
        {
            using (var service = AssessmentServiceFactory.CreateService())
            {
                if (args.Item is ParishViewModel parish)
                {
                    await service.UpdateParishAsync(parish.Adapt<Parish>());

                    RefreshGrid(parish, m => m.ParishNo == parish.ParishNo);
                }
            }
        }
    }
}
