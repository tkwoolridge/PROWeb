using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PROWeb.Data.Models.Navigation
{
    [XmlRoot(ElementName = "MenuItem", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Navigation")]
    public class MenuItem : IXmlSerializable
    {
        #region Properties

        public List<MenuItem> MenuItems { get; set; } = new();

        [XmlAttribute]
        public SecurityRequirement? Requirement { get; set; }

        [XmlAttribute]
        public string? Name { get; set; }

        [XmlAttribute]
        public string? Description { get; set; }

        [XmlAttribute]
        public string? Page { get; set; }

        [XmlAttribute]
        public bool IsExpanded { get; set; } = true;

        #endregion

        #region IXmlSerializable Members

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            Name = reader.GetAttribute("Name");
            Description = reader.GetAttribute("Description");
            Page = reader.GetAttribute("Page");

            if (bool.TryParse(reader.GetAttribute("IsExpanded"), out bool isExpanded))
            {
                IsExpanded = isExpanded;
            }

            if (Enum.TryParse(reader.GetAttribute("Requirement"), out SecurityRequirement requirement))
            {
                Requirement = requirement;
            }

            XmlReaderSettings readerSetings = new XmlReaderSettings();
            readerSetings.IgnoreComments = true;
            readerSetings.ConformanceLevel = ConformanceLevel.Auto;
            readerSetings.IgnoreWhitespace = true;

            XmlReader helpItemsReader = XmlReader.Create(reader.ReadSubtree(), readerSetings);
            helpItemsReader.ReadStartElement();

            while (helpItemsReader.Depth > 0)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(MenuItem));

                if (serializer.ReadObject(helpItemsReader) is MenuItem item)
                {
                    MenuItems.Add(item);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Requirement", Requirement?.ToString());
            writer.WriteAttributeString("Page", Page);
            writer.WriteAttributeString("IsExpanded", IsExpanded.ToString());


            DataContractSerializer serializer = new DataContractSerializer(typeof(MenuItem));

            foreach (MenuItem menuItem in MenuItems)
            {
                serializer.WriteObject(writer, menuItem);
            }
        }

        #endregion
    }
}



