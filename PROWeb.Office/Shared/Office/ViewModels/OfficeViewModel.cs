using PROWeb.Common.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROWeb.Office.Shared.Office.ViewModels
{
    public class OfficeViewModel : SlimViewModelBase
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
        private string? _pO1;
        private string? _pO2;
        private string? _pO3;
        private int? _yearEndMonth;
        private int? _yearEndDay;
        private int? _electionYear;
        private DateTime? _nextElectionsDate;
        private string? _nextAdvancedPollDate;
        private int _id;

        public int Id
        {
            get => _id;
            set => RaiseAndSetIfChanged(ref _id, value);
        }

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

        [Required]
        public int? ElectionYear 
        { 
            get => _electionYear; 
            set => RaiseAndSetIfChanged(ref _electionYear, value); 
        }

        public DateTime? NextElectionsDate 
        { 
            get => _nextElectionsDate; 
            set => RaiseAndSetIfChanged(ref _nextElectionsDate, value); 
        }

        public string? NextAdvancedPollDate 
        { 
            get => _nextAdvancedPollDate; 
            set => RaiseAndSetIfChanged(ref _nextAdvancedPollDate, value); 
        }
    }
}
