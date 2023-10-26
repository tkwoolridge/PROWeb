using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Registrations
{
    public interface IRegistrationService : IDbContextService<DataContext>
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

        Task AddRegistration(Registration registration, string user);

        Task UpdateRegistration(Registration registration, string user);
    }
}