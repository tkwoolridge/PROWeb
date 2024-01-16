using PROWeb.Data.Models;
using PROWeb.Data.Models.Map;

namespace PROWeb.Data.Services.CachedData
{
    public interface ICachedDataService
    {
        int RegistrationYear { get; }

        List<int> RegistrationYears { get; }

        List<char> Genders { get; }

        List<string> Titles { get; }

        List<Country> Countries { get; }

        List<VoterFlag> Flags { get; }

        List<Constituency> Constituencies { get; }

        List<Parish> Parishes { get; }

        string? ConstituenciesGeoJSON { get; }

        List<ConstituencyMarker>? ConstituencyMarkers { get; }

        Task PreloadCachedDataAsync(DataContext context);
    }
}