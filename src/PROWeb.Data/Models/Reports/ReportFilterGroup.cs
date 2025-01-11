using PROWeb.Data.Models.Enums;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PROWeb.Data.Models.Reports
{
    [XmlRoot(ElementName = "FilterGroup", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Reports")]
    public class ReportFilterGroup : IXmlSerializable, IReportFilter
    {
        public ReportFilterLogicalOperator LogicalOperator { get; set; }

        public List<IReportFilter> Filters { get; set; } = Enumerable.Empty<IReportFilter>().ToList();

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();

            if (Enum.TryParse(reader.GetAttribute(nameof(LogicalOperator)), out ReportFilterLogicalOperator @operator))
            {
                LogicalOperator = @operator;
            }

            XmlReaderSettings readerSetings = new XmlReaderSettings();
            readerSetings.IgnoreComments = true;
            readerSetings.ConformanceLevel = ConformanceLevel.Auto;
            readerSetings.IgnoreWhitespace = true;

            XmlReader helpItemsReader = XmlReader.Create(reader.ReadSubtree(), readerSetings);
            helpItemsReader.ReadStartElement();

            while (helpItemsReader.Depth > 0)
            {
                Type filterType = helpItemsReader.Name switch
                {
                    "ReportFilterGroup" => typeof(ReportFilterGroup),
                    _ => typeof(ReportFilter),
                };

                DataContractSerializer serializer = new DataContractSerializer(filterType);

                if (serializer.ReadObject(helpItemsReader) is IReportFilter filter)
                {
                    Filters.Add(filter);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(LogicalOperator), LogicalOperator.ToString());

            foreach (IReportFilter filter in Filters)
            {
                Type filterType = filter switch
                {
                    ReportFilterGroup => typeof(ReportFilterGroup),
                    _ => typeof(ReportFilter),
                };

                DataContractSerializer serializer = new DataContractSerializer(filterType);

                serializer.WriteObject(writer, filter);
            }
        }
    }
}
