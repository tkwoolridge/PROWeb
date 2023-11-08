using Microsoft.EntityFrameworkCore;
using PROWeb.Common.Data;
using PROWeb.Data.Extensions;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Assessments
{
    #region Service Factory

    public class AssessmentServiceFactory : DataContextServiceFactory<IAssessmentService>, IAssessmentServiceFactory
    {
        public AssessmentServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override IAssessmentService CreateService()
        {
            return new AssessmentService(ContextFactory);
        }
    }

    #endregion Service Factory

    public class AssessmentService : DbContextService<DataContext>, IAssessmentService
    {
        public AssessmentService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public IQueryable<Constituency> GetConstituencies()
        {
            return Context.Constituencies;
        }

        public IQueryable<Parish> GetParishes()
        {
            return Context.Parishes;
        }

        public IQueryable<AssessmentFlag> GetAssessmentFlags()
        {
            return Context.AssessmentFlags;
        }

        public IQueryable<Assessment> GetAssessments
        (
            int? assessmentNo,
            string? address1,
            string? address2,
            string? houseNo,
            string? postalCode,
            int? parishNo,
            int? constituencyNo,
            bool? IsBogus
        )
        {
            IQueryable<Assessment> assessments = Context.Assessments;

#nullable disable
            assessments =
                assessments
                .WhereIfNotNull(assessmentNo, a => a.AssessmentNo == assessmentNo)
                .WhereIfNotNull(address1, a => a.Address1.StartsWith(address1))
                .WhereIfNotNull(address2, a => a.Address2.StartsWith(address2))
                .WhereIfNotNull(houseNo, a => a.HouseNo.StartsWith(houseNo))
                .WhereIfNotNull(postalCode, a => a.PostalCode.StartsWith(postalCode))
                .WhereIfNotNull(constituencyNo, a => a.ConstituencyNo == constituencyNo)
                .WhereIfNotNull(parishNo, a => a.ParishNo == parishNo)
                .WhereIfNotNull(IsBogus, a => a.IsBogus == IsBogus);
#nullable enable

            return assessments;
        }
    }
}
