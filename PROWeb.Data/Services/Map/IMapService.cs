
using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Map
{
    public interface IMapService : IDbContextService<DataContext>
    {
        Task<string> GetConstituenciesGeoJSON();

        IQueryable<ConstituencyBoundary> GetBoundaries();
    }
}