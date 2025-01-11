using Mapster;
using PROWeb.WebSiteService.Contracts.Responses;
using PROWeb.WebSiteService.Repositories;

namespace PROWeb.WebSiteService.Services
{
    public class ProService : IProService
    {
        private readonly IProRepository _repository;

        public ProService(IProRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<AssessmentResponse>> GetAssessmentsAsync(
            string? assessmentNo, 
            string? houseName, 
            string? street, 
            string? houseNo, 
            string? parish, 
            string? postalCode, 
            int? constituencyNo, 
            string? constituencyName)
        {
            var response = await _repository.GetAssessmentsAsync(
                assessmentNo,
                houseName, 
                street, 
                houseNo, 
                parish, 
                postalCode, 
                constituencyNo, 
                constituencyName);

            return response.Adapt<List<AssessmentResponse>>();
        }

        public async Task<IReadOnlyList<ConstituencyResponse>> GetConstituenciesAsync()
        {
            var response = await _repository.GetConstituenciesAsync();

            return response.Adapt<List<ConstituencyResponse>>();
        }

        public async Task<VoterResponse?> GetVoterAsync(
            string firstName,
            string lastName,
            DateTime dateOfBirth
        )
        {
            var result = await _repository.GetVoterAsync(firstName, lastName, dateOfBirth);

            return result.Adapt<VoterResponse>();
        }
    }
}
