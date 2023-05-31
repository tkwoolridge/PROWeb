using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Extensions;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Registrations
{
    #region Service Factory

    public class RegistrationServiceFactory : DbContextServiceFactory<RegistrationService, DataContext>, IRegistrationServiceFactory
    {
        public RegistrationServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override RegistrationService CreateService()
        {
            return new RegistrationService(ContextFactory);
        }
    }

    #endregion

    public class RegistrationService : DbContextService<DataContext>, IRegistrationService
    {
        public RegistrationService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public IQueryable<FormType> GetFormTypes()
        {
            return Context.FormTypes;
        }

        public IQueryable<Registration> GetRegistrations
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
            )
        {
            return GetRegistrations
            (
                Context.Registrations,
                registryYear,
                voterId,
                firstName,
                lastName,
                middleName,
                maidenName,
                isEligible,
                dateOfBirth,
                ageFrom,
                ageTo,
                phone,
                assessmentNo,
                streetName,
                houseNo,
                constituencyId,
                parishNo,
                postalCode
            );
        }

        private IQueryable<Registration> GetRegistrations
            (
                IQueryable<Registration> registrations,
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
            )
        {
            registrations = registrations.Where(v => v.RegistryYear == registryYear);
            registrations = registrations.WhereIfNotNull(voterId, v => v.VoterId == voterId);


            if (!voterId.HasValue)
            {
#nullable disable
                registrations =
                    registrations
                    .WhereIfNotNull(firstName, v => v.FirstName.StartsWith(firstName))
                    .WhereIfNotNull(lastName, v => v.LastName.StartsWith(lastName))
                    .WhereIfNotNull(middleName, v => v.MiddleName.StartsWith(middleName))
                    .WhereIfNotNull(maidenName, v => v.MaidenName.StartsWith(maidenName))
                    .WhereIfNotNull(dateOfBirth, v => v.DateOfBirth == dateOfBirth)
                    .WhereIfNotNull(ageFrom, v => DateTime.Now.Year - v.DateOfBirth.Year >= ageFrom)
                    .WhereIfNotNull(ageTo, v => DateTime.Now.Year - v.DateOfBirth.Year <= ageTo)
                    .WhereIfNotNull(phone, v => v.ContactPhone.StartsWith(phone))
                    .WhereIfNotNull(assessmentNo, v => v.AssessmentNo == assessmentNo)
                    .WhereIfNotNull(streetName, v => v.Assessment.Address2.StartsWith(streetName))
                    .WhereIfNotNull(houseNo, v => v.Assessment.HouseNo.StartsWith(houseNo))
                    .WhereIfNotNull(constituencyId, v => v.Assessment.ConstituencyNo == constituencyId)
                    .WhereIfNotNull(parishNo, v => v.Assessment.ParishNo == constituencyId)
                    .WhereIfNotNull(postalCode, v => v.Assessment.PostalCode == postalCode);
#nullable enable
            }

            return registrations;
        }
    }

}
