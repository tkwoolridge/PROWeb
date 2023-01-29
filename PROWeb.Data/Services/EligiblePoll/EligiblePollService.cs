using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Models;
using PROWeb.Data.Extensions;

namespace PROWeb.Data.Services.EligiblePoll
{
    #region Service Factory

    public class EligiblePollServiceFactory : DbContextServiceFactory<EligiblePollService, DataContext>, IEligiblePollServiceFactory
    {
        public EligiblePollServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override EligiblePollService CreateService()
        {
            return new EligiblePollService(ContextFactory);
        }
    }

    #endregion Service Factory

    public class EligiblePollService : DbContextService<DataContext>, IEligiblePollService
    {
        public EligiblePollService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<List<Eligible>> GetVoterCandidatesAsync(
            string? firstName,
            string? lastName,
            DateTime? dateOfBirth
            )
        {
            var lastNameParam = lastName.ToSqlParameter();
            var firstNameParam = firstName.ToSqlParameter();
            var dateOfBirthParam = dateOfBirth.ToSqlParameter();

            return await Context.EligiblePoll.FromSqlInterpolated($"GetEligible {lastNameParam}, {firstNameParam}, {dateOfBirthParam}").ToListAsync();
        }
    }
}
