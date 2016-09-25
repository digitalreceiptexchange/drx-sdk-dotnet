using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class PartnerIdentification
    {
        /// <summary>
        /// Gets and sets the Authority agency of the identification key.
        /// The "Authority" attribute, although optional, MUST be used and 
        /// its value MUST be set to GS1.
        /// </summary>
        [DataMember]
        [XmlAttribute]
        public string Authority { get; set; }

        [DataMember]
        [XmlText]
        public string Value { get; set; }
    }
}