using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Reporting;

namespace PROWeb.Data.Models.Reports
{
    [XmlRoot(ElementName = "Parameter", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Reports")]
    public class ReportParameter : IXmlSerializable
    {
        [XmlAttribute]
        public string? Name { get; set; }

        [XmlAttribute]
        public ReportParameterType Type { get; set; }


        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();

            Name = reader.GetAttribute(nameof(Name));
            
            if(Enum.TryParse(reader.GetAttribute(nameof(Type)),out ReportParameterType type))
            {
                Type = type;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(Name), Name);
            writer.WriteAttributeString(nameof(Type), Type.ToString());
        }
    }
}
