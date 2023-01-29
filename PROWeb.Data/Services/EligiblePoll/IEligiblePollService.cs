using PROWeb.Data.Models;

namespace PROWeb.Data.Services.EligiblePoll
{
    public interface IEligiblePollService
    {
        Task<List<Eligible>> GetVoterCandidatesAsync(string? firstName, string? lastName, DateTime? dateOfBirth);
    }
}