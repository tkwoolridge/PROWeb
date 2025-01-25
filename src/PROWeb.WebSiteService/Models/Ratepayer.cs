namespace PROWeb.WebSiteService.Models
{
    public class Ratepayer
    {
        public int CorporationID { get; set; }

        public string? CompanyName { get; set; }

        public string AssessmentNo { get; set; } = null!;

        public string? HouseNo { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Address3 { get; set; }

        public string? PostalCode { get; set; }

        public string? NomineeFirstName { get; set; }

        public string? NomineeLastName { get; set; }

        public string? NomineeMiddleName { get; set; }
    }
}
