using PROWeb.Common.ViewModels;

namespace PROWeb.Office.ViewModels.EligiblePoll
{
    public class EligibleViewModel : SlimViewModelBase
    {
        public int? BirthId { get; set; }

        public string? BirthLastName { get; set; }

        public string? BirthFirstName { get; set; }

        public string? BirthMiddleName { get; set; }

        public DateTime? BirthDateOfBirth { get; set; }

        public char? BirthGender { get; set; }

        public int? ImmigrationId { get; set; }

        public string? ImmigrationLastName { get; set; }

        public string? ImmigrationFirstName { get; set; }

        public string? ImmigrationMiddleName { get; set; }

        public DateTime? ImmigrationDateOfBirth { get; set; }

        public char? ImmigrationGender { get; set; }

        public string? ImmigrationStatus { get; set; }

        public string? ImmigrationStatusDescription { get; set; }

        public DateTime? ImmigrationAuditChangeDate { get; set; }

        public DateTime? ImmigrationAuditAddDate { get; set; }

        public DateTime? ImmigrationStatusAcquired { get; set; }

        public bool? ImmigrationIsDeceased { get; set; }

        public string? DriverLicenseId { get; set; }

        public DateTime? DriverLicenseAuditDate { get; set; }

        public string? DriverLicenseLicenseType { get; set; }

        public string? DriverLicenseLastName { get; set; }

        public string? DriverLicenseFirstName { get; set; }

        public string? DriverLicenseMiddleName { get; set; }

        public DateTime? DriverLicenseDateOfBirth { get; set; }

        public char? DriverLicenseGender { get; set; }

        public int? DriverLicenseAssessmentNo { get; set; }

        public string? DriverLicenseAddress1 { get; set; }

        public string? DriverLicenseHouseNo { get; set; }

        public string? DriverLicenseAddress2 { get; set; }

        public string? DriverLicensePostalCode { get; set; }

        public string? DriverLicenseParishName { get; set; }
        
        public int? DriverLicenseConstituencyNo { get; set; }

        public string? DriverLicenseConstituencyName { get; set; }

        public string? DriverLicensePhotoPath { get; set; }

        public int? SortIndex { get; set; }
    }
}
