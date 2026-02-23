using System.ServiceModel;
using PROWeb.ExternalResources.TCDResources.Configurations;
using PROWeb.ExternalResources.TCDResources.Contracts;

namespace PROWeb.ExternalResources.TCDResources
{
    [ServiceContract(Namespace = $"{Constants.TCDService.TCDServiceNamespace}", ConfigurationName = $"{nameof(ITCDServiceChannel)}")]
    public interface ITCDServiceChannel
    {
        [OperationContract(Name = "GETPROInfoReq", Action = "GETPROInfoReq", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [return: MessageParameter(Name = "GETPROInfoReqReturn")]
        Task<TCDPhotoResponse> GetPhotoAsync(TCDPhotoRequest request);
    }
}
