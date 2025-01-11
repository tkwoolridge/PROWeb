using Mapster;
using PROWeb.Data.Models;
using PROWeb.Data.Models.Map;

namespace PROWeb.Data.Mappings
{
    internal class DataMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ConstituencyBoundary, ConstituencyMarker>()
                .Map(d => d.Center, s => new[] { s.CenterLatitude, s.CenterLongitude });
        }
    }
}
