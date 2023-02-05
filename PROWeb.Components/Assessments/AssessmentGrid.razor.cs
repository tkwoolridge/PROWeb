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
        public bool EnableSelection { get; set; }

        protected override void OnSelectionChanged(IEnumerable<AssessmentViewModel> selectedItems)
        {
            base.OnSelectionChanged(selectedItems);

            if (selectedItems.FirstOrDefault() is { } eligible)
            {
                AssessmentSelected.InvokeAsync(eligible);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
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
