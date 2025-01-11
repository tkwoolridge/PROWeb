using PROWeb.Common.Data;

namespace PROWeb.Data.Services.Map
{
    public interface IMapServiceFactory : IDbContextServiceFactory<IMapService, DataContext>
    {
    }
}
