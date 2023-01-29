using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Components.Person.Contexts;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class FlagsViewBase<TFlagsViewModel, TFlagViewModel> : PROView<TFlagsViewModel>
        where TFlagsViewModel : SlimViewModelBase
        where TFlagViewModel : SlimViewModelBase, new()
    {
    }

    public abstract partial class FlagsView<TFlagsViewModel, TFlagViewModel> : FlagsViewBase<TFlagsViewModel, TFlagViewModel>
        where TFlagsViewModel : SlimViewModelBase
        where TFlagViewModel : SlimViewModelBase, new()
    {
        private readonly Expression<Func<TFlagsViewModel, List<TFlagViewModel>?>>? _flagsPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _commonwealthCitizenPath;
        private readonly Expression<Func<TFlagsViewModel, DateTime?>>? _bermudianStatusGrantedPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _registeredAsElectorPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _isBermudianStatusGrantedPath;
        private readonly Expression<Func<TFlagViewModel, int>>? _flagIdPath;
        private readonly Expression<Func<TFlagViewModel, string?>>? _flagDescriptionPath;

        internal FlagsContext<TFlagsViewModel, TFlagViewModel> Context { get; } = new();

        internal IList<FlagContext<TFlagViewModel>>? Flags { get; set; }

        protected List<int> FlagsValues { get; set; } = Enumerable.Empty<int>().ToList();

        protected FlagsView(
            Expression<Func<TFlagsViewModel, List<TFlagViewModel>?>>? flagsPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? commonwealthCitizenPath = null,
            Expression<Func<TFlagsViewModel, DateTime?>>? bermudianStatusGrantedPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? registeredAsElectorPath = null,
            Expression<Func<TFlagsViewModel, bool?>>? isBermudianStatusGrantedPath = null,
            Expression<Func<TFlagViewModel, int>>? flagIdPath = null,
            Expression<Func<TFlagViewModel, string?>>? flagDescriptionPath = null)
        {
            _flagsPath = flagsPath;
            _commonwealthCitizenPath = commonwealthCitizenPath;
            _bermudianStatusGrantedPath = bermudianStatusGrantedPath;
            _registeredAsElectorPath = registeredAsElectorPath;
            _isBermudianStatusGrantedPath = isBermudianStatusGrantedPath;
            _flagIdPath = flagIdPath;
            _flagDescriptionPath = flagDescriptionPath;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IList<TFlagViewModel>? flags = await GetFlagsAsync();

            Flags = flags?.Select(f => new FlagContext<TFlagViewModel>(f, _flagIdPath, _flagDescriptionPath)).ToList();
        }

        protected abstract Task<IList<TFlagViewModel>?> GetFlagsAsync();

        protected override EditContext? GetEditContext()
        {
            return new EditContext(Context);
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            Context.UnBind();
            if (Model is { } model)
            {
                Context.Bind
                (
                model,
                _flagsPath,
                _commonwealthCitizenPath,
                _bermudianStatusGrantedPath,
                _registeredAsElectorPath,
                _isBermudianStatusGrantedPath,
                _flagIdPath,
                _flagDescriptionPath
                );

                FlagsValues = Context?.ContextFlags?.Select(f => f.FlagId).ToList() ?? Enumerable.Empty<int>().ToList();
            }
        }

        public override void OnSave()
        {
            Context.Flags = Flags?
                .Where(f => FlagsValues.Contains(f.FlagId))
                .Select(f => f.Model)
                .OfType<TFlagViewModel>()
                .ToList();
        }
    }
}
