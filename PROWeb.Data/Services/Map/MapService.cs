using GeoJSON.Net.Converters;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PROWeb.Common.Data;
using PROWeb.Data.Models;

namespace PROWeb.Data.Services.Map
{
    #region Service Factory

    public class MapServiceFactory : DataContextServiceFactory<IMapService>, IMapServiceFactory
    {
        public MapServiceFactory(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public override IMapService CreateService()
        {
            return new MapService(ContextFactory);
        }
    }

    #endregion Service Factory

    public class MapService : DbContextService<DataContext>, IMapService
    {
        public MapService(IDbContextFactory<DataContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<string> GetConstituenciesGeoJSON()
        {
            var bounderies = await Context.ConstituencyBoundaries.ToListAsync();

            FeatureCollection featureCollection = new FeatureCollection();

            foreach (var boundary in bounderies)
            {
                var geometry = JsonConvert.DeserializeObject<IGeometryObject>(boundary.Geometry!, new GeometryConverter());

                var props = new Dictionary<string, object>
                {
                    { "name", boundary.ConstituencyName! },
                };

                Feature feature = new Feature(geometry, props, boundary.ConstituencyId.ToString());

                featureCollection.Features.Add(feature);
            }

            return JsonConvert.SerializeObject(featureCollection);
        }

        public IQueryable<ConstituencyBoundary> GetBoundaries()
        {
            return Context.ConstituencyBoundaries;
        }
    }


}
