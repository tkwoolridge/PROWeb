using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWeb.Data.Models
{
    public class Candidate
    {
        public int? BirthId { get; set; }
        
        public string? BirthLastName { get; set; }
        
        public string? BirthFirstName { get; set; }

        public string? BirthsMiddleName { get; set; }

        public DateTime? BirthDateOfBirth { get; set; }

        public string? BirthsGender { get; set; }
        
        public int? ImmigrationId { get; set; }

        public string? ImmigrationsLastName { get; set; }
        
        public string? ImmigrationsFirstName { get; set; }
        
        public string? ImmigrationsMiddleName { get; set; }
        
        public DateTime? ImmigrationsDateOfBirth { get; set; }

        public string? ImmigrationsGender { get; set; }
        
        public string? ImmigrationsStatus { get; set; }

        public string? ImmigrationsStatusDescription { get; set; }
        
        public DateTime? ImmigrationsAuditChangeDate { get; set; }

        public DateTime? ImmigrationsAuditAddDate { get; set; }

        public DateTime? ImmigrationsStatusAcquired { get; set; }

        public bool? ImmigrationsIsDeceased { get; set; }
        
        public string? DriverLicenseId { get; set; }

        public DateTime? DriverLicensesAuditDate { get; set; }

        public string? DriverLicensesLicenseType { get; set; }

        public string? DriverLicensesLastName { get; set; }
        
        public string? DriverLicensesFirstName { get; set; }
        
        public string? DriverLicensesMiddleName { get; set; }
        
        public DateTime? DriverLicensesDateOfBirth { get; set; }
        
        public char? DriverLicensesGender { get; set; }
        
        public int? DriverLicensesAssessmentNo { get; set; }
        
        public string? DriverLicensesPhotoPath { get; set; }
        
        public int? SortIndex { get; set; }
    }
}
