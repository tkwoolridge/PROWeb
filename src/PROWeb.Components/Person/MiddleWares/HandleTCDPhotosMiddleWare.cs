using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using PROWeb.Components.Configurations;
using PROWeb.ExternalResources.TCDResources;
using System.Net;
using Ser = Serilog;

namespace PROWeb.Components.Person.MiddleWares
{
    public class HandleTCDPhotosMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly Ser.ILogger _logger;
        private readonly TCDSettings _settings;

        public HandleTCDPhotosMiddleWare(
            RequestDelegate next,
            Ser.ILogger logger,
            IOptions<TCDSettings> options)
        {
            _next = next;
            _logger = logger;
            _settings = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string url = context.Request.GetEncodedPathAndQuery();

            if (url.StartsWith(_settings.TCDPhotosPath))
            {
                try
                {
                    TCDClient client = new TCDClient(_settings.ServiceUrl);

                    string personId = Path.GetFileNameWithoutExtension(context.Request.Query["TCDPhoto"]);

                    var response = await client.GetPhotoAsync(personId);

                    var photoBytes = response.Photo!;
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "image/jpeg";
                    context.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
                    await context.Response.Body.WriteAsync(photoBytes, 0, photoBytes.Length);
                }
                catch(Exception exception)
                {
                    _logger.Error(exception, $"Error while handling photo request: {url}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;   
                }

                return;
            }

            await _next(context);
        }
    }
}

