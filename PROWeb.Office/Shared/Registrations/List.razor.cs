using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Data.Services.Registrations;
using PROWeb.Office.Shared.Registrations.Filters;
using PROWeb.Office.Shared.Registrations.ViewModels;
using Telerik.Blazor.Components;

namespace PROWeb.Office.Shared.Registrations
{
    public partial class List : PROListComponent<RegistrationFilterModel, ListRegistrationViewModel>
    {
        [Inject]
        private IRegistrationServiceFactory _registrationServiceFactory { get; set; } = null!;

        protected TelerikListView<ListRegistrationViewModel>? ListRef { get; set; }

        protected void OnUpdate(ListViewCommandEventArgs args)
        {
            if (args.Item is ListRegistrationViewModel current &&
                Data?.FirstOrDefault(m => m.VoterId == current.VoterId) is { } previous &&
                Data?.IndexOf(previous) is { } index && index > -1)
            {
                Data?.RemoveAt(index);
                Data?.Insert(index, current);

                ListRef?.Rebind();
            }
        }

        protected override async Task<IList<ListRegistrationViewModel>> GetDataAsync(RegistrationFilterModel filter)
        {
            using (var service = _registrationServiceFactory.CreateService())
            {
                return await service.GetRegistrations
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
                    .ProjectToListAsync<ListRegistrationViewModel>(Mapper);
            }
        }
    }
}
