using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.Invoice;

namespace Net.Dreceiptx.Receipt.Common
{
    [DataContract]
    public class TransactionalParty
    {
        [DataMember]
        public Organisation OrganisationDetails { get; set; }
        [DataMember]
        public DutyFeeTaxRegistration DutyFeeTaxRegistration { get; set; }
    }
}