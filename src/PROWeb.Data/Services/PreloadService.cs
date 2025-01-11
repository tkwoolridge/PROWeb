using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PROWeb.Components.Reports.Configuration;
using PROWeb.Data.Services.CachedData;
using PROWeb.Data.Services.Configuration;

namespace PROWeb.Data.Services
{
    public class PreloadService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public PreloadService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var navigationService = scope.ServiceProvider.GetRequiredService<INavigationService>();
                await navigationService.PreloadConfigAsync();

                var reportsService = scope.ServiceProvider.GetRequiredService<IReportsService>();
                await reportsService.PreloadConfigAsync();

                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                var cachedDataService = scope.ServiceProvider.GetRequiredService<ICachedDataService>();
                await cachedDataService.PreloadCachedDataAsync(dataContext);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}