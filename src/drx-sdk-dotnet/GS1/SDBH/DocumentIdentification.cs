using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    /// <summary>
    /// Identification information for the document
    /// </summary>
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class DocumentIdentification
    {
        /// <summary>
        /// Creates instance of DocumentIdentification with default values set.
        /// </summary>
        public DocumentIdentification()
        {
            // Current design of GS1 XML for eCom does not allow for inclusion more 
            // than one business document 225 type in the GS1 message, thus 
            // MultipleType should always be set to false.
            MultipleType = "false";
        }

        /// <summary>
        /// Gets and sets the Standard. The name of the document standard contained in the payload
        /// The value of the element tandard MUST be set to the value 'GS1'
        /// </summary>
        [DataMember]
        public string Standard { get; set; }
        
        /// <summary>
        /// Gets and sets the TypeVersion. Version information of the document included in the 
        /// payload of SBDH. This is the 'complete' version of the document itself and is 
        /// different than the HeaderVersion. The value of TypeVersion MUST be set the 
        /// version number of the root schema of the XML business document contained 
        /// in the payload of the message. Every GS1 standard schema has version 
        /// information in the „xsd:version‟ attribute of the „xsd:schema‟ tag of the schema 
        /// and also in the schema annotation tag.
        /// 
        /// The SBDH specification requires that all documents sent with one header have the 
        /// same version number. To comply with this requirement; Only business documents 
        /// belonging to the same BMS publication release and having the same version number 
        /// MUST be included in the payload if sending more than one document type.
        /// </summary>
        [DataMember]
        public string TypeVersion { get; set; }
        
        /// <summary>
        /// Gets and sets the Type. This element identifies the type of the document
        /// The value of the „Type‟ element of „DocumentIdentification‟ element MUST be set 
        /// to the name of the XML element that defines the root of the business document. 
        /// This is the name of the global XML element declared in the root schema for the 
        /// business document in consideration. 
        /// Example; order, invoice, debitCreditAdvice,
        /// </summary>
        [DataMember]
        public string Type { get; set; }
        
        /// <summary>
        /// Gets and sets the InstanceIdentifier. Description which contains reference information 
        /// which uniquely identifies this instance of the Standard Business Document (SBD) 
        /// between the Sender and the Receiver. This identifier identifies this document as being 
        /// distinct from others.
        /// Example: MSG-1645000099
        /// </summary>
        [DataMember]
        public string InstanceIdentifier { get; set; }
        
        /// <summary>
        /// Gets and sets the MultipleType property. Flag to indicate that there is more than one 
        /// type of business document in the payload of the SBDH.
        /// The value of the MultiType element of DocumentIdentification element MUST be set 
        /// false as the GS1 XML design allows only one type of business documents to be sent 
        /// within one message.
        /// TODO: Raised #6 on this as i think it should be a boolean
        /// </summary>
        [DataMember]
        public string MultipleType { get; set; }
        
        /// <summary>
        /// Gets and sets the CreationDateAndTime. Date and time of the SBDH document creation.
        /// The value of the „CreationDateAndTime‟ element MUST be set to the date and time when 
        /// the document originating application or the parser created the document. 
        /// This value will typically be populated by the trading partner and will typically 
        /// differ from the time stamping of the message by the communications software.
        /// </summary>
        [DataMember]
        public DateTime CreationDateAndTime { get; set; }
    }

}
