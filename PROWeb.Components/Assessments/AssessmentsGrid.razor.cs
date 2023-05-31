using Microsoft.AspNetCore.Components;
using PROWeb.Components.Extensions;
using PROWeb.Data.Services.Assessments;
using PROWeb.Components.Common;
using PROWeb.Components.ViewModels.Assessments;

namespace PROWeb.Components.Assessments
{
    public partial class AssessmentsGrid : PROGridComponent<AssessmentFilterViewModel, AssessmentViewModel>
    {
        [Inject]
        private IAssessmentServiceFactory _assessmentServiceFactory { get; set; } = null!;

        [Parameter]
        public EventCallback<AssessmentViewModel> AssessmentSelected { get; set; }

        [Parameter]
        public bool EnableSelection { get; set; }

        protected override void OnSelectionChanged(IEnumerable<AssessmentViewModel> selectedItems)
        {
            base.OnSelectionChanged(selectedItems);

            if (selectedItems.FirstOrDefault() is { } assessment)
            {
                AssessmentSelected.InvokeAsync(assessment);
            }
        }

        protected override async Task<IList<AssessmentViewModel>> GetDataAsync(AssessmentFilterViewModel filter)
        {
            using (var service = _assessmentServiceFactory.CreateService())
            {
                IList<AssessmentViewModel> data = await service.GetAssessments
                    (
                        filter.AssessmentNo,
                        filter.Address1,
                        filter.Address2,
                        filter.HouseNo,
                        filter.PostalCode,
                        filter.ParishNo,
                        filter.ConstituencyNo,
                        filter.IsBogusNo
                    )
                .ProjectToListAsync<AssessmentViewModel>(Mapper);

                return data;
            }
        }
    }
}
