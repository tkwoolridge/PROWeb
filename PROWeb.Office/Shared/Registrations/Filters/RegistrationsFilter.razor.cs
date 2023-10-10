using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Extensions;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Data.Services.Assessments;

namespace PROWeb.Office.Shared.Registrations.Filters
{
    public partial class RegistrationsFilter : PROFilterComponent<RegistrationFilterModel>
    {

        [Inject]
        private IAssessmentServiceFactory _assessmentsServiceFactory { get; set; } = null!;


        public IEnumerable<ConstituencyViewModel>? Constituencies { get; private set; }

        public IEnumerable<ParishViewModel>? Parishes { get; private set; }

        public IEnumerable<object>? RegistrationYears { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            using (var assessments = _assessmentsServiceFactory.CreateService())
            {
                Constituencies = await assessments.GetConstituencies().ProjectToListAsync<ConstituencyViewModel>();
                Parishes = await assessments.GetParishes().ProjectToListAsync<ParishViewModel>();
            }

            RegistrationYears = Enumerable.Range(DateTime.Now.Year - 1, 3).Cast<object>();

            ResetFilter();
        }

        protected override void ResetFilter()
        {
            base.ResetFilter();
            Filter.RegistryYear = 2022;
        }
    }
}
