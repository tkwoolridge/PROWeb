using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PROWeb.Data.Models.Navigation
{
    [XmlRoot(ElementName = "Menu", Namespace = "http://schemas.datacontract.org/2004/07/PROWeb.Data.Models.Navigation")]
    public class Menu : IXmlSerializable
    {
        public List<MenuItem> MenuItems { get; set; } = new();

        #region IXmlSerializable Members

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlReaderSettings readerSetings = new XmlReaderSettings();
            readerSetings.ConformanceLevel = ConformanceLevel.Auto;
            readerSetings.IgnoreComments = true;
            readerSetings.IgnoreWhitespace = true;

            XmlReader menuItemReader = XmlReader.Create(reader.ReadSubtree(), readerSetings);
            menuItemReader.ReadStartElement();

            while (menuItemReader.Depth > 0)
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(MenuItem));

                if (serializer.ReadObject(menuItemReader) is MenuItem group)
                {
                    MenuItems.Add(group);
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(MenuItem));

            foreach (MenuItem helpItem in MenuItems)
            {
                serializer.WriteObject(writer, helpItem);
            }
        }

        #endregion
    }
}
