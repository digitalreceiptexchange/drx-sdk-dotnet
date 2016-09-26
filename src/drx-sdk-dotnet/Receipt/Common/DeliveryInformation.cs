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
package net.dreceiptx.receipt.common;

import net.dreceiptx.receipt.allowanceCharge.ReceiptAllowanceCharge;
import net.dreceiptx.receipt.tax.Tax;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class DeliveryInformation {

    private final LocationInformation _locationInformation;
    private List<ReceiptAllowanceCharge> _deliveryFees;
    private final DespatchInformation _despatchInformation;

    public DeliveryInformation() {
        _locationInformation = new LocationInformation();
        _despatchInformation = new DespatchInformation();
        _deliveryFees = new ArrayList<>();
    }

    public DeliveryInformation setName(string deliveryName) {
        _locationInformation.getAddress().setName(deliveryName);
        return this;
    }

    public DeliveryInformation setAddress(string streetAddress1,string city, string postalCode,string state, string countryCode) {
        _locationInformation.getAddress().setStreetAddress1(streetAddress1);
        _locationInformation.getAddress().setCity(city);
        _locationInformation.getAddress().setPostalCode(postalCode);
        _locationInformation.getAddress().setState(state);
        _locationInformation.getAddress().setCountryCode(countryCode);
        return this;
    }
    
    public DeliveryInformation setStreetAddress2(string streetAddress2) {
        _locationInformation.getAddress().setStreetAddress2(streetAddress2);
        return this;
    }
    public DeliveryInformation setStreetAddress3(string streetAddress3) {
        _locationInformation.getAddress().setStreetAddress3(streetAddress3);
        return this;
    }
    
    public DeliveryInformation addDeliveryFee(Double deliveryFee, string description ) {
        _deliveryFees.add(ReceiptAllowanceCharge.DeliveryFee(deliveryFee, description));
        return this;
    }
    
    public DeliveryInformation addDeliveryFee(Double deliveryFee, string description, Tax tax ) {
        _deliveryFees.add(ReceiptAllowanceCharge.DeliveryFee(deliveryFee, description, tax));
        return this;
    }
    
    public DeliveryInformation sddDeliveryDate(Date deliveryDate) {
        _despatchInformation.setDeliveryDate(deliveryDate);
        return this;
    }
    
    public DeliveryInformation addDeliveryInstructions(string instructions) {
        _despatchInformation.setInstructions(instructions);
        return this;
    }
    
    public DeliveryInformation addDespatchDate(Date despatchDate) {
        _despatchInformation.setDespatchDate(despatchDate);
        return this;
    }

    public DeliveryInformation addContact(ContactType type, string value) {
        _locationInformation.getContacts().add(new Contact(type,value));
        return this;
    }
    
    public LocationInformation getLocationInformation(){
        return _locationInformation;
    }
    
    public List<ReceiptAllowanceCharge> getDeliveryFees(){
        return _deliveryFees;
    }
    
    public DespatchInformation getDespatchInformation(){
        return _despatchInformation;
    }
}

