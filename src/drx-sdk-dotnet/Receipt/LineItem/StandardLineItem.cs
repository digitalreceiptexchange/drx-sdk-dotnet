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
using Net.Dreceiptx.Receipt.Serialization.Json;
using Newtonsoft.Json;

namespace Net.Dreceiptx.Receipt.LineItem
{
    [DataContract]
    public class StandardLineItem : LineItem
    {
        public static readonly string LineItemTypeValue = "STANDARD";

        public StandardLineItem(string brand, string name, string description, int quantity, decimal price) 
            : base(brand, name, description, quantity, price)
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, StandardLineItem.LineItemTypeValue);
        }

        public StandardLineItem(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, decimal price) 
            : base(tradeItemDescriptionInformation, quantity, price)
        {
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public StandardLineItem(TransactionalTradeItemType transactionalTradeItemType, string transactionalTradeItemCode,
            int quantity, decimal price) : base(transactionalTradeItemType, transactionalTradeItemCode, quantity, price)
        {
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }
    }
}