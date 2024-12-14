using Dapper;
using Microsoft.Data.SqlClient;
using PROWeb.Service.Models;

namespace PROWeb.Service.Repositories
{
    public class ProRepository : IProRepository
    {
        private readonly IConfiguration _configuration;

        public ProRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Connection
        {
            get => new SqlConnection(_configuration.GetConnectionString("ProConnectionString"));
        }

        public async Task<IReadOnlyList<Constituency>> GetConstituenciesAsync()
        {
            using var connection = Connection;

            var result = await connection.QueryAsync<Constituency>("SELECT * FROM Constituency");

            return result.ToList();
        }
    }
}
