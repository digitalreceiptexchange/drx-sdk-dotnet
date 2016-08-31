using System.Runtime.Serialization;

namespace Net.Dreceiptx
{
    public class InvoiceIdentification
    {
        [DataMember]
        public string EntityIdentification { get; set; }
    }
}