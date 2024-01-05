using Mapster;
using Microsoft.AspNetCore.Components;
using PROWeb.Common.Components;
using PROWeb.Components.Assessments.ViewModels;
using PROWeb.Data.Services.CachedData;
using System.Diagnostics;
namespace PROWeb.Components.Assessments
{
    public partial class MapView : PROComponent
    {
        protected string[] SubDomains { get; set; } = new string[] { "a", "b", "c" };

        protected string UrlTemplate { get; set; } = "https://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png";

        protected string Attribution { get; set; } = "&copy; <a href='https://osm.org/copyright'>OpenStreetMap contributors</a>";

        [Parameter]
        public double Zoom { get; set; } = 16;

        [Parameter]
        public string Height { get; set; } = "400px";

        [Parameter]
        public AddressMarker? Marker { get; set; }

        private void UpdateAddressMarkers()
        {
            Debug.Assert(Marker != null);

            AddressMarkers = new List<AddressMarker>()
            {
                Marker
            };
        }

        [Inject]
        protected ICachedDataService _cachedDataService { get; set; } = default!;

        protected IList<ConstituencyMarkerViewModel>? Markers { get; set; }

        protected IList<AddressMarker>? AddressMarkers { get; set; }


        public string? WorldData { get; set; }

        protected override void OnInitialized()
        {
            if(Marker == null)
            {
                return;
            }

            WorldData = _cachedDataService.ConstituenciesGeoJSON;

            Markers = _cachedDataService.ConstituencyMarkers?.Adapt<List<ConstituencyMarkerViewModel>>();

            UpdateAddressMarkers();
        }
    }

    public record AddressMarker
    (
        double[] Location,
        string Address
    );
}
