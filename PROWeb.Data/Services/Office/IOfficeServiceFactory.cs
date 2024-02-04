using PROWeb.Common.Data;

namespace PROWeb.Data.Services.Office
{
    public interface IOfficeServiceFactory : IDbContextServiceFactory<IOfficeService, DataContext>
    {
    }
}
