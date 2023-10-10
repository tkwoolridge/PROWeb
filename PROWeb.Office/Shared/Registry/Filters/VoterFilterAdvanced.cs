using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Office.Shared.Registry.Filters
{
    public class VoterFilterAdvanced : FilterAdvanced<VoterFlagViewModel>
    {
        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        public VoterFilterAdvanced()
            : base(f => f.FlagId,
                  f => f.FlagDescription)
        {
        }

        protected override async Task<IList<VoterFlagViewModel>?> GetFlags()
        {
            using (var voters = _votersServiceFactory.CreateService())
            {
                return await voters.GetVoterFlags().ProjectToListAsync<VoterFlagViewModel>();
            }
        }
    }
}
