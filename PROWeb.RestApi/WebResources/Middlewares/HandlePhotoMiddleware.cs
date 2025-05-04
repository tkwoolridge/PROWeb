using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net;
using PROWeb.RestApi.WebResources.Configuration;
using PROWeb.RestApi.Authentication.Constants;
using Microsoft.AspNetCore.Http.Extensions;
using Ser = Serilog;

namespace PROWeb.RestApi.WebResources.Middlewares
{
    public class HandlePhotoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Ser.ILogger _logger;
        private readonly WebResourcesSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;

        public HandlePhotoMiddleware(
            RequestDelegate next,
            IOptions<WebResourcesSettings> options,
            Ser.ILogger logger,
            IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _logger = logger;
            _settings = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string url = context.Request.GetEncodedPathAndQuery();

            if (url.StartsWith(_settings.TCDPhotosPath) == true)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient("WebResourcesClient");

                    client.DefaultRequestHeaders.Add(ApiKeyConstants.ApiKeyHeaderName, _settings.ApiKey);

                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var photoBytes = await apiResponse.Content.ReadAsByteArrayAsync();
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.ContentType = "image/jpeg";
                        context.Response.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
                        await context.Response.Body.WriteAsync(photoBytes, 0, photoBytes.Length);
                        return;
                    }
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

