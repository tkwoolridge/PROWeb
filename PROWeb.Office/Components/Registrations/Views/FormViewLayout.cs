using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.Layouts;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.ViewModels.Registrations;

namespace PROWeb.Office.Components.Registrations.Views
{
    public class FormViewLayout : ViewsLayout<ListRegistrationViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        public override async Task SaveAsync()
        {
            Registration? voter = Model?.MapTo<Registration>(Mapper);

            //if (voter == null) { return; }

            //using (var service = _voterServiceFactory.CreateService())
            //{
            //    await service.UpdateVoter(voter, "pro1");
            //}
        }
    }
}
