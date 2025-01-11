using Dapper;
using Microsoft.Data.SqlClient;
using PROWeb.WebSiteService.Models;

namespace PROWeb.WebSiteService.Repositories
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

            var result = await connection.QueryAsync<Constituency>($"SELECT {nameof(Constituency.ConstituencyNo)}, {nameof(Constituency.ConstituencyName)} FROM Constituency");

            return result.ToList();
        }

        public async Task<IReadOnlyList<Assessment>> GetAssessmentsAsync
            (
                string? assessmentNo,
                string? houseName,
                string? street,
                string? houseNo,
                string? parish,
                string? postalCode,
                int? constituencyNo,
                string? constituencyName
            )
        {
            var sql = @$"SELECT TOP(10) 
                           AssessmentNo AS {nameof(Assessment.AssessmentNo)}
                          ,Address1 AS {nameof(Assessment.HouseName)}
                          ,Address2 AS {nameof(Assessment.Street)}
                          ,Address3 AS {nameof(Assessment.Parish)}
                          ,HouseNo AS {nameof(Assessment.HouseNo)}
                          ,PostalCode AS {nameof(Assessment.PostalCode)}
                          ,ConstituencyNo AS {nameof(Assessment.ConstituencyNo)}
                          ,ConstituencyName AS {nameof(Assessment.ConstituencyName)}
                      FROM Assessment WHERE
                      (AssessmentNo LIKE @{nameof(assessmentNo)} OR @{nameof(assessmentNo)} IS NULL) AND
                      (Address1 LIKE @{nameof(houseName)} OR @{nameof(houseName)} IS NULL) AND
                      (Address2 LIKE @{nameof(street)} OR @{nameof(street)} IS NULL) AND
                      (HouseNo = @{nameof(houseNo)} OR @{nameof(houseNo)} IS NULL) AND
                      (Address3 LIKE @{nameof(parish)} OR @{nameof(parish)} IS NULL) AND
                      (PostalCode = @{nameof(postalCode)} OR @{nameof(postalCode)} IS NULL)  AND
                      (ConstituencyNo = @{nameof(constituencyNo)} OR @{nameof(constituencyNo)} IS NULL)  AND
                      (ConstituencyName LIKE @{nameof(constituencyName)} OR @{nameof(constituencyName)} IS NULL)";

            using var connection = Connection;

            var result = await connection.QueryAsync<Assessment>(sql, new 
            { 
                assessmentNo = assessmentNo is null ? null : assessmentNo + "%",
                houseName = houseName is null ? null : houseName + "%",
                street = street is null ? null : street + "%",
                houseNo,
                parish = parish is null? null : parish + "%",
                postalCode,
                constituencyNo,
                constituencyName = constituencyName is null ? null : constituencyName + "%"
            });

            return result.ToList();
        }

        public async Task<Voter?> GetVoterAsync(
            string firstName,
            string lastName,
            DateTime dateOfBirth
        )
        {
            var sql = @$"SELECT TOP 1
                            FirstName
                            ,LastName
                            ,MiddleName
                            ,MaidenName
                            ,Title
                            ,DateOfBirth
                            ,AssessmentNo
                            ,HouseNo
                            ,Address1 AS {nameof(Voter.HouseName)}
                            ,Address2 AS {nameof(Voter.Street)}
                            ,Address3 AS {nameof(Voter.Parish)}
                            ,PostalCode
                            ,ConstituencyNo
                            ,ConstituencyName
                            ,Latitude
                            ,Longitude
                            ,IsBogusNo
                            ,BogusNo
                        FROM Voter WHERE
                        FirstName = @{nameof(firstName)} AND
                        LastName =  @{nameof(lastName)} AND
                        DateofBirth = @{nameof(dateOfBirth)} AND
                        RegistryYear IN (SELECT ElectionYear FROM ParliamentaryOffice)";

            using var connection = Connection;

            var result = await connection.QueryAsync<Voter>(sql, new
            {
                firstName,
                lastName,
                dateOfBirth
            });

            return result.FirstOrDefault();
        }
    }
}
