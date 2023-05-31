using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Registrations
{
    public interface IRegistrationService
    {
        IQueryable<FormType> GetFormTypes();

        IQueryable<Registration> GetRegistrations
            (
               int registryYear,
               int? voterId = null,
               string? firstName = null,
               string? lastName = null,
               string? middleName = null,
               string? maidenName = null,
               bool? isEligible = null,
               DateTime? dateOfBirth = null,
               int? ageFrom = null,
               int? ageTo = null,
               string? phone = null,
               int? assessmentNo = null,
               string? streetName = null,
               string? houseNo = null,
               int? constituencyId = null,
               int? parishNo = null,
               string? postalCode = null
            );
    }
}