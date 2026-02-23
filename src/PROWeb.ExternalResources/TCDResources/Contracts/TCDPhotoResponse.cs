using System.Xml.Serialization;
using PROWeb.ExternalResources.TCDResources.Configurations;

namespace PROWeb.ExternalResources.TCDResources.Contracts
{
    [XmlType(TypeName = "RETPROInfo", Namespace = $"{Constants.TCDService.TCDServiceNamespace}")]
    public class TCDPhotoResponse
    {
        [XmlElement(ElementName ="photoJPG",  DataType = "base64Binary", IsNullable = true, Order = 0)]
        public byte[]? Photo { get; set; }

        [XmlElement(ElementName = "aaStatus", IsNullable = true, Order = 1)]   
        public string? Status { get; set; }
    }
}
