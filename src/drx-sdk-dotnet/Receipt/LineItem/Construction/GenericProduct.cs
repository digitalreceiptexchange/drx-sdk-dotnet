using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Dreceiptx.Receipt.LineItem.Construction
{
    [DataContract]
    public class GenericProduct : LineItem
    {
        public static readonly string LineItemTypeValue = "CON0001";

        public GenericProduct(string brandName, string productName, string productDescription,
            int quantity, decimal price) 
            : base(brandName, productName, productDescription, quantity, price)
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, LineItemTypeValue);
        }

        public GenericProduct(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity,
            decimal price) : base(tradeItemDescriptionInformation, quantity, price)
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, LineItemTypeValue);
        }
    }
}
