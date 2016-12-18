using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Dreceiptx.Receipt.Invoice
{
    [DataContract]
    public class SellerInformation
    {
        [DataMember]
        public Organisation OrganisationDetails { get; set; }
        [DataMember]
        public TaxRegistration DutyFeeTaxRegistration { get; set; }
    }

    public class TaxRegistration
    {
        [DataMember]
        public string DutyFeeTaxTypeCode { get; set; }

        [DataMember]
        public string DutyFeeTaxRegistationID { get; set; }
    }

    public class Organisation
    {
        [DataMember]
        public string OrganisationName { get; set; }
    }
}
