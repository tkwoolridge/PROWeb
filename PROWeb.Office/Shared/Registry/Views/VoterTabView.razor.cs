using Microsoft.AspNetCore.Components;
using PROWeb.Components.Common.Views;
using PROWeb.Components.Extensions;
using PROWeb.Data.Models;
using PROWeb.Data.Services.Voters;
using PROWeb.Office.Shared.Registry.ViewModels;

namespace PROWeb.Office.Shared.Registry.Views
{
    public partial class VoterTabView : PROCompositView<ListVoterViewModel>
    {
        [Inject]
        private IVotersServiceFactory _voterServiceFactory { get; set; } = null!;

        [Parameter]
        public bool Editable { get; set; }

        protected int TabIndex
        {
            get => LayoutRef?.Model?.TabIndex ?? 0;
            set
            {
                if (LayoutRef?.Model is { } model)
                {
                    model.TabIndex = value;
                }
            }
        }

        protected override async Task SaveAsync(ListVoterViewModel model)
        {
            Voter? voter = LayoutRef?.Model?.MapTo<Voter>(Mapper);

            if (voter == null) { return; }

            using (var service = _voterServiceFactory.CreateService())
            {
                await service.UpdateVoter(voter, "pro1");
            }
        }
    }
}
