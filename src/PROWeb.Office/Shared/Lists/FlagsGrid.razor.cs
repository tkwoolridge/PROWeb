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
        [Inject]
        protected IVotersServiceFactory VotersServiceFactory { get; set; } = null!;

        protected override async Task<IList<VoterFlagViewModel>> GetDataAsync(ListFilterViewModel filter)
        {
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
    }
}
