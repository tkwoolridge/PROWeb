using System.Xml.Serialization;

namespace PROWeb.Authentication.Models
{
    public class VerificationCodeEmail
    {
        [XmlElement("VerificationCode")]
        public string? Code { get; set; }

        [XmlElement("UserName")]
        public string? UserName { get; set; }

        [XmlElement("GeneralName")]
        public string? GeneralName { get; set; }
    }
}
