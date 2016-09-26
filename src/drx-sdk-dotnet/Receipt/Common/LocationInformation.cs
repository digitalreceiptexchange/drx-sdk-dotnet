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

import com.google.gson.annotations.SerializedName;
import java.util.ArrayList;
import java.util.List;

public class LocationInformation {
    @SerializedName("address") private Address _address = null;
    @SerializedName("contact") private List<Contact> _contacts = null;

    public LocationInformation(){
    }
    
    public LocationInformation(Address address, Contact contact){
        _address = address;
    }
    
    public LocationInformation(Address address){
        _address = address;
    }

    public LocationInformation(GeographicalCoordinates geographicalCoordinates){
        this.setGeographicalCoordinates(geographicalCoordinates);
    }
    
    public LocationInformation(Contact contact){
        _contacts.add(contact);
    }
    
    public void addContact(Contact contact){
        if(_contacts == null){
            _contacts = new ArrayList<>();
        }
        _contacts.add(contact);
    }

    public void setAddress(Address address) {
        _address = address;
    }

    public void setGeographicalCoordinates(GeographicalCoordinates geographicalCoordinates) {
        if(_address != null){
            _address.setGeographicalCoordinates(geographicalCoordinates);
        }else{
            _address = new Address(geographicalCoordinates);
        }
    }

    public Address getAddress() {
        return _address;
    }

    public GeographicalCoordinates getGeographicalCoordinates() {
        if(_address != null){
            return _address.getGeographicalCoordinates();
        }

        return null;
    }
    
    public List<Contact> getContacts() {
        return _contacts;
    }
    
    public bool gsonValidator(){
        return _address != null || (_contacts != null && !_contacts.isEmpty());

    }
}
