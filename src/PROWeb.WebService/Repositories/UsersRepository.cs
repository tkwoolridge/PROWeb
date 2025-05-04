using Microsoft.Extensions.Options;
using PROWeb.Common.Security;
using PROWeb.RestApi.Authentication.Configuration;
using PROWeb.RestApi.Authentication.Models;
using PROWeb.WebService.Resources;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PROWeb.WebService.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ServiceUsers _serviceUsers;

        public UsersRepository(IOptions<JwtSettings> jwtOptions)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{typeof(ServiceUsers).FullName}.json.enc";
            string encResource = string.Empty;

            string[] names = assembly.GetManifestResourceNames();

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                Debug.Assert(stream != null);

                using (StreamReader reader = new StreamReader(stream))
                {
                    encResource = reader.ReadToEnd();
                }
            }

            //var result = Encryptor.Encrypt(encResource);

            var key = jwtOptions.Value.Secret; // result.Key;

            var result = new EncryptionResult
            {
                EncryptedData = encResource,
                Key = key
            };

            string users = Decryptor.Decrypt(result);

            _serviceUsers = JsonSerializer.Deserialize<ServiceUsers>(users, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
               {
                    new JsonStringEnumConverter()
               }
            })!;
        }

        public ServiceUser? FindUser(string? userName)
        {
            return _serviceUsers.Users.FirstOrDefault(u => u.UserName!.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
