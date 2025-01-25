namespace PROWeb.WebSiteService.Models
{
    public class Assessment
    {
        public string AssessmentNo { get; set; } = null!;

        public string HouseName { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string Parish { get; set; } = null!;

        public string HouseNo { get; set; } = null!;

        public string PostalCode { get; set; } = null!;
        
        public int ConstituencyNo { get; set; }

        public string ConstituencyName { get; set; } = null!;

        public double? Longitude { get; set; }
        
        public double? Latitude { get; set; }
    }
}
