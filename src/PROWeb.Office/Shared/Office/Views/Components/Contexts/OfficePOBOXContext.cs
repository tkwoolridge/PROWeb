using PROWeb.Common.StrongBindings.Enums;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Office.Shared.Office.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Office.Shared.Office.Views.Components.Contexts
{
    public class OfficePOBOXContext : OfficeBaseContext
    {
        private string? _pO1;
        private string? _pO2;
        private string? _pO3;

        [StringLength(50)]
        public string? PO1
        {
            get => _pO1;
            set => RaiseAndSetIfChanged(ref _pO1, value);
        }

        [StringLength(50)]
        public string? PO2
        {
            get => _pO2;
            set => RaiseAndSetIfChanged(ref _pO2, value);
        }

        [StringLength(50)]
        public string? PO3
        {
            get => _pO3;
            set => RaiseAndSetIfChanged(ref _pO3, value);
        }

        public override void Bind(OfficeViewModel model)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model.Bind(this, m => m.PO1, o => o.PO1, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.PO2, o => o.PO2, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.PO3, o => o.PO3, StrongBindingMode.TwoWay));
            }
        }
    }
}
