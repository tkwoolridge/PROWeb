using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Office.Shared.Office.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Office.Shared.Office.Views.Components.Contexts
{
    public class OfficeYearEndContext : OfficeBaseContext
    {
        private int? _yearEndMonth;
        private int? _yearEndDay;

        [Required]
        public int? YearEndMonth
        {
            get => _yearEndMonth;
            set => RaiseAndSetIfChanged(ref _yearEndMonth, value);
        }

        [Required]
        public int? YearEndDay
        {
            get => _yearEndDay;
            set => RaiseAndSetIfChanged(ref _yearEndDay, value);
        }

        public override void Bind(OfficeViewModel model)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model.Bind(this, m => m.YearEndDay, o => o.YearEndDay, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.YearEndMonth, o => o.YearEndMonth, StrongBindingMode.TwoWay));
            }
        }
    }
}
