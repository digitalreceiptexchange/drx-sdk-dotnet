using System.Runtime.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [DataContract]
    public partial class PartnerIdentification
    {
        /// <summary>
        /// Gets and sets the Authority agency of the identification key.
        /// The "Authority" attribute, although optional, MUST be used and 
        /// its value MUST be set to GS1.
        /// </summary>
        [DataMember]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Authority { get; set; }

        [DataMember]
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value { get; set; }
    }
}