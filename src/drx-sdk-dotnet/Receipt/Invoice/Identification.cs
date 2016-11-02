using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
