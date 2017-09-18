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
namespace Net.Dreceiptx.Receipt.LineItem.General
{
    public class Book : LineItem
    {
        public static readonly string LineItemTypeValue = "GENERAL0001";
        private readonly string _tradeItemIdentificationISBN = "ISBN";

        
        public Book(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, decimal price) 
            : this(tradeItemDescriptionInformation, (double)quantity, price)
        {

        }

        public Book(TradeItemDescriptionInformation tradeItemDescriptionInformation, double quantity, decimal price)
            : base(tradeItemDescriptionInformation, quantity, price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, Book.LineItemTypeValue);
        }

        public Book(string ISBN, int quantity, decimal price) :base()
        {
            AddTradeItemIdentification(LineItem.LineItemTypeIdentifier, Book.LineItemTypeValue);
            AddTradeItemIdentification(_tradeItemIdentificationISBN, ISBN);
            Quantity = quantity;
            Price = price;
        }

        public string IBSNNumber => GetTradeItemIdentificationValue(_tradeItemIdentificationISBN);
    }
}