using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PROWeb.Data.Services.Configuration
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
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}