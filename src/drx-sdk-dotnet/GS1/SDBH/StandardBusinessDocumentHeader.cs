using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    /// <summary>
    /// The UN/CEFACT standard, containing information about the routing and processing of the business 
    /// document. It also identifies the message set that is sent together with on SBDH and the 
    /// version number of the document(s) contained.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class StandardBusinessDocumentHeader
    {
        public StandardBusinessDocumentHeader()
        {
            Sender = new List<Partner>();
            Receiver = new List<Partner>();
            DocumentIdentification = new DocumentIdentification();
        }

        //TODO: Check this. Why is it a collection?
        /// <summary>
        /// Gets and sets the sender of the message, party representing the 
        /// organization which created the standard business document
        /// </summary>
        [DataMember]
        public List<Partner> Sender { get; set; }
        
        //TODO: Why is this a collection?
        [DataMember]
        [XmlElement("Receiver")]
        public List<Partner> Receiver { get; set; }
        
        [DataMember]
        public DocumentIdentification DocumentIdentification { get; set; }
    }
}