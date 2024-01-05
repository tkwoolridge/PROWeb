using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.CachedData;

namespace PROWeb.Office.Shared.Registry.Views
{
    public class VoterFlagsView : FlagsView<VoterViewModel, VoterFlagViewModel>
    {
        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = null!;

        public VoterFlagsView() : base(
            v => v.Flags,
            v => v.CommonwealthCitizen,
            v => v.BermudianStatusGranted,
            v => v.RegisteredAsElector,
            v => v.IsBermudianStatusGranted,
            v => v.WasBornIn,
            v => v.CountryId,
            f => f.FlagId,
            f => f.FlagDescription)
        {
        }


        protected override async Task<IList<VoterFlagViewModel>?> GetFlagsAsync()
        {
            return await Task.FromResult(_cachedDataService.Flags.Adapt<List<VoterFlagViewModel>>());
        }
    }
}
