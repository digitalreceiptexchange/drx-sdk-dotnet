using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt.Common
{
    public class DutyFeeTaxRegistration
    {
        [DataMember]
        public TaxCode DutyFeeTaxTypeCode { get; set; }

        [DataMember]
        public string DutyFeeTaxRegistationID { get; set; }
    }
}