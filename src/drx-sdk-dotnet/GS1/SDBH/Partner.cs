using System.Runtime.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [DataContract]
    public partial class Partner
    {
        public Partner()
        {
            Identifier = new PartnerIdentification();
        }
        
        [DataMember]
        public PartnerIdentification Identifier { get; set; }
        
    }
}