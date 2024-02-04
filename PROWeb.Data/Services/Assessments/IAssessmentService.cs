using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Assessments
{
    public interface IAssessmentService : IDbContextService<DataContext>
    {
        IQueryable<Constituency> GetConstituencies(int? constituencyNo = null, string? constituencyName = null);

        IQueryable<Parish> GetParishes(int? parishNo = null, string? parishName = null);

        Task UpdateParishAsync(Parish parish);

        Task UpdateConstituencyAsync(Constituency constituency);

        IQueryable<AssessmentFlag> GetAssessmentFlags();

        IQueryable<Assessment> GetAssessments
            (
                int? assessmentNo,
                string? address1,
                string? address2,
                string? houseNo,
                string? postalCode,
                int? parishNo,
                int? contituencyNo,
                bool? IsBogus
            );
    }
}
