using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Data.Models;
using PROWeb.Data.Services.CachedData;
using PROWeb.Data.Services.Office;
using PROWeb.Office.Shared.Office.ViewModels;

namespace PROWeb.Office.Shared.Office.Views
{
    public partial class OfficeView : PROCompositeView<OfficeViewModel>
    {
        [Inject]
        protected IOfficeServiceFactory OfficeServiceFactory { get; set; } = null!;

        [Inject]
        protected ICachedDataService CachedDataService { get; set; } = null!;

        protected override async Task SaveAsync(OfficeViewModel model)
        {
            using (var service = OfficeServiceFactory.CreateService())
            {
                var officeSettings = model.Adapt<PROOffice>();
                await service.SaveOfficeAsync(officeSettings);

                CachedDataService.UpdateOfficeCachedData(officeSettings);
            }
        }
    }
}
