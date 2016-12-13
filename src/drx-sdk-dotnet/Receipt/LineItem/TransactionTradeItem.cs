using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.Common.Measurements;

namespace Net.Dreceiptx.Receipt.LineItem
{
    [DataContract]
    public class TransactionalTradeItem
    {
        [DataMember]
        public TradeItemDescriptionInformation TradeItemDescriptionInformation { get; set; } = null;

        [DataMember]
        public TransactionalItemData TransactionItemData { get; set; }

        [DataMember(Name = "AdditionalTradeItemIdentification")]
        public TradeItemIdentification ItemIdentification { get; set; } = new TradeItemIdentification();

        [DataMember]
        public TradeItemMeasurements TradeItemMeasurements { get; set; }
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
