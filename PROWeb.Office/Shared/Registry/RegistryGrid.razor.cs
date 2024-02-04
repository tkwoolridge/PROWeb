using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PROWeb.Common.Components;
using PROWeb.Common.Extensions;
using PROWeb.Components.Person.Filters;
using PROWeb.Components.Voters.ViewModels;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Registrations;
using PROWeb.Office.Shared.Registrations.ViewModels;
using System.Diagnostics;
using Telerik.Blazor.Components;
using Telerik.SvgIcons;

namespace PROWeb.Office.Shared.Registry
{
    public partial class RegistryGrid : PROListComponent<FilterModel, VoterViewModel>
    {
        private VoterViewModel? _contextVoter;

        [Inject]
        private IVotersServiceFactory _votersServiceFactory { get; set; } = null!;

        protected TelerikContextMenu<MenuItem> ContextMenu { get; set; } = default!;

        protected FormDialog FormDialogRef { get; set; } = default!;

        protected RegistrationViewModel? FormModel { get; set; }

        protected List<MenuItem> MenuItems { get; set; }

        public RegistryGrid()
        {
            MenuItems = new List<MenuItem>
            {
                new MenuItem()
                {
                    Action = CreateForm2,
                    Text = "Create Form 2",
                    Icon = SvgIcon.TrackChangesEnable
                },
                new MenuItem()
                {
                    Separator = true
                },
                new MenuItem()
                {
                    Separator = true
                },
                new MenuItem()
                {
                    Action = ExportToExcel,
                    Text = "Export to Excel...",
                    Icon = SvgIcon.FileExcel
                },
                new MenuItem()
                {
                    Action = ExportToPDF,
                    Text = "Export to PDF...",
                    Icon = SvgIcon.FilePdf
                }
            };
        }

        protected override async Task<IList<VoterViewModel>> GetDataAsync(FilterModel filter)
        {
            using (var service = _votersServiceFactory.CreateService())
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
                    .ProjectToListAsync<VoterViewModel>();
            }
        }

        private void OnRowContextMenu(GridRowClickEventArgs args)
        {
            _contextVoter = args.Item as VoterViewModel;

            FormModel = new RegistrationViewModel();

            if (args.EventArgs is MouseEventArgs mouseEventArgs)
            {
                _ = ContextMenu.ShowAsync(mouseEventArgs.ClientX, mouseEventArgs.ClientY);
            }
        }

        public void OnItemClick(MenuItem item)
        {
            item.Action?.Invoke(item);
        }

        private void CreateForm2(MenuItem item)
        {
            Debug.Assert(FormModel != null);

            _contextVoter.Adapt(FormModel);
            FormModel.FormTypeId = (int)FormTypes.Form2;

            FormDialogRef.Show();
        }

        private void ExportToPDF(MenuItem item)
        {
        }

        private void ExportToExcel(MenuItem item)
        {
        }
    }

    public class MenuItem
    {
        public string? Text { get; set; }
        
        public bool Disabled { get; set; }
        
        public bool Separator { get; set; }

        public bool HasChildren { get; set; }

        public List<MenuItem>? Items { get; set; } = null;
        
        public Action<MenuItem>? Action { get; set; }

        public ISvgIcon? Icon { get; set; }
    }
}
