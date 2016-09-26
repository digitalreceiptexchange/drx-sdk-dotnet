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

package net.dreceiptx.receipt.lineitem.travel;

import net.dreceiptx.receipt.common.GeographicalCoordinates;
import net.dreceiptx.receipt.ecom.AVPType;
import net.dreceiptx.receipt.lineitem.TradeItemDescriptionInformation;
import net.dreceiptx.receipt.lineitem.TransactionalTradeItemType;
import net.dreceiptx.receipt.lineitem.LineItem;

import java.util.Date;

public class GroundTransport extends LineItem {
    public static final String LineItemTypeValue = "TRAVEL0003";

    public GroundTransport(GroundTransportType groundTransportType, String provider, String shortDescription, String longDescription, double price) {
        this(groundTransportType, provider, shortDescription, longDescription, 1, price);
    }
    
    public GroundTransport(GroundTransportType groundTransportType, String provider, String shortDescription, String longDescription, int quantity, double price) {
        super(provider, shortDescription, longDescription , quantity, price);
        this.setTradeItemGroupIdentificationCode(groundTransportType.code());
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, GroundTransport.LineItemTypeValue);
    }

    public GroundTransport(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) {
        super(tradeItemDescriptionInformation, quantity, price);
        this._transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
        this.addTradeItemIdentification(LineItem.LineItemTypeIdentifier, GroundTransport.LineItemTypeValue);
    }
    
    public String getProvider(){
        return this.getBrandName();
    }
    
    public GroundTransportType getGroundTransportType(){
        return (GroundTransportType)this.getLineItemType(GroundTransportType.class, GroundTransportType.DEFAULT);
    }
    
    public String getTripDescription(){
        return this.getDescription();
    }
    
    public void setPassengerName(String passengerName){
        this._AVPList.add(AVPType.PASSENGER_NAME.Code(), passengerName);
    }
    
    public String getPassengerName(){
        return this._AVPList.get(AVPType.PASSENGER_NAME.Code()).getValue();
    }

    public void setDriveName(String driverName){
        this._AVPList.add(AVPType.DRIVER_NAME.Code(), driverName);
    }

    public String getDriverName(){
        return this._AVPList.get(AVPType.DRIVER_NAME.Code()).getValue();
    }

    public void setVehicleIdentifier(String vehicleIdentifier){
        this._AVPList.add(AVPType.VEHICLE_IDENTIFIER.Code(), vehicleIdentifier);
    }

    public String getVehicleIdentifier(){
        return this._AVPList.get(AVPType.VEHICLE_IDENTIFIER.Code()).getValue();
    }

    public void setTripDistance(Double tripDistance){
        this._AVPList.add(AVPType.TRIP_DISTANCE.Code(), tripDistance.toString());
    }

    public Double getTripDistance(){
        if(this._AVPList.has(AVPType.TRIP_DISTANCE.Code())){
            return Double.parseDouble(this._AVPList.get(AVPType.TRIP_DISTANCE.Code()).getValue());
        }

        return null;
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
    
    public void setBookingNumber(String bookingNumber){
        this._serialNumber = bookingNumber;
    }
    
    public String getBookingNumber(){
        return this._serialNumber;
    }

    public void setDepartureGeoLocation(GeographicalCoordinates geographicalCoordinates) {
        _origin.setGeographicalCoordinates(geographicalCoordinates);
    }

    public GeographicalCoordinates getDepartureGeoLocation(){
        return _origin.getGeographicalCoordinates();
    }

    public void setArrivalGeoLocation(GeographicalCoordinates geographicalCoordinates) {
        _destination.setGeographicalCoordinates(geographicalCoordinates);
    }

    public GeographicalCoordinates getArrivalGeoLocation(){
        return _destination.getGeographicalCoordinates();
    }

    public void setDepartureDetails(Date departureDate, GeographicalCoordinates geographicalCoordinates){
        this.setDespatchDate(departureDate);
        _origin.setGeographicalCoordinates(geographicalCoordinates);
    }

    public void setArrivalDetails(Date arrivalDate, GeographicalCoordinates geographicalCoordinates){
        this.setArrivalDate(arrivalDate);
        _destination.setGeographicalCoordinates(geographicalCoordinates);
    }
}