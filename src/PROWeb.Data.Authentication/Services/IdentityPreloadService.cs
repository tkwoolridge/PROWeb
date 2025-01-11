// Ignore Spelling: Preload

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PROWeb.Data.Authentication.Services.CachedData;

namespace PROWeb.Data.Authentication.Services
{
    public class IdentityPreloadService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public IdentityPreloadService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();
                var cachedDataService = scope.ServiceProvider.GetRequiredService<ICachedIdentityDataService>();
                await cachedDataService.PreloadCachedDataAsync(dataContext);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
