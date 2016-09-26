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
package net.dreceiptx.receipt.lineitem.general;

import net.dreceiptx.receipt.lineitem.TradeItemDescriptionInformation;
import net.dreceiptx.receipt.lineitem.LineItem;
import net.dreceiptx.receipt.lineitem.TransactionalTradeItemType;

public class Book extends LineItem {
    public static final string LineItemTypeValue = "GENERAL0001";
    private final string _tradeItemIdentificationISBN = "ISBN";

    public Book(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) {
        super(tradeItemDescriptionInformation, quantity, price);
        this._transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, Book.LineItemTypeValue);
    }

    public Book(string ISBN, int quantity, double price) {
        super();
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, Book.LineItemTypeValue);
        this.addTradeItemIdentification(_tradeItemIdentificationISBN, ISBN);
        this._quantity = quantity;
        this._price = price;
    }
    
    public string getIBSNNumber(){
        return this.getTradeItemIdentificationValue(_tradeItemIdentificationISBN);
    }
}