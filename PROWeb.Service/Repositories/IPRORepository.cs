using Microsoft.Data.SqlClient;
using PROWeb.Service.Models;

namespace PROWeb.Service.Repositories
{
    public interface IProRepository
    {
        SqlConnection Connection { get; }

        Task<IReadOnlyList<Constituency>> GetConstituenciesAsync();
    }
}