using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PROWeb.Data.Models.Reports
{
    [XmlRoot(ElementName = "Reports", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Reports")]
    public class ReportsConfig : IXmlSerializable
    {
        public List<ReportInfo> Reports { get; set; } = new();

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlReaderSettings readerSetings = new XmlReaderSettings();
            readerSetings.IgnoreComments = true;
            readerSetings.ConformanceLevel = ConformanceLevel.Auto;
            readerSetings.IgnoreWhitespace = true;

            XmlReader helpItemsReader = XmlReader.Create(reader.ReadSubtree(), readerSetings);
            helpItemsReader.ReadStartElement();

            DataContractSerializer serializer = new DataContractSerializer(typeof(ReportInfo));

            while (helpItemsReader.Depth > 0)
            {
                if (serializer.ReadObject(helpItemsReader) is ReportInfo report)
                {
                    Reports.Add(report);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(ReportInfo));

            foreach (ReportInfo report in Reports)
            {
                serializer.WriteObject(writer, report);
            }
        }
    }
}
