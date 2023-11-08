using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Assessments
{
    public interface IAssessmentService : IDbContextService<DataContext>
    {
        IQueryable<Constituency> GetConstituencies();

        IQueryable<Parish> GetParishes();

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
