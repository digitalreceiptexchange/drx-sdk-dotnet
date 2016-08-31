using System.Runtime.Serialization;
using Net.Dreceiptx.GS1.SDBH;

namespace Net.Dreceiptx
{
    [DataContract]
    public class DrxDigitalReceipt : MessageBase
    {
        public DrxDigitalReceipt()
        {
            StandardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
        }

        [DataMember]
        public StandardBusinessDocumentHeader StandardBusinessDocumentHeader { get; set; }

        [DataMember]
        public DigitalReceipt Invoice { get; set; }
    }
}