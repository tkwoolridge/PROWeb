namespace PROWeb.Data.Models
{
    public class VoterView
    {
        public int VoterId { get; set; }

        public int RegistryYear { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? MaidenName { get; set; }

        public string? FullName { get; set; }

        public char Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }

        public bool? Email { get; set; }

        public string? ContactPhone { get; set; }

        public string? DriverLicense { get; set; }

        public int AssessmentNo { get; set; }

        public string? Address1 { get; set; }

        public string? HouseNo { get; set; }

        public string? Address2 { get; set; }

        public string? PostalCode { get; set; }

        public int ConstituencyNo { get; set; }

        public string? ConstituencyName { get; set; }

        public int ParishNo { get; set; }

        public string? ParishName { get; set; }

        public bool? IsBogusAssessment { get; set; }

        public bool? IsEligible { get; set; }

        public string? Address { get; set; }
    }
}
