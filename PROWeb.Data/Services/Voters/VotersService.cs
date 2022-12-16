using PROWeb.Data.Models;
using PROWeb.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Services.Assessments;

namespace PROWeb.Data.Services.Voters
{
    #region Service Factory

    public class VotersServiceFactory : DbContextServiceFactory<VotersService, DataContext>, IVotersServiceFactory
    {
        public VotersServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override VotersService CreateService()
        {
            return new VotersService(ContextFactory);
        }
    }

    #endregion Service Factory

    public class VotersService : DbContextService<DataContext>, IVotersService
    {
        public VotersService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public IQueryable<VoterFlag> GetVoterFlags()
        {
            return Context.VoterFlags;
        }

        public Voter? GetVoter(int registryYear, int voterId)
        {
            return GetVoters(
                Context.Voters,
                registryYear,
                voterId)
                .FirstOrDefault();
        }

        public IQueryable<Voter> GetVoters
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
            return GetVoters
            (
                Context.Voters,
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

        private IQueryable<Voter> GetVoters
            (
                IQueryable<Voter> voters,
                int registryYear,
                int? voterId = null,
                string? firstName = null,
                string? lastName = null,
                string? middleName = null ,
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
            voters = voters.Where(v => v.RegistryYear == registryYear);
            voters = voters.WhereIfNotNull(voterId, v => v.VoterId == voterId);
            
            if(!voterId.HasValue)
            {
#nullable disable
                voters =
                    voters
                    .WhereIfNotNull(firstName, v => v.FirstName.StartsWith(firstName))
                    .WhereIfNotNull(lastName, v => v.LastName.StartsWith(lastName))
                    .WhereIfNotNull(middleName, v => v.MiddleName.StartsWith(middleName))
                    .WhereIfNotNull(maidenName, v => v.MaidenName.StartsWith(maidenName))
                    .WhereIfNotNull(isEligible, v => v.IsEligible)
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

            return voters;
        }

        //public void UpdateVoter(VoterViewModel voter)
        //{

        //}
    }
}
