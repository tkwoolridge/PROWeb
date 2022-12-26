using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Components.ViewModels.Person
{
    public class PersonFilterViewModel
    {
        public int RegistryYear { get; set; }

        [Display(Name = "First Name:")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name:")]
        public string? LastName { get; set; }

        [Display(Name = "Middle Name:")]
        public string? MiddleName { get; set; }

        [Display(Name = "Maiden Name:")]
        public string? MaidenName { get; set; }

        [Display(Name = "Eligible:")]
        public bool? IsEligible { get; set; }

        [Display(Name = "DOB:")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Age From:")]
        public int? AgeFrom { get; set; }

        [Display(Name = "Age To:")]
        public int? AgeTo { get; set; }

        [Display(Name = "Phone:")]

        public string? Phone { get; set; }

        [Display(Name = "Assessment No:")]

        public int? AssessmentNo { get; set; }

        [Display(Name = "Street Name:")]

        public string? StreetName { get; set; }

        [Display(Name = "House No:")]

        public string? HouseNo { get; set; }

        [Display(Name = "Constituency:")]

        public int? ConstituencyNo { get; set; }

        [Display(Name = "Parish:")]

        public int? ParishNo { get; set; }

        [Display(Name = "Postal Code:")]
        public string? PostalCode { get; set; }

        public int? VoterFlagId { get; set; }

        public List<int>? VoterFlags { get; set; }
    }
}
