using GeoJSON.Net.Converters;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Map;

namespace PROWeb.Data.Services.CachedData
{
    public class CachedDataService : ICachedDataService
    {
        public int RegistrationYear { get; private set; }

        public List<int> RegistrationYears { get; private set; } = Enumerable.Empty<int>().ToList();

        public List<Country> Countries { get; private set; } = Enumerable.Empty<Country>().ToList();

        public List<VoterFlag> Flags { get; private set; } = Enumerable.Empty<VoterFlag>().ToList();

        public List<Constituency> Constituencies { get; private set; } = Enumerable.Empty<Constituency>().ToList();

        public List<Parish> Parishes { get; private set; } = Enumerable.Empty<Parish>().ToList();

        public List<char> Genders { get; private set; } = Enumerable.Empty<char>().ToList();

        public List<string> Titles { get; private set; } = Enumerable.Empty<string>().ToList();

        public string? ConstituenciesGeoJSON { get; private set; }
        
        public List<ConstituencyMarker>? ConstituencyMarkers { get; private set; }

        public async Task PreloadCachedDataAsync(DataContext context)
        {
            PreloadPersonData();
            await PreloadOfficeDataAsync(context);
            await PreloadFlagsDataAsync(context);
            await PreloadConstituencyBounderiesAsync(context);
            //await PreloadAssessmentDataAsync(context);
        }

        private void PreloadPersonData()
        {
            Titles = new List<string>() { "MS", "MISS", "MR", "MRS", "DR", "JP", "MP" };
            Genders = new List<char>() { 'M', 'F', 'N' };
        }

        private async Task PreloadOfficeDataAsync(DataContext context)
        {
            var office = await context.PROOffices.FirstOrDefaultAsync();

            RegistrationYear = office?.ElectionYear ?? DateTime.Now.Year;

            RegistrationYears = Enumerable.Range(DateTime.Now.Year - 2, 3).ToList();
        }

        private async Task PreloadFlagsDataAsync(DataContext context)
        {
            Countries = await context.Countries.ToListAsync();
            Flags = await context.VoterFlags.ToListAsync();
        }

        private async Task PreloadAssessmentDataAsync(DataContext context)
        {
            Constituencies = await context.Constituencies.ToListAsync();
            Parishes = await context.Parishes.ToListAsync();
        }

        private async Task PreloadConstituencyBounderiesAsync(DataContext context)
        {
            var bounderies = await context.ConstituencyBoundaries.ToListAsync();

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

            ConstituenciesGeoJSON = JsonConvert.SerializeObject(featureCollection);

            ConstituencyMarkers = bounderies.Adapt<List<ConstituencyMarker>>();
        }
    }
}
