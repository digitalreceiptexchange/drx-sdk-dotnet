using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Dreceiptx.Receipt.LineItem
{
    [DataContract]
    public class TransactionalTradeItem
    {
        [DataMember]
        public TradeItemDescriptionInformation TradeItemDescriptionInformation { get; set; } = null;

        [DataMember]
        public TransactionalItemData TransactionItemData { get; set; }
    }

    [DataContract]
    public class TransactionalItemData
    {
        [DataMember]
        public string SerialNumber { get; set; }
        [DataMember]
        public string BatchNumber { get; set; }
    }
}
