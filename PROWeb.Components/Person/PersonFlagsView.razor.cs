using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Components.ViewModels.Voter;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Components.Person
{
    public class PersonFlagsViewBase<TPersonFlagsViewModel> : PROView<TPersonFlagsViewModel> where TPersonFlagsViewModel : class, IPersonFlagsViewModel
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected IList<VoterFlagViewModel>? VoterFlags { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            using(var service = _voterServiceFactory.CreateService())
            {
                VoterFlags = await service.GetVoterFlags().ProjectToListAsync<VoterFlagViewModel>(Mapper);
            }

            Context.VoterFlagsValues =  Context.VoterFlags.Select(f=>f.VoterFlagId).ToList();
        }
    }
}
