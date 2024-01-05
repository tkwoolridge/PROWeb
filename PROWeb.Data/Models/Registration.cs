#nullable disable

using System.ComponentModel.DataAnnotations;

namespace PROWeb.Data.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        [Required]
        public int? RegistryYear { get; set; }

        public int? VoterId { get; set; }

        public int? BirthId { get; set; }

        public int? ImmigrationId { get; set; }

        [StringLength(10)]
        public string Title { get; set; }

        public char? Initial { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string NewLastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string MaidenName { get; set; }

        [Required]
        public char? Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int? AssessmentNo { get; set; }

        public Assessment Assessment { get; set; }

        public int? OldAssessmentNo { get; set; }

        public Assessment OldAssessment { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string ContactPhone { get; set; }

        [StringLength(50)]
        public string PhoneHome { get; set; }

        [StringLength(50)]
        public string PhoneWork { get; set; }

        [StringLength(50)]
        public string PhoneMobile { get; set; }

        [StringLength(20)]
        public string DriverLicense { get; set; }

        [StringLength(5000)]
        public string Comment { get; set; }

        public bool WasBornIn { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public bool CommonwealthCitizen { get; set; }

        public DateTime BermudianStatusGranted { get; set; }

        public bool RegisteredAsElector { get; set; }

        public bool IsBermudianStatusGranted { get; set; }

        [Required]
        public int RegistrationStatusId { get; set; }

        [Required]
        public int? FormTypeId { get; set; }

        [Required]
        public int RegistrationOriginId { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public string LastUpdatedBy { get; set; }
    }
}
