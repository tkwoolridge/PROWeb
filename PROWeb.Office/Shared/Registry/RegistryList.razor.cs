using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Components;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Services.State;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Registry.Views;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Registry
{
    public partial class RegistryList : PROListComponent<FilterModel, VoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected TelerikListView<VoterViewModel>? ListRef { get; set; }

        [Inject]
        protected IStateService<VoterTabView, int> VoterTabStateService { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            PageChanged += OnPageChanged;
        }

        private void OnPageChanged(object? sender, int e)
        {
            VoterTabStateService.Clear();
        }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            if (args.Item is VoterViewModel current &&
                Data?.FirstOrDefault(m => m.VoterId == current.VoterId) is { } previous &&
                Data?.IndexOf(previous) is { } index && index > -1)
            {
                Data?.RemoveAt(index);
                Data?.Insert(index, current);

                ListRef?.Rebind();
            }
        }

        protected override async Task<IList<VoterViewModel>> GetDataAsync(FilterModel filter)
        {
            VoterTabStateService.Clear();

            using (var service = _voterServiceFactory.CreateService())
            {
                return await service.GetVoters
                    (
                    filter.RegistryYear,
                    null,
                    filter.FirstName,
                    filter.LastName,
                    filter.MiddleName,
                    filter.MaidenName,
                    filter.IsEligible,
                    filter.DateOfBirth,
                    filter.AgeFrom,
                    filter.AgeTo,
                    filter.Phone,
                    filter.AssessmentNo,
                    filter.StreetName,
                    filter.HouseNo,
                    filter.ConstituencyNo,
                    filter.ParishNo,
                    filter.PostalCode)
                    .ProjectToType<VoterViewModel>()
                    .ToListAsync();
            }
        }
    }
}
