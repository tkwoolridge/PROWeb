using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class FlagsContext<TFlagsViewModel, TFlagViewModel> : ViewModelContext<TFlagsViewModel>
        where TFlagsViewModel : SlimViewModelBase
        where TFlagViewModel : SlimViewModelBase, new()
    {
        private bool? _commonwealthCitizen;

        public bool? CommonwealthCitizen
        {
            get => _commonwealthCitizen;
            set => RaiseAndSetIfChanged(ref _commonwealthCitizen, value);
        }

        private DateTime? _bermudianStatusGranted;

        public DateTime? BermudianStatusGranted
        {
            get => _bermudianStatusGranted;
            set => RaiseAndSetIfChanged(ref _bermudianStatusGranted, value);
        }

        private bool? _registeredAsElector;

        public bool? RegisteredAsElector
        {
            get => _registeredAsElector;
            set => RaiseAndSetIfChanged(ref _registeredAsElector, value);
        }

        private bool? _isBermudianStatusGranted;

        public bool? IsBermudianStatusGranted
        {
            get => _isBermudianStatusGranted;
            set => RaiseAndSetIfChanged(ref _isBermudianStatusGranted, value);
        }

        private bool? _wasBornIn;

        public bool? WasBornIn
        {
            get => _wasBornIn;
            set => RaiseAndSetIfChanged(ref _wasBornIn, value);
        }

        private int? _countryId;

        public int? CountryId
        {
            get => _countryId;
            set => RaiseAndSetIfChanged(ref _countryId, value);
        }

        public string? CountryIdValue
        {
            get => CountryId?.ToString();
            set => CountryId = Convert.ToInt32(value);
        }

        private List<FlagContext<TFlagViewModel>>? _contextFlags = new List<FlagContext<TFlagViewModel>>();

        public List<FlagContext<TFlagViewModel>>? ContextFlags
        {
            get => _contextFlags;
            set => RaiseAndSetIfChanged(ref _contextFlags, value);
        }


        private IList<TFlagViewModel>? _flags;

        public IList<TFlagViewModel>? Flags
        {
            get => _flags;
            set => RaiseAndSetIfChanged(ref _flags, value);
        }

        public void Bind(
            TFlagsViewModel model,
            Expression<Func<TFlagsViewModel, List<TFlagViewModel>?>>? flagsPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? commonwealthCitizenPath = null,
            Expression<Func<TFlagsViewModel, DateTime?>>? bermudianStatusGrantedPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? registeredAsElectorPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? isBermudianStatusGrantedPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? wasBornIn = null,
            Expression<Func<TFlagsViewModel, int?>>? countryId = null,
            Expression<Func<TFlagViewModel, int>>? flagIdPath = null,
            Expression<Func<TFlagViewModel, string?>>? flagDescriptionPath = null)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model?.Bind(this, flagsPath, c => c.Flags, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, commonwealthCitizenPath, c => c.CommonwealthCitizen, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, bermudianStatusGrantedPath, c => c.BermudianStatusGranted, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, registeredAsElectorPath, c => c.RegisteredAsElector, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, isBermudianStatusGrantedPath, c => c.IsBermudianStatusGranted, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, wasBornIn, c => c.WasBornIn, StrongBindingMode.TwoWay));
                AddBinding(Model?.Bind(this, countryId, c => c.CountryId, StrongBindingMode.TwoWay));
            }

            if (Flags is not { } flags)
            {
                return;
            }

            foreach (var flag in flags)
            {
                ContextFlags?.Add(new FlagContext<TFlagViewModel>(
                    flag,
                    flagIdPath,
                    flagDescriptionPath
                    ));
            }
        }
    }
}
