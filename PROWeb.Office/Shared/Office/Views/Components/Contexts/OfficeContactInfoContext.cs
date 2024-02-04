using PROWeb.Common.StrongBindings.Enums;
using System.ComponentModel.DataAnnotations;
using PROWeb.Common.StrongBindings.Extensions;
using PROWeb.Office.Shared.Office.ViewModels;

namespace PROWeb.Office.Shared.Office.Views.Components.Contexts
{
    public class OfficeContactInfoContext : OfficeBaseContext
    {
        private string? _registerName;
        private string? _address1;
        private string? _address2;
        private string? _address3;
        private string? _address4;
        private string? _phone;
        private string? _fax;
        private string? _email;
        private string? _website;
        private string? _assistantName;

        public string? RegisterName
        {
            get => _registerName;
            set => RaiseAndSetIfChanged(ref _registerName, value);
        }

        public string? AssistantName
        {
            get => _assistantName;
            set => RaiseAndSetIfChanged(ref _assistantName, value);
        }

        [StringLength(255)]
        public string? Address1
        {
            get => _address1;
            set => RaiseAndSetIfChanged(ref _address1, value);
        }

        [StringLength(255)]
        public string? Address2
        {
            get => _address2;
            set => RaiseAndSetIfChanged(ref _address2, value);
        }

        [StringLength(255)]
        public string? Address3
        {
            get => _address3;
            set => RaiseAndSetIfChanged(ref _address3, value);
        }

        [StringLength(255)]
        public string? Address4
        {
            get => _address4;
            set => RaiseAndSetIfChanged(ref _address4, value);
        }

        [StringLength(50)]
        public string? Phone
        {
            get => _phone;
            set => RaiseAndSetIfChanged(ref _phone, value);
        }

        [StringLength(50)]
        public string? Fax
        {
            get => _fax;
            set => RaiseAndSetIfChanged(ref _fax, value);
        }

        [StringLength(150)]
        public string? Email
        {
            get => _email;
            set => RaiseAndSetIfChanged(ref _email, value);
        }

        [StringLength(150)]
        public string? Website
        {
            get => _website;
            set => RaiseAndSetIfChanged(ref _website, value);
        }

        public override void Bind(OfficeViewModel model)
        {
            Model = model;

            using (SuspendSubscriptions())
            {
                AddBinding(Model.Bind(this, m => m.RegisterName, o => o.RegisterName, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.AssistantName, o => o.AssistantName, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Address1, o => o.Address1, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Address2, o => o.Address2, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Address3, o => o.Address3, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Address4, o => o.Address4, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Fax, o => o.Fax, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Phone, o => o.Phone, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Email, o => o.Email, StrongBindingMode.TwoWay));
                AddBinding(Model.Bind(this, m => m.Website, o => o.Website, StrongBindingMode.TwoWay));
            }
        }
    }
}
