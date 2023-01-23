namespace PROWeb.Data.Services.Voters
{
    public interface IVotersServiceFactory : IDbContextServiceFactory<VotersService, DataContext>
    {
    }
}