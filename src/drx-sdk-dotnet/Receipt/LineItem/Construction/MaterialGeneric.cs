#region copyright
// Copyright 2016 Digital Receipt Exchange Limited
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion

using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.LineItem.Construction
{
    [DataContract]
    public class MaterialGeneric : LineItem
    {
        public static readonly string LineItemTypeValue = "CON0001";

        public MaterialGeneric(string brandName, string productName, string productDescription,
            int quantity, decimal price)
            : this(brandName, productName, productDescription, (double)quantity, price)
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, LineItemTypeValue);
        }

        public MaterialGeneric(string brandName, string productName, string productDescription,
            double quantity, decimal price) 
            : base(brandName, productName, productDescription, quantity, price)
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, LineItemTypeValue);
        }

        public MaterialGeneric(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity,
            decimal price) : this(tradeItemDescriptionInformation, (double)quantity, price)
        {
        }

        public MaterialGeneric(TradeItemDescriptionInformation tradeItemDescriptionInformation, double quantity,
            decimal price) : base(tradeItemDescriptionInformation, quantity, price)
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, LineItemTypeValue);
        }

    }
}
