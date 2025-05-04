using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PROWeb.RestApi.Authentication;
using PROWeb.WebResources.Configurations;
using PROWeb.WebResources.DependencyInjection;
using Srl = Serilog;

namespace PROWeb.WebResources.Endpoints
{
    public static class WebResourcesEndpoints
    {
        public static void MapWebResourcesEndpoints(this WebApplication app)
        {
            var websiteGroup = app.MapGroup(string.Empty)
                .RequireAuthorization(p => p.Requirements.Add(new ApiKeyRequirement()));

            websiteGroup.MapGet($"/{Configuration.Routs.TCDPhotos}", TCDPhotos).WithName("TCDPhotos");
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(byte[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        private static async Task<IResult> TCDPhotos([FromQuery] string name, IOptions<TCDPhotosOptions> options, Srl.ILogger logger)
        {
            var config = options.Value;

            if(config.Path is not { } path)
            {
                return TypedResults.NotFound();
            }

            string filePath = $"{path}/{name}";

            var client = new AsyncFtpClient(config.FTPServer, config.UserName, config.Password);

            try
            {
                await client.AutoConnect();
            }
            catch (Exception exception)
            {
                logger.Error(exception,$"Connection to TCD-FTP server '{config.FTPServer}' failed!.");
                return TypedResults.NotFound();
            }

            
            var test = await client.FileExists(filePath);

            //List<string> files = new List<string>();

            //foreach (FtpListItem item in await client.GetListing(path))
            //{

            // if this is a file
            //if (item.Type == FtpObjectType.File)
            //{

            //files.Add(item.FullName);

            // get the file size
            //long size = await client.GetFileSize(item.FullName);

            // calculate a hash for the file on the server side (default algorithm)
            //FtpHash hash = await client.GetChecksum(item.FullName);
            //}

            // get modified date/time of the file or folder
            //DateTime time = await client.GetModifiedTime(item.FullName);
            //}

            if(await client.FileExists(filePath))
            {
                byte[] imgBuffer;

                using (var stream = new MemoryStream())
                {
                    await client.DownloadStream(stream, filePath);

                    imgBuffer = stream.ToArray();
                }

                return TypedResults.File(imgBuffer);
            }

            return TypedResults.NotFound();
        }
    }
}
