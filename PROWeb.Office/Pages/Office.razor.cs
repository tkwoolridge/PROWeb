using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Data.Services.Office;
using PROWeb.Office.Shared.Office.ViewModels;

namespace PROWeb.Office.Pages
{
    public partial class Office : PROComponent
    {
        protected override string? PageTitle => "Office Settings";

        [Inject]
        protected IOfficeServiceFactory OfficeServiceFactory { get; set; } = default!;

        public OfficeViewModel? OfficeModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            using (var service = OfficeServiceFactory.CreateService())
            {
                var office = await service.GetOfficeAsync();

                OfficeModel = office.Adapt<OfficeViewModel>();
            }
        }
    }
}
