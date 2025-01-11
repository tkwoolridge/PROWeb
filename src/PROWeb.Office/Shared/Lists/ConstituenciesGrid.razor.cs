using DynamicData;
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
    public partial class ConstituenciesGrid : PROGridComponent<ListFilterViewModel, ConstituencyViewModel>
    {
        [Inject]
        protected IAssessmentServiceFactory AssessmentServiceFactory { get; set; } = null!;

        protected override async Task<IList<ConstituencyViewModel>> GetDataAsync(ListFilterViewModel filter)
        {
            using (var service = AssessmentServiceFactory.CreateService())
            {
                return await service.GetConstituencies(constituencyName: filter.Description).ProjectToListAsync<ConstituencyViewModel>();
            }
        }

        protected override async Task UpdateAsync(GridCommandEventArgs args)
        {
            using (var service = AssessmentServiceFactory.CreateService())
            {
                if (args.Item is ConstituencyViewModel constituency)
                {
                    await service.UpdateConstituencyAsync(constituency.Adapt<Constituency>());

                    RefreshGrid(constituency, m => m.ConstituencyNo == constituency.ConstituencyNo);
                }
            }
        }
    }
}
