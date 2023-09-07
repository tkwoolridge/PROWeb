using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace PROWeb.Common.Transformations
{
    public class ObjectToHtml
    {
        public static string ToHtml(object obj, string xsltPath) 
        {
            string xml = GetXmlFromObject(obj);

            return TransformXml(xml, xsltPath);
        }

        private static string TransformXml(string xml, string xsltPath)
        {
            var xd = new XmlDocument();
            xd.LoadXml(xml);

            var xslt = new System.Xml.Xsl.XslCompiledTransform();
            xslt.Load(xsltPath);

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            xslt.Transform(xd, null, sw);


            return sb.ToString();
        }

        private static string GetXmlFromObject(object obj)
        {
            StringBuilder sb = new StringBuilder();

            XmlWriter xwt = XmlWriter.Create(sb);

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(xwt, obj, ns);

            xwt.Flush();
            xwt.Close();

            return sb.ToString();
        }
    }
}
