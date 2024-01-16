using System.Xml.Serialization;

namespace PROWeb.Authentication.Models
{
    public class UserEmailConformation
    {
        [XmlElement("ConfirmEmailLink")]
        public string? Link { get; set; }

        [XmlElement("UserName")]
        public string? UserName { get; set; }

        [XmlElement("GeneralName")]
        public string? GeneralName { get; set; }
    }
}
