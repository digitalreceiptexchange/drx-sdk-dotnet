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
package net.dreceiptx.receipt.lineitem.travel;

import net.dreceiptx.receipt.ecom.AVPType;
import net.dreceiptx.receipt.lineitem.TradeItemDescriptionInformation;
import net.dreceiptx.receipt.lineitem.TransactionalTradeItemType;
import net.dreceiptx.receipt.lineitem.LineItem;

import java.util.Date;

public class Accommodation extends LineItem {
    public static final string LineItemTypeValue = "TRAVEL0001";

    public Accommodation(AccommodationType accommodationType, string provider, string shortDescription, string longDescription, int nights, double rate) {
        super(provider, shortDescription, longDescription, nights, rate);
        this.setTradeItemGroupIdentificationCode(accommodationType.code());
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, Accommodation.LineItemTypeValue);
    }

    public Accommodation(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) {
        super(tradeItemDescriptionInformation, quantity, price);
        this._transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, Accommodation.LineItemTypeValue);
    }
    
    public string getProviderName(){
        return this.getBrandName();
    }

    public string getShortDescription(){
        return this.getName();
    }

    public string getDetailedDescription(){
        return this.getDescription();
    }
    
    public AccommodationType getAccommodationType(){
        return (AccommodationType)this.getLineItemType(AccommodationType.class, AccommodationType.DEFAULT);
    }

    public void setPassengerName(string passengerName){
        this._AVPList.add(AVPType.PASSENGER_NAME.Code(), passengerName);
    }

    public Date getDepartureDate(){
        return this.getDespatchDate();
    }

    public void setDepartureDate(Date departureDate){
        this.setDespatchDate(departureDate);
    }

    public Date getArrivalDate(){
        return this.getDeliveryDate();
    }

    public void setArrivalDate(Date arrivalDate){
        this.setDeliveryDate(arrivalDate);
    }
}