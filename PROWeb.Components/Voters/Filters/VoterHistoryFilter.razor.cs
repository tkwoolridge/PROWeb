using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using PROWeb.Common.Extensions;
using PROWeb.Components.Common;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.Voters;
using System.Runtime.CompilerServices;
using Telerik.SvgIcons;

namespace PROWeb.Components.Voters.Filters
{
    public partial class VoterHistoryFilter : PROFilterComponent<VoterHistoryViewModel>
    {
        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        [Parameter]
        public int? RegistryYear { get; set; }

        [Parameter]
        public int? VoterId { get; set; }

        protected int? FilterId { get; set; }

        protected IList<VoterHistoryViewModel>? Histories { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            using (var service = _votersServiceFactory.CreateService())
            {
                if(VoterId is { } voterId && RegistryYear is { } registryYear)
                {
                    Histories = await service.GetVoterHistories(registryYear, voterId).OrderBy(h => h.Created).ProjectToListAsync<VoterHistoryViewModel>();
                    
                    if(Histories?.FirstOrDefault() is { } filter)
                    {
                        UpdateFilter(filter.Id);
                        await OnSearchAsync();
                    }
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender && Histories?.Count > 0)
            {
                await OnSearchAsync();
            }
        }

        protected async Task OnFilterValueChanged(int? id)
        {
            UpdateFilter(id);
            await OnSearchAsync();
        }

        private void UpdateFilter(int? id)
        {
            Filter = Histories!.First(h => h.Id == id);
            FilterId = id;
        }
    }
}
