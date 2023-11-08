using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.EligiblePoll
{
    public interface IEligiblePollService : IDbContextService<DataContext>
    {
        Task<List<Eligible>> GetVoterCandidatesAsync(string? firstName, string? lastName, DateTime? dateOfBirth);
    }
}