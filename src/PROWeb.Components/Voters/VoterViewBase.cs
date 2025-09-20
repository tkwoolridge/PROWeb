using KellermanSoftware.CompareNetObjects;
using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.ViewModels;
using PROWeb.Components.Common.Views;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Authentication.Models;
using PROWeb.Data.Authentication.Services.Users;
using PROWeb.Data.Models;
using PROWeb.Data.Services.CachedData;
using PROWeb.Data.Services.Voters;
using System.Diagnostics;

namespace PROWeb.Components.Voters
{
    public abstract class VoterViewBase<TViewModel> : PROCompositeView<TViewModel> where TViewModel : SlimViewModelBase
    {
        private static CompareLogic _voterCompare;

        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        [Inject]
        private IUsersServiceFactory _userServiceFactory { get; set; } = default!;

        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = default!;


        [Parameter]
        public EventCallback OnVoterSaved { get; set; }

        protected PROUser? User { get; private set; }

        [Parameter]
        public bool Editable { get; set; }

        protected override async Task OnInitializedAsync()
        {
            using (var service = _userServiceFactory.CreateService())
            {
                User = await service.GetCurrentUser();
            }

            await base.OnInitializedAsync();
        }

        protected override async Task SaveAsync(TViewModel model)
        {
            var voter = model as VoterViewModel;

            Debug.Assert(voter != null);

            if (voter.VoterId is not null)
            {
                await UpdateVoterAsync(voter);
            }
            else
            {
                await AddVoterAsync(voter);
            }

            if (OnVoterSaved.HasDelegate)
            {
                await OnVoterSaved.InvokeAsync(null);
            }
        }

        protected async Task AddVoterAsync(VoterViewModel model, int? registrationId = null)
        {
            Debug.Assert(User?.UserName != null);
            Debug.Assert(Model != null);

            model.LastUpdated = DateTime.UtcNow;
            model.LastUpdatedBy = User.UserName;
            model.RegistryYear = _cachedDataService.Office.ElectionYear;

            using (var service = _votersServiceFactory.CreateService())
            {
                var voter = model.Adapt<Voter>();
                await service.AddVoterAsync(voter);
            }
        }

        protected async Task UpdateVoterAsync(VoterViewModel model)
        {
            Debug.Assert(User?.UserName != null);
            Debug.Assert(Model != null);

            model.LastUpdated = DateTime.UtcNow;
            model.LastUpdatedBy = User.UserName;


            using (var service = _votersServiceFactory.CreateService())
            {
                var oldVoter = await service.GetVoter(model.RegistryYear, model.VoterId!.Value);

                VoterViewModel? oldView = null;

                if (oldVoter is not null)
                {
                    oldView = oldVoter.Adapt<VoterViewModel>();
                }

                Voter? voter = Model.Adapt(oldVoter);

                Debug.Assert(voter != null);

                await service.UpdateVoterAsync(voter);

                if (oldView is not null)
                {
                    await UpdateVoterHistory(oldView, model);
                }
            }
        }

        private async Task UpdateVoterHistory(VoterViewModel oldVoter, VoterViewModel newVoter)
        {
            Debug.Assert(User?.UserName != null);

            // Update voter history.

            var compareResult = _voterCompare.Compare(oldVoter, newVoter);

            VoterHistory history = new VoterHistory
            {
                VoterId = oldVoter.VoterId!.Value,
                RegistryYear = oldVoter.RegistryYear,
                Created = DateTime.Now,
                RegistrationId = null,
                UserName = User.UserName,
                Fields = new List<VoterHistoryField>()
            };

            foreach (var diff in compareResult.Differences)
            {
                history.Fields.Add(new VoterHistoryField
                {
                    Field = diff.PropertyName,
                    OldValue = diff.Object1Value,
                    NewValue = diff.Object2Value
                });
            }

            using (var service = _votersServiceFactory.CreateService())
            {
                await service.AddVoterHistory(history);
            }
        }

        static VoterViewBase()
        {
            ComparisonConfig config = new ComparisonConfig
            {
                CompareBackingFields = false,
                CompareReadOnly = false,
                CompareChildren = false,
                CompareStaticFields = false,
                CompareFields = false,
                CompareStaticProperties = false,
                MaxDifferences = 20
            };

            config.IgnoreProperty<VoterViewModel>(v => v.LastUpdated);
            config.IgnoreProperty<VoterViewModel>(v => v.CountryId);
            config.IgnoreProperty<VoterViewModel>(v => v.LastUpdatedBy);
            config.IgnoreProperty<VoterViewModel>(v => v.Flags);
            config.IgnoreProperty<VoterViewModel>(v => v.Documents);

            _voterCompare = new CompareLogic(config);
        }
    }
}
