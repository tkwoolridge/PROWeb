using Microsoft.AspNetCore.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Components.Common;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Services.Assessments;
using PROWeb.Data.Services.CachedData;
using PROWeb.Office.Shared.Registrations.ViewModels;

namespace PROWeb.Office.Shared.Registrations.Filters
{
    public partial class RegistrationsFilter : PROFilterComponent<RegistrationFilterModel>
    {

        [Inject]
        private IAssessmentServiceFactory _assessmentsServiceFactory { get; set; } = null!;

        [Inject]
        private ICachedDataService _cachedDataService { get; set; } = null!;

        protected FormDialog FormDialogRef { get; set; } = default!;

        protected RegistrationViewModel? FormModel { get; set; }

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

            Filter.RegistryYear = _cachedDataService.RegistrationYear;
        }

        private void CreateForm1()
        {
            FormModel = new RegistrationViewModel
            {
                FormTypeId = (int)FormTypes.Form1
            };

            FormDialogRef.Show();
        }

        private void CreateForm2()
        {
            FormModel = new RegistrationViewModel
            {
                FormTypeId = (int)FormTypes.Form2
            };

            FormDialogRef.Show();
        }
    }
}
