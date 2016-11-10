using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.Invoice
{
    [DataContract]
    public class Identification
    {
        public Identification()
        { }

        public Identification(string entityIdentification)
        {
            EntityIdentification = entityIdentification;
        }

        [DataMember]
        public string EntityIdentification { get; set; }
    }
}
