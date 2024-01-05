using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Components.Person;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.CachedData;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Registrations.Views
{
    public class FormFlagsView : FlagsView<RegistrationViewModel, VoterFlagViewModel>
    {
        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = null!;

        public FormFlagsView() : base(
            null,
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
