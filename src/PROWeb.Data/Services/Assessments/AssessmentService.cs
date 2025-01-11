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

        public IQueryable<Constituency> GetConstituencies(int? constituencyNo = null, string? constituencyName = null)
        {
            IQueryable<Constituency> constituencies = Context.Constituencies;

#nullable disable
            constituencies = constituencies
                .WhereIfNotNull(constituencyNo, c => c.ConstituencyNo == constituencyNo)
                .WhereIfNotNull(constituencyName, c => c.ConstituencyName.StartsWith(constituencyName));
#nullable enable

            return constituencies;
        }

        public IQueryable<Parish> GetParishes(int? parishNo = null, string? parishName = null)
        {
            IQueryable<Parish> parishes = Context.Parishes;

#nullable disable
            parishes = parishes
                .WhereIfNotNull(parishNo, p => p.ParishNo == parishNo)
                .WhereIfNotNull(parishName, p => p.ParishName.StartsWith(parishName));
#nullable enable

            return parishes;
        }

        public async Task UpdateParishAsync(Parish parish)
        {
            Context.Attach(parish);

            Context.Parishes.Update(parish);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateConstituencyAsync(Constituency constituency)
        {
            Context.Attach(constituency);

            Context.Constituencies.Update(constituency);
            await Context.SaveChangesAsync();
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
