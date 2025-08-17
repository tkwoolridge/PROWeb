using PROWeb.ExternalResources.TCDResources.Contracts;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace PROWeb.ExternalResources.TCDResources
{
    public class TCDClient : ClientBase<ITCDServiceChannel>, ITCDClient
    {
        public TCDClient(string url)
            : base(GetBindingForEndpoint(), new EndpointAddress(url))
        {
        }

        public async Task<TCDPhotoResponse> GetPhotoAsync(TCDPhotoRequest request)
        {
            var response = await Channel.GetPhotoAsync(request);

            if (response.Status is { } status && status != "OK")
            {
                throw new FaultException<TCDPhotoResponse>(
                     response,
                     new FaultReason(GetMessageFromCode(status)));
            }

            return response;
        }

        public async Task<TCDPhotoResponse> GetPhotoAsync(string personId)
        {
            var auditDateTime = DateTime.Now.ToString("yyyyMMddhhmmss");

            TCDPhotoRequest request = new TCDPhotoRequest
            {
                Token = GetMd5Hash(auditDateTime),
                AuditDateTime = auditDateTime,
                PersonID = personId
            };

            return await GetPhotoAsync(request);
        }

        private static Binding GetBindingForEndpoint()
        {
            CustomBinding binding = new CustomBinding();

            TextMessageEncodingBindingElement textBindingElement = new TextMessageEncodingBindingElement
            {
                MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None),
                WriteEncoding = Encoding.UTF8
            };

            binding.Elements.Add(textBindingElement);

            HttpTransportBindingElement httpBindingElement = new HttpTransportBindingElement
            {
                AllowCookies = true,
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
            };

            binding.Elements.Add(httpBindingElement);

            return binding;
        }

        private static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString().ToUpper();
            }
        }

        private static string GetMessageFromCode(string code)
        {
            return code switch
            {
                "01" => "Audit date and time is missing.",
                "02" => "Audit date and time format is invalid (expected: YYYYMMDDHHMMSS).",
                "03" => "Audit date and time is outside the allowed range (±5 minutes from current time).",
                "04" => "The provided token is invalid.",
                "05" => "Person not found.",
                "06" => "No photo available for the specified person.",
                "OK" => "Request completed successfully.",
                _ => "Unknown error."
            };
        }
    }
}
