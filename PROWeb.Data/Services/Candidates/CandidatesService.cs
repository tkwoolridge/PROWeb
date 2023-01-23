using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Models;
using PROWeb.Data.Extensions;
using System.Runtime.CompilerServices;

namespace PROWeb.Data.Services.Candidates
{
    #region Service Factory

    public class CandidatesServiceFactory : DbContextServiceFactory<CandidatesService, DataContext>, ICandidatesServiceFactory
    {
        public CandidatesServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override CandidatesService CreateService()
        {
            return new CandidatesService(ContextFactory);
        }
    }

    #endregion Service Factory

    public class CandidatesService : DbContextService<DataContext>, ICandidatesService
    {
        public CandidatesService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<List<Candidate>> GetCandidatesAsync(
            string? firstName,
            string? lastName,
            DateTime? dateOfBirth
            )
        {
            var lastNameParam = lastName.ToSqlParameter();  
            var firstNameParam = firstName.ToSqlParameter();
            var dateOfBirthParam = dateOfBirth.ToSqlParameter();

            return await Context.Candidates.FromSqlInterpolated($"GetCandidateGroups {lastNameParam}, {firstNameParam}, {dateOfBirthParam}").ToListAsync();
        }
    }
}
