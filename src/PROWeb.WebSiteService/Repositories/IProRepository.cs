using Microsoft.Data.SqlClient;
using PROWeb.WebSiteService.Models;

namespace PROWeb.WebSiteService.Repositories
{
    public interface IProRepository
    {
        SqlConnection Connection { get; }

        Task<IReadOnlyList<Constituency>> GetConstituenciesAsync();

        Task<IReadOnlyList<Assessment>> GetAssessmentsAsync(
            string? assessmentNo,
            string? houseName,
            string? street,
            string? houseNo,
            string? parish,
            string? postalCode,
            int? constituencyNo,
            string? constituencyName
        );

        Task<Voter?> GetVoterAsync(
            string firstName,
            string lastName,
            DateTime dateOfBirth
        );
    }
}