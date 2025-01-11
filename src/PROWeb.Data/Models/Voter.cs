#nullable disable

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROWeb.Data.Models
{
    public class Voter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoterId { get; set; }

        public int RegistryYear { get; set; }

        public int? BirthID { get; set; }

        public int? ImmigrationID { get; set; }

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
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string MaidenName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        public int AssessmentNo { get; set; }

        public Assessment Assessment { get; set; }

        [Required]
        public bool IsEligible { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

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

        public bool? WasBornIn { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public bool? CommonwealthCitizen { get; set; }

        public DateTime? BermudianStatusGranted { get; set; }

        public bool? RegisteredAsElector { get; set; }

        public bool? IsBermudianStatusGranted { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public string LastUpdatedBy { get; set; }

        public ICollection<VoterFlag> Flags { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<VoterHistory> VoterHistories { get; set; }
    }
}
