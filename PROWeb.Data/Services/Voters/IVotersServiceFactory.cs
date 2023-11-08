using PROWeb.Common.Data;

namespace PROWeb.Data.Services.Voters
{
    public interface IVotersServiceFactory : IDbContextServiceFactory<IVotersService, DataContext>
    {
    }
}