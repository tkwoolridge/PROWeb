using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Common.ViewModels;
using System.Linq.Expressions;

namespace PROWeb.Components.Person.Contexts
{
    internal class FlagsContext<TFlagsViewModel, TFlagViewModel> : BindableContext<TFlagsViewModel> 
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

        //private List<TFlagViewModel>? _sourceFlags { get; }

        private IDisposable? _flagsBinding;
        private IDisposable? _commonwealthCitizenBinding;
        private IDisposable? _bermudianStatusGrantedBinding;
        private IDisposable? _registeredAsElectorBinding;
        private IDisposable? _isBermudianStatusGrantedBinding;

        public void Bind(
            TFlagsViewModel model,
            Expression<Func<TFlagsViewModel, List<TFlagViewModel>?>>? flagsPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? commonwealthCitizenPath = null,
            Expression<Func<TFlagsViewModel, DateTime?>>? bermudianStatusGrantedPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? registeredAsElectorPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? isBermudianStatusGrantedPath = null,
            Expression<Func<TFlagViewModel, int>>? flagIdPath = null,
            Expression<Func<TFlagViewModel, string?>>? flagDescriptionPath = null)
        {
            Model = model;

            _flagsBinding = Model?.Bind(this, flagsPath, c => c.Flags, StrongBindingMode.TwoWay);
            _commonwealthCitizenBinding = Model?.Bind(this, commonwealthCitizenPath, c => c.CommonwealthCitizen, StrongBindingMode.TwoWay);
            _bermudianStatusGrantedBinding = Model?.Bind(this, bermudianStatusGrantedPath, c => c.BermudianStatusGranted, StrongBindingMode.TwoWay);
            _registeredAsElectorBinding = Model?.Bind(this, registeredAsElectorPath, c => c.RegisteredAsElector, StrongBindingMode.TwoWay);
            _isBermudianStatusGrantedBinding = Model?.Bind(this, isBermudianStatusGrantedPath, c => c.IsBermudianStatusGranted, StrongBindingMode.TwoWay);

            if(Flags is not { } flags)
            {
                return;
            }

            foreach(var flag in flags)
            {
                ContextFlags?.Add(new FlagContext<TFlagViewModel>(
                    flag,
                    flagIdPath,
                    flagDescriptionPath
                    ));
            }
        }

        public override void UnBind()
        {
            _flagsBinding?.Dispose();
            _commonwealthCitizenBinding?.Dispose();
            _bermudianStatusGrantedBinding?.Dispose();
            _registeredAsElectorBinding?.Dispose();
            _isBermudianStatusGrantedBinding?.Dispose();
        }
    }
}
