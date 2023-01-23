using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Candidates
{
    public interface ICandidatesService
    {
        Task<List<Candidate>> GetCandidatesAsync (string? firstName, string? lastName, DateTime? dateOfBirth);
    }
}