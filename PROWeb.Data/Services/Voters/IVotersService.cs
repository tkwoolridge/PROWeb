using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Voters
{
    public interface IVotersService : IDbContextService<DataContext>
    {
        #region Voters

        Voter? GetVoter(int registryYear, int voterId);

        IQueryable<Voter> GetVoters
           (
               int registryYear,
               int? voterId = null,
               string? firstName = null,
               string? lastName = null,
               string? middleName = null,
               string? maidenName = null,
               bool? isEligible = null,
               DateTime? dateOfBirth = null,
               int? ageFrom = null,
               int? ageTo = null,
               string? phone = null,
               int? assessmentNo = null,
               string? streetName = null,
               string? houseNo = null,
               int? constituencyId = null,
               int? parishNo = null,
               string? postalCode = null
           );

        Task UpdateVoter(Voter voter, string userName);

        #endregion

        #region Voter Flags

        IQueryable<VoterFlag> GetVoterFlags();

        #endregion

        #region Documents

        Task<Document?> GetVoterDocumentAsync(int id);

        Task<byte[]?> GetVoterDocumentContentAsync(int id);

        Task AddDocumentAsync(Document document);

        Task DeleteDocumentAsync(int id);

        #endregion

        #region

        Task AddVoter(Voter voter, string user);

        #endregion
    }
}