using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;
using PROWeb.Data.Extensions;
using PROWeb.Data.Models;
using System.Diagnostics;

namespace PROWeb.Data.Services.Voters
{
    #region Service Factory

    public class VotersServiceFactory : DataContextServiceFactory<IVotersService>, IVotersServiceFactory
    {
        public VotersServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override IVotersService CreateService()
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

        #region Voters

        public async Task<Voter?> GetVoter(int registryYear, int voterId)
        {
            var querable = GetVoters(
                Context.Voters,
                registryYear,
                voterId)
                .Include(v => v.Assessment)
                .Include(v => v.Country)
                .Include(v => v.Assessment.Parish)
                .Include(v => v.Assessment.Constituency);

            return await querable
                .FirstOrDefaultAsync();
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
            voters = voters.Where(v => v.RegistryYear == registryYear);
            voters = voters.WhereIfNotNull(voterId, v => v.VoterId == voterId);

            if (!voterId.HasValue)
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

        public async Task UpdateVoterAsync(Voter voter)
        {
            var flags = voter.Flags?.ToList();
            voter.Flags?.Clear();

            Voter? curentVoter = Context.Voters.AsNoTracking().FirstOrDefault(v => v.VoterId == voter.VoterId);

            Debug.Assert(curentVoter != null);

            if (flags is not null)
            {
                Context.AttachRange(flags);
            }

            if (Context.Entry(voter).State == EntityState.Detached)
            {
                Context.Attach(voter);
            }

            voter = await Context.Voters.Include(l => l.Flags).SingleAsync(v => voter.Equals(v));
            voter.Flags = flags;

            Context.Update(voter);

            await Context.SaveChangesAsync();
        }

        public async Task AddVoterHistory(VoterHistory history)
        {
            Context.VoterHistories.Add(history);

            await Context.SaveChangesAsync();
        }

        public async Task AddVoterAsync(Voter voter)
        {
            Context.Voters.Add(voter);

            await Context.SaveChangesAsync();
        }

        #endregion

        #region Voter History

        public IQueryable<VoterHistory> GetVoterHistories(int registryYear, int voterId)
        {
            return Context.VoterHistories
                .Where(h => h.VoterId == voterId && h.RegistryYear == registryYear);
        }

        public IQueryable<VoterHistoryField> GetVoterHistoryFields(int registryYear, int voterId, DateTime created)
        {
            return Context.VoterHistoryFields
                .Where(f => f.VoterId == voterId && f.RegistryYear == registryYear && f.Created == created);
        }

        #endregion

        #region VoterFlags

        public IQueryable<VoterFlag> GetVoterFlags(string? flagDescription = null)
        {
            IQueryable<VoterFlag> flags = Context.VoterFlags;

#nullable disable
            flags = flags
                .WhereIfNotNull(flagDescription, f => f.FlagDescription.StartsWith(flagDescription));
#nullable enable

            return flags;
        }

        public async Task UpdateVoterFlagAsync(VoterFlag flag)
        {
            Context.VoterFlags.Update(flag);
            await Context.SaveChangesAsync();
        }

        public async Task<VoterFlag> AddVoterFlagAsync(VoterFlag flag)
        {
            Context.VoterFlags.Add(flag);
            await Context.SaveChangesAsync();

            return flag;
        }

        #endregion

        #region Countries

        public IQueryable<Country> GetCountries()
        {
            return Context.Countries;
        }

        #endregion

        #region Documents

        public async Task<byte[]?> GetVoterDocumentContentAsync(int id)
        {
            return await Context.Documents
                .Where(d => d.DocumentId == id)
                .Select(d => d.Content)
                .FirstOrDefaultAsync();
        }

        public async Task<Document?> GetVoterDocumentAsync(int id)
        {
            return await Context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        public async Task AddDocumentAsync(Document document)
        {
            Context.Documents.Add(document);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            Document? document = await Context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);

            if (document != null)
            {
                Context.Documents.Remove(document);

                await Context.SaveChangesAsync();
            }
        }

        #endregion
    }
}
