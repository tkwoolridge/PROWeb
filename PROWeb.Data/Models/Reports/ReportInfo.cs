using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PROWeb.Data.Models.Reports
{
    [XmlRoot(ElementName = "Report", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Reports")]
    public class ReportInfo : IXmlSerializable
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string? FileName { get; set; }

        [XmlAttribute]
        public string? Name { get; set; }

        [XmlAttribute]
        public string? Description { get; set; }

        public ReportFilterGroup Filters { get; set; } = new ReportFilterGroup();

        public List<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();

        #region IXmlSerializable Members

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();

            if (int.TryParse(reader.GetAttribute(nameof(Id)), out int id))
            {
                Id = id;
            }

            Name = reader.GetAttribute(nameof(Name));
            FileName = reader.GetAttribute(nameof(FileName));
            Description = reader.GetAttribute(nameof(Description));

            XmlReaderSettings readerSetings = new XmlReaderSettings();
            readerSetings.IgnoreComments = true;
            readerSetings.ConformanceLevel = ConformanceLevel.Auto;
            readerSetings.IgnoreWhitespace = true;

            XmlReader helpItemsReader = XmlReader.Create(reader.ReadSubtree(), readerSetings);
            helpItemsReader.ReadStartElement();

            while (helpItemsReader.Depth > 0)
            {
                Type itemType = helpItemsReader.Name switch
                {
                    "FilterGroup" => typeof(ReportFilterGroup),
                    "Parameter" => typeof(ReportParameter),
                    _ => typeof(ReportFilter),
                };

                DataContractSerializer serializer = new DataContractSerializer(itemType);

                switch(serializer.ReadObject(helpItemsReader))
                {
                    case IReportFilter filter:
                    {
                        Filters.Filters.Add(filter);
                        break;
                    }
                    case ReportParameter parameter:
                    {
                        Parameters.Add(parameter);
                        break;
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(Id), Id.ToString());
            writer.WriteAttributeString(nameof(Name), Name);
            writer.WriteAttributeString(nameof(FileName), FileName);
            writer.WriteAttributeString(nameof(Description), Description);

            foreach (IReportFilter filter in Filters.Filters)
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

        #endregion
    }
}
