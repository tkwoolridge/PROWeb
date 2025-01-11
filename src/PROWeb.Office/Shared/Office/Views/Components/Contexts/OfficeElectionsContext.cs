using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Office.Shared.Office.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Office.Shared.Office.Views.Components.Contexts
{
    public class OfficeElectionsContext : OfficeBaseContext
    {
        private int _electionTypeId;
        private int? _electionYear;
        private DateTime? _nextElectionsDate;
        private string? _nextAdvancedPollDate;

        [Required]
        public int? ElectionYear
        {
            get => _electionYear;
            set => RaiseAndSetIfChanged(ref _electionYear, value);
        }

        [Required]
        public DateTime? NextElectionsDate
        {
            get => _nextElectionsDate;
            set => RaiseAndSetIfChanged(ref _nextElectionsDate, value);
        }

        [Required]
        public string? NextAdvancedPollDate
        {
            get => _nextAdvancedPollDate;
            set => RaiseAndSetIfChanged(ref _nextAdvancedPollDate, value);
        }

        [Required]
        public int ElectionTypeId
        {
            get => _electionTypeId;
            set => RaiseAndSetIfChanged(ref _electionTypeId, value);
        }

        public override void Bind(OfficeViewModel model)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model.Bind(this, m => m.NextElectionsDate, o => o.NextElectionsDate, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.NextAdvancedPollDate, o => o.NextAdvancedPollDate, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.ElectionYear, o => o.ElectionYear, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.ElectionTypeId, o => o.ElectionTypeId, StrongBindingMode.TwoWay));
            }
        }
    }
}
