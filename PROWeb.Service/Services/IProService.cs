using PROWeb.Service.Contracts.Responses;

namespace PROWeb.Service.Services
{
    public interface IProService
    {
        Task<IReadOnlyList<ConstituencyResponse>> GetConstituenciesAsync();
    }
}