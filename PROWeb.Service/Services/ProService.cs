using Mapster;
using PROWeb.Service.Contracts.Responses;
using PROWeb.Service.Repositories;

namespace PROWeb.Service.Services
{
    public class ProService : IProService
    {
        private readonly IProRepository _repository;

        public ProService(IProRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ConstituencyResponse>> GetConstituenciesAsync()
        {
            var response = await _repository.GetConstituenciesAsync();

            return response.Adapt<List<ConstituencyResponse>>();
        }
    }
}
