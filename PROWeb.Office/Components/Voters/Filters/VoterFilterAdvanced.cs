using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;

namespace PROWeb.Office.Components.Voters.Filters
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
                return await voters.GetVoterFlags().ProjectToListAsync<VoterFlagViewModel>(Mapper);
            }
        }
    }
}
