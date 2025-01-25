using PROWeb.WebSiteService.Contracts.Responses;

namespace PROWeb.WebSiteService.Services
{
    public interface IProService
    {
        Task<IReadOnlyList<ConstituencyResponse>> GetConstituenciesAsync();

        Task<IReadOnlyList<AssessmentResponse>> GetAssessmentsAsync(
            string? assessmentNo,
            string? houseName,
            string? street,
            string? houseNo,
            string? parish,
            string? postalCode,
            int? constituencyNo,
            string? constituencyName
        );

        Task<VoterResponse?> GetVoterAsync(
            string firstName,
            string lastName,
            DateTime dateOfBirth
        );

        Task<IReadOnlyList<RatepayerResponse>> GetRatepayerAsync(
            int corporationID,
            string assessmentNo
        );

        Task<List<JPVoterResponse>> GetJPVotersAsync(int constituencyNo);
    }
}