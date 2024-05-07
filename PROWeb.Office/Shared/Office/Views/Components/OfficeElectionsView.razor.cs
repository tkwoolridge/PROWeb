using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Data.Services.CachedData;
using PROWeb.Office.Shared.Office.ViewModels;
using PROWeb.Office.Shared.Office.Views.Components.Contexts;

namespace PROWeb.Office.Shared.Office.Views.Components
{
    public partial class OfficeElectionsView : OfficeBaseView<OfficeElectionsContext>
    {
        [Inject]
        protected ICachedDataService CachedDataService { get; set; } = null!;

        protected List<ElectionTypeViewModel>? ElectionTypes { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ElectionTypes = CachedDataService.ElectionTypes.Adapt<List<ElectionTypeViewModel>>();
        }
    }
}
