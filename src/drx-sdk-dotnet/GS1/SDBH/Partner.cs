using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class Partner
    {
        public Partner()
        {
            Identifier = new PartnerIdentification();
        }
        
        [DataMember]
        public PartnerIdentification Identifier { get; set; }
        
    }
}