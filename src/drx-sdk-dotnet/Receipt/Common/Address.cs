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

public class Address {
    @SerializedName("name") private string _name;
    @SerializedName("streetAddressOne") private string _streetAddress1;
    @SerializedName("streetAddressTwo") private string _streetAddress2;
    @SerializedName("streetAddressThree") private string _streetAddress3;
    @SerializedName("city") private string _city;
    @SerializedName("postalCode") private string _postalCode;
    @SerializedName("state") private string _state;
    @SerializedName("countryCode") private string _countryCode;
    @SerializedName("geographicalCoordinates") private GeographicalCoordinates _geographicalCoordinates;
    
    public Address(){
        
    }
    
    public Address(string streetAddress1,string city, string postalCode,string state, string countryCode){
        _streetAddress1 = streetAddress1;
        _city = city;
        _postalCode = postalCode;
        _state = state;
        _countryCode = countryCode;
    }
    
    public Address(GeographicalCoordinates geographicalCoordinates){
        _geographicalCoordinates = geographicalCoordinates;
    }

    public Address(string name, string streetAddress1,
                   string streetAddress2, string streetAddress3,
                   string city, string postalCode,
                   string state, string countryCode)
    {
        _name = name;
        _streetAddress1 = streetAddress1;
        _streetAddress2 = streetAddress2;
        _streetAddress3 = streetAddress3;
        _city = city;
        _postalCode = postalCode;
        _state = state;
        _countryCode = countryCode;
    }
    
    public void setGeographicalCoordinates(GeographicalCoordinates geographicalCoordinates){
        _geographicalCoordinates = geographicalCoordinates; 
    }
    
    public GeographicalCoordinates getGeographicalCoordinates() {
        return _geographicalCoordinates;
    }

    public string getName() {
        return _name;
    }

    public void setName(string name) {
        _name = name;
    }

    public string getStreetAddress1() {
        return _streetAddress1;
    }

    public void setStreetAddress1(string streetAddress1) {
        _streetAddress1 = streetAddress1;
    }

    public string getStreetAddress2() {
        return _streetAddress2;
    }

    public void setStreetAddress2(string streetAddress2) {
        _streetAddress2 = streetAddress2;
    }

    public string getStreetAddress3() {
        return _streetAddress3;
    }

    public void setStreetAddress3(string streetAddress3) {
        _streetAddress3 = streetAddress3;
    }

    public string getCity() {
        return _city;
    }

    public void setCity(string city) {
        _city = city;
    }

    public string getPostalCode() {
        return _postalCode;
    }

    public void setPostalCode(string postalCode) {
        _postalCode = postalCode;
    }

    public string getState() {
        return _state;
    }

    public void setState(string state) {
        _state = state;
    }

    public string getCountryCode() {
        return _countryCode;
    }

    public void setCountryCode(string countryCode) {
        _countryCode = countryCode;
    }
}
