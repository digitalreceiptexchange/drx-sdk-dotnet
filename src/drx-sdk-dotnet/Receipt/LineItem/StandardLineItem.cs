/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package net.dreceiptx.receipt.lineitem;

public class StandardLineItem extends LineItem {

    public static final String LineItemTypeValue = "STANDARD";

    public StandardLineItem(String brand, String name, String description, int quantity, double price) {
        super(brand, name, description, quantity, price);
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, StandardLineItem.LineItemTypeValue);
    }

    public StandardLineItem(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) {
        super(tradeItemDescriptionInformation, quantity, price);
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, StandardLineItem.LineItemTypeValue);
    }

    public StandardLineItem(TransactionalTradeItemType transactionalTradeItemType, String transactionalTradeItemCode, int quantity, double price) {
        super(transactionalTradeItemType, transactionalTradeItemCode, quantity, price);
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, StandardLineItem.LineItemTypeValue);
    }
}