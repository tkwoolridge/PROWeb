using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.Grid;

namespace PROWeb.Office.Components.VoterRegistry
{
    public partial class VotersList : PROListComponent<FilterModel, ListVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected TelerikListView<ListVoterViewModel>? ListRef { get; set; }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            if (args.Item is ListVoterViewModel current &&
                Data?.FirstOrDefault(m => m.VoterId == current.VoterId) is { } previous &&
                Data?.IndexOf(previous) is { } index && index > -1) 
            {
                Data?.RemoveAt(index);
                Data?.Insert(index, current);

                ListRef?.Rebind();
            }
        }

        protected override async Task<IList<ListVoterViewModel>> OnFilterAsync(FilterModel filter)
        {
            Page = 1;

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
                    .ProjectToListAsync<ListVoterViewModel>(Mapper);
            }
        }
    }
}
