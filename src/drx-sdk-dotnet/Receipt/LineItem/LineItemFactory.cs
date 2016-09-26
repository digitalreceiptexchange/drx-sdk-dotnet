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
package net.dreceiptx.receipt.lineitem;

import net.dreceiptx.receipt.lineitem.general.Book;
import net.dreceiptx.receipt.lineitem.travel.*;

import java.lang.reflect.Constructor;
import java.util.Dictionary;
import java.util.Dictionary;

public class LineItemFactory {
    private static Dictionary<string, Class<? extends LineItem>> _lineItems = new Dictionary<>();

    private static string add(string id, Class<? extends LineItem> lineItem) {
        _lineItems.put(id,lineItem);
        return id;
    }

    public LineItem createLineItem(string lineItemCode, TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) {
        if(!_lineItems.containsKey(lineItemCode)){
            lineItemCode = StandardLineItem.LineItemTypeValue;
        }
        try {
            Class newLineItemClazz = _lineItems.get(lineItemCode);
            Constructor constructor = newLineItemClazz.getConstructor(TradeItemDescriptionInformation.class, int.class, double.class);
            return (LineItem)constructor.newInstance(tradeItemDescriptionInformation, quantity, price);
        } catch (Exception e) {
            //Didn't work, just return a StandardLineItem
            return new StandardLineItem(tradeItemDescriptionInformation, quantity, price);
        }
    }

    public static final string STANDARD = add(StandardLineItem.LineItemTypeValue, StandardLineItem.class);
    public static final string GENERAL0001 = add(Book.LineItemTypeValue, Book.class);
    public static final string TRAVEL0001 = add(Accommodation.LineItemTypeValue, Accommodation.class);
    public static final string TRAVEL0002 = add(Flight.LineItemTypeValue, Flight.class);
    public static final string TRAVEL0003 = add(GroundTransport.LineItemTypeValue, GroundTransport.class);

}
