using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Components.Person.Contexts;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.CachedData;
using PROWeb.Data.Services.Voters;
using System.Linq.Expressions;

namespace PROWeb.Components.Person
{
    public abstract class FlagsViewBase<TFlagsViewModel, TFlagViewModel> : PROEditableView<TFlagsViewModel>
        where TFlagsViewModel : SlimViewModelBase
        where TFlagViewModel : SlimViewModelBase, new()
    {
    }

    public abstract partial class FlagsView<TFlagsViewModel, TFlagViewModel> : FlagsViewBase<TFlagsViewModel, TFlagViewModel>
        where TFlagsViewModel : SlimViewModelBase
        where TFlagViewModel : SlimViewModelBase, new()
    {
        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = default!;

        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = default!;

        protected IList<CountryViewModel>? Countries { get; set; }

        private readonly Expression<Func<TFlagsViewModel, List<TFlagViewModel>?>>? _flagsPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _commonwealthCitizenPath;
        private readonly Expression<Func<TFlagsViewModel, DateTime?>>? _bermudianStatusGrantedPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _registeredAsElectorPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _isBermudianStatusGrantedPath;
        private readonly Expression<Func<TFlagsViewModel, bool?>>? _wasBornIn;
        private readonly Expression<Func<TFlagsViewModel, int?>>? _countryId;
        private readonly Expression<Func<TFlagViewModel, int>>? _flagIdPath;
        private readonly Expression<Func<TFlagViewModel, string?>>? _flagDescriptionPath;

        public void UpdateFromVoter(VoterViewModel voter)
        {
            voter.Adapt(FlagsContext);

            NotifyFieldsChanged();
        }

        internal FlagsContext<TFlagsViewModel, TFlagViewModel> FlagsContext { get; } = new();

        internal IList<FlagContext<TFlagViewModel>>? Flags { get; set; }

        protected List<int> FlagsValues { get; set; } = Enumerable.Empty<int>().ToList();

        protected string? CountryName { get; set; }

        protected void OnChanged()
        {
            FlagsContext.Flags = Flags?
                .Where(f => FlagsValues.Contains(f.FlagId))
                .Select(f => f.Model)
                .OfType<TFlagViewModel>()
                .ToList();
        }

        protected FlagsView(
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
            _flagsPath = flagsPath;
            _commonwealthCitizenPath = commonwealthCitizenPath;
            _bermudianStatusGrantedPath = bermudianStatusGrantedPath;
            _registeredAsElectorPath = registeredAsElectorPath;
            _isBermudianStatusGrantedPath = isBermudianStatusGrantedPath;
            _wasBornIn = wasBornIn;
            _countryId = countryId;
            _flagIdPath = flagIdPath;
            _flagDescriptionPath = flagDescriptionPath;
        }

        protected void OnCountryChanged(object? countryName)
        {
            GetCountry(countryName?.ToString());
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var flags = await GetFlagsAsync();
            Countries = _cachedDataService.Countries.Adapt<List<CountryViewModel>>();

            SetCountry(FlagsContext.CountryId);
            Flags = flags?.Select(f => new FlagContext<TFlagViewModel>(f, _flagIdPath, _flagDescriptionPath)).ToList();
        }

        protected abstract Task<IList<TFlagViewModel>?> GetFlagsAsync();

        protected override EditContext? GetEditContext()
        {
            return new EditContext(FlagsContext);
        }

        protected override void OnContextChanged(object? sender, ContextChangedEventArgs e)
        {
            base.OnContextChanged(sender, e);

            if (e.PropertyName.Equals(nameof(FlagsContext.CountryId)))
            {
                SetCountry(e.PropertyValue as int?);
            }
        }

        private void SetCountry(int? countryId)
        {
            CountryName = Countries?.FirstOrDefault(c => c.CountryId == countryId)?.CountryName;
        }

        private void GetCountry(string? countryName)
        {
            var countryId =
                Countries?.
                FirstOrDefault(c => c.CountryName!.Equals(countryName, StringComparison.InvariantCultureIgnoreCase))?.
                CountryId;

            if (countryId != FlagsContext.CountryId)
            {
                FlagsContext.CountryId = countryId;
            }
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();

            FlagsContext.UnBind();
            if (Model is { } model)
            {
                FlagsContext.Bind
                (
                model,
                _flagsPath,
                _commonwealthCitizenPath,
                _bermudianStatusGrantedPath,
                _registeredAsElectorPath,
                _isBermudianStatusGrantedPath,
                _wasBornIn,
                _countryId,
                _flagIdPath,
                _flagDescriptionPath
                );

                FlagsValues = FlagsContext?.ContextFlags?.Select(f => f.FlagId).ToList() ?? Enumerable.Empty<int>().ToList();
            }
        }

        private void NotifyFieldsChanged()
        {
            EditContext?.NotifyFieldChanged(new FieldIdentifier(FlagsContext, nameof(FlagsContext.BermudianStatusGranted)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(FlagsContext, nameof(FlagsContext.CommonwealthCitizen)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(FlagsContext, nameof(FlagsContext.RegisteredAsElector)));
            EditContext?.NotifyFieldChanged(new FieldIdentifier(FlagsContext, nameof(FlagsContext.IsBermudianStatusGranted)));
        }
    }
}
