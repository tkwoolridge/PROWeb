using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Person;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Voters;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Components.VoterRegistry
{
    public partial class VoterList : PROListComponent<PersonFilterViewModel, ListVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        protected int Page { get; set; }

        protected TelerikListView<ListVoterViewModel> VoterListRef { get; set; }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            if (args.Item is ListVoterViewModel current &&
                Data.FirstOrDefault(m => m.VoterId == current.VoterId) is { } previous &&
                Data.IndexOf(previous) is { } index && index > -1) 
            {
                Data.RemoveAt(index);
                Data.Insert(index, current);

                VoterListRef.Rebind();
            }
        }

        protected override async Task<IList<ListVoterViewModel>> OnFilterAsync(PersonFilterViewModel filter)
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
