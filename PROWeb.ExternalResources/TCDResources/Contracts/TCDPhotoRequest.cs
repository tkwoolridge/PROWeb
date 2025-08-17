using System.Xml.Serialization;
using PROWeb.ExternalResources.TCDResources.Configurations;

namespace PROWeb.ExternalResources.TCDResources.Contracts
{
    [XmlType(TypeName = "GETPROInfoReq", Namespace = $"{Constants.TCDService.TCDServiceNamespace}")]
    public class TCDPhotoRequest
    {
        [XmlElement(ElementName = "token", IsNullable = true, Order = 0)]
        public string? Token { get; set; }

        [XmlElement(ElementName = "auditDateTime", IsNullable = true, Order = 1)]
        public string? AuditDateTime { get; set; }

        [XmlElement(ElementName = "personID", IsNullable = true, Order = 2)]
        public string? PersonID { get; set; }
    }
}
