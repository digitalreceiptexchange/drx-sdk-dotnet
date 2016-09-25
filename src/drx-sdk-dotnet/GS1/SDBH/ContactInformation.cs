using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    /// <summary>
    /// Name of the contact person or department for the sending Party.
    /// The element ontactInformation, although optional, SHOULD be used, if possible.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class ContactInformation
    {
        
        [DataMember]
        public string RepId { get; set; }
        
        [DataMember]
        public string RepName { get; set; }

        /// <summary>
        /// Gets and sets the Name of contact person or department.
        /// The element „Contact‟, although optional, SHOULD be used, if possible.
        /// Example: Delysha Burnet
        /// </summary>
        [DataMember]
        public string Contact { get; set; }

        /// <summary>
        /// Gets and sets the EmailAddress. The EmailAddress, although optional, SHOULD be used, if possible.
        /// </summary>
        [DataMember]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets and sets the FaxNumber. A number format agreed upon between the Sender
        ///  and Receiver SHOULD be used. Number format expressed using [RFC3966]. 
        /// The tel URI for Telephone Numbers MAY be used. 
        /// </summary>
        [DataMember]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets and sets the TelephoneNumber. A number format agreed upon between the Sender
        /// and Receiver SHOULD be used. Number format expressed using [RFC3966].
        /// The tel URI for Telephone Numbers‟ MAY be used. 
        /// </summary>
        [DataMember]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Gets and sets the ContactTypeIdentifier. Role of the identifier.
        /// Example: EDI co-ordinator
        /// </summary>
        [DataMember]
        public string ContactTypeIdentifier { get; set; }

    }
}