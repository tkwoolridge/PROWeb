namespace PROWeb.WebService.Contracts.Responses
{
    public class VoterResponse
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? Title { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? AssessmentNo { get; set; }

        public string? HouseNo { get; set; }

        public string? HouseName { get; set; }

        public string? Street { get; set; }

        public string? Parish { get; set; }

        public string? PostalCode { get; set; }

        public int? ConstituencyNo { get; set; }

        public string? ConstituencyName { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool? IsBogusNo { get; set; }

        public string? BogusNo { get; set; }
    }
}
