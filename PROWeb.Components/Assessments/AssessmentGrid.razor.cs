using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Extensions;
using PROWeb.Components.ViewModels.Assessment;
using PROWeb.Data.Services.Assessments;


namespace PROWeb.Components.Assessments
{
    public partial class AssessmentGrid : PROListComponent<AssessmentFilterViewModel, AssessmentViewModel>
    {
        [Inject]
        IAssessmentServiceFactory _assessmentServiceFactory { get; set; } = null!;

        [Parameter]
        public EventCallback<AssessmentViewModel> AssessmentSelected { get; set; }

        [Parameter]
        public int PageSize { get; set; } = 20;

        protected int Page { get; set; }

        private async Task OnSelectionChanged(IEnumerable<AssessmentViewModel> selectedItems)
        {
            await AssessmentSelected.InvokeAsync(selectedItems.First());
        }

        protected override async Task<IList<AssessmentViewModel>> OnFilterAsync(AssessmentFilterViewModel filter)
        {
            Page = 1;

            using (var service = _assessmentServiceFactory.CreateService())
            {
                return await service.GetAssessments
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
            }
        }
    }
}
