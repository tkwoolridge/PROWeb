using PROWeb.Common.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.ViewModels.Person;

namespace PROWeb.Components.Person
{
    public abstract partial class PersonFlagsView<TPersonFlagsViewModel, TPersonFlagViewModel> : PersonFlagsViewBase<TPersonFlagsViewModel, TPersonFlagViewModel>
        where TPersonFlagsViewModel : class, IPersonFlagsViewModel<TPersonFlagViewModel>
        where TPersonFlagViewModel : ViewModelBase, IPersonFlagViewModel, new()
    {
    }

    public abstract class PersonFlagsViewBase<TPersonFlagsViewModel,TPersonFlagViewModel> : PROView<TPersonFlagsViewModel> 
        where TPersonFlagsViewModel : class, IPersonFlagsViewModel<TPersonFlagViewModel>
        where TPersonFlagViewModel : ViewModelBase,IPersonFlagViewModel, new()
    {
        protected IList<TPersonFlagViewModel>? Flags { get; set; }

        protected List<int> FlagsValues { get; set; } = Enumerable.Empty<int>().ToList();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Flags = await GetFlagsAsync();

            FlagsValues = Context.Flags?.Select(f => f.FlagId).ToList() ?? Enumerable.Empty<int>().ToList();
        }

        protected abstract Task<IList<TPersonFlagViewModel>?> GetFlagsAsync();

        protected override void OnViewContextUpdate()
        {
            base.OnViewContextUpdate();

            FlagsValues = Context.Flags?.Select(f => f.FlagId).ToList() ?? Enumerable.Empty<int>().ToList();
        }

        public override void OnSave()
        {
            Context.Flags = Flags?.Where(f => FlagsValues.Contains(f.FlagId)).ToList();
        }  
    }
}
