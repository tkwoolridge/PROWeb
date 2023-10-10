using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class FlagContext<TFlagViewModel> : ViewModelContext<TFlagViewModel> where TFlagViewModel : SlimViewModelBase, new()
    {
        private int _flagId;

        public int FlagId
        {
            get => _flagId;
            set => RaiseAndSetIfChanged(ref _flagId, value);
        }

        private string? _flagDescription;

        public string? FlagDescription
        {
            get => _flagDescription;
            set => RaiseAndSetIfChanged(ref _flagDescription, value);
        }

        public FlagContext(
            TFlagViewModel? model = null,
            Expression<Func<TFlagViewModel, int>>? flagIdPath = null,
            Expression<Func<TFlagViewModel, string?>>? flagDescriptionPath = null
            )
        {
            Model = model ?? new TFlagViewModel();

            AddBinding(Model?.Bind(this, flagIdPath, c => c.FlagId, StrongBindingMode.TwoWay));            
            AddBinding(Model?.Bind(this, flagDescriptionPath, c => c.FlagDescription, StrongBindingMode.TwoWay));
        }
    }
}
