using PROWeb.Common.Data;

namespace PROWeb.Data.Services.EligiblePoll
{
    public interface IEligiblePollServiceFactory : IDbContextServiceFactory<IEligiblePollService, DataContext>
    {
    }
}
