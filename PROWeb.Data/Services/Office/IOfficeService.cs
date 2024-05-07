using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Office
{
    public interface IOfficeService : IDbContextService<DataContext>
    {
        Task<PROOffice?> GetOfficeAsync();

        Task SaveOfficeAsync(PROOffice office);

        IQueryable<ElectionType> GetElectionTypes();
    }
}
