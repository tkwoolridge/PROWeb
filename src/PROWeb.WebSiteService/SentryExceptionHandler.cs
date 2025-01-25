using Microsoft.AspNetCore.Diagnostics;

namespace PROWeb.WebSiteService
{
    internal sealed class SentryExceptionHandler : IExceptionHandler
    {
        public SentryExceptionHandler()
        {
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            SentrySdk.CaptureException(exception);

            return await Task.FromResult(false);
        }
    }
}
