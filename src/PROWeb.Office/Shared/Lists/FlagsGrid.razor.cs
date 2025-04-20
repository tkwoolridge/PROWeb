using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Common;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Lists.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Lists
{
    public partial class FlagsGrid : PROGridComponent<ListFilterViewModel, VoterFlagViewModel>
    {
        private ListFilterViewModel _filter = new ListFilterViewModel();

        [Inject]
        protected IVotersServiceFactory VotersServiceFactory { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await OnFilterAsync(_filter);
        }

        protected override async Task<IList<VoterFlagViewModel>> GetDataAsync(ListFilterViewModel filter)
        {
            _filter = filter;

            using (var service = VotersServiceFactory.CreateService())
            {
                return await service.GetVoterFlags(filter.Description).ProjectToListAsync<VoterFlagViewModel>();
            }
        }

        protected override async Task UpdateAsync(GridCommandEventArgs args)
        {
            using (var service = VotersServiceFactory.CreateService())
            {
                if (args.Item is VoterFlagViewModel flag)
                {
                    await service.UpdateVoterFlagAsync(flag.Adapt<VoterFlag>());

                    RefreshGrid(flag, m => m.FlagId == flag.FlagId);
                }
            }
        }

        protected override async Task CreateAsync(GridCommandEventArgs args)
        {
            using (var service = VotersServiceFactory.CreateService())
            {
                if (args.Item is VoterFlagViewModel flag)
                {
                    var result = await service.AddVoterFlagAsync(flag.Adapt<VoterFlag>());

                    flag.FlagId = result.FlagId;

                    RefreshGrid(flag);
                }
            }
        }
    }
}
