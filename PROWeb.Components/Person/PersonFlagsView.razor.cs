using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Data.Services.Voters;

namespace PROWeb.Components.Person
{
    public class PersonFlagsViewBase<TPersonFlagsViewModel,TPersonFlagViewModel> : PROView<TPersonFlagsViewModel> 
        where TPersonFlagsViewModel : class, IPersonFlagsViewModel<TPersonFlagViewModel>
        where TPersonFlagViewModel : ViewModelBase,IPersonFlagViewModel, new()
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected IList<TPersonFlagViewModel>? PersonFlags { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            using (var service = _voterServiceFactory.CreateService())
            {
                PersonFlags = await service.GetVoterFlags().ProjectToListAsync<TPersonFlagViewModel>(Mapper);
            }

            Context.PersonFlagsValues = Context.PersonFlags.Select(f => f.FlagId).ToList();
        }
    }
}
