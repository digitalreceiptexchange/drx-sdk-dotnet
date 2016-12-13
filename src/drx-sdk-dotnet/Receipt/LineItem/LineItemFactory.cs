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

using System.Collections.Generic;
using Net.Dreceiptx.Receipt.LineItem.Construction;
using Net.Dreceiptx.Receipt.LineItem.General;
using Net.Dreceiptx.Receipt.LineItem.Travel;

namespace Net.Dreceiptx.Receipt.LineItem
{
    public class LineItemFactory
    {
        private static readonly Dictionary<string, LineItemFunc> LineItems = new Dictionary<string, LineItemFunc>();

        private delegate LineItem LineItemFunc(
            TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, decimal price);

        public LineItem CreateLineItem(string lineItemCode, TradeItemDescriptionInformation tradeItemDescriptionInformation, 
            int quantity, decimal price)
        {
            if (!LineItems.ContainsKey(lineItemCode))
            {
                lineItemCode = StandardLineItem.LineItemTypeValue;
            }
            return LineItems[lineItemCode](tradeItemDescriptionInformation, quantity, price);
        }

        public static readonly string STANDARD    = Add(StandardLineItem.LineItemTypeValue, (desc, qty, price) => new StandardLineItem(desc, qty, price));
        public static readonly string GENERAL0001 = Add(Book.LineItemTypeValue,             (desc, qty, price) => new Book(desc, qty, price));
        public static readonly string TRAVEL0001  = Add(Accommodation.LineItemTypeValue,    (desc, qty, price) => new Accommodation(desc, qty, price));
        public static readonly string TRAVEL0002  = Add(Flight.LineItemTypeValue,           (desc, qty, price) => new Flight(desc, qty, price));
        public static readonly string TRAVEL0003  = Add(GroundTransport.LineItemTypeValue,  (desc, qty, price) => new GroundTransport(desc, qty, price));
        public static readonly string CONSTRUCTION001 = Add(GenericProduct.LineItemTypeValue, (desc, qty, price) => new GenericProduct(desc, qty, price));


        private static string Add(string id, LineItemFunc lineItemFunc)
        {
            LineItems.Add(id, lineItemFunc);
            return id;
        }
    }
}