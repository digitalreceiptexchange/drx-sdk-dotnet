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

package net.dreceiptx.receipt.common;

import com.google.gson.annotations.SerializedName;

public class Address {
    @SerializedName("name") private String _name;
    @SerializedName("streetAddressOne") private String _streetAddress1;
    @SerializedName("streetAddressTwo") private String _streetAddress2;
    @SerializedName("streetAddressThree") private String _streetAddress3;
    @SerializedName("city") private String _city;
    @SerializedName("postalCode") private String _postalCode;
    @SerializedName("state") private String _state;
    @SerializedName("countryCode") private String _countryCode;
    @SerializedName("geographicalCoordinates") private GeographicalCoordinates _geographicalCoordinates;
    
    public Address(){
        
    }
    
    public Address(String streetAddress1,String city, String postalCode,String state, String countryCode){
        _streetAddress1 = streetAddress1;
        _city = city;
        _postalCode = postalCode;
        _state = state;
        _countryCode = countryCode;
    }
    
    public Address(GeographicalCoordinates geographicalCoordinates){
        _geographicalCoordinates = geographicalCoordinates;
    }

    public Address(String name, String streetAddress1,
                   String streetAddress2, String streetAddress3,
                   String city, String postalCode,
                   String state, String countryCode)
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

    public String getName() {
        return _name;
    }

    public void setName(String name) {
        _name = name;
    }

    public String getStreetAddress1() {
        return _streetAddress1;
    }

    public void setStreetAddress1(String streetAddress1) {
        _streetAddress1 = streetAddress1;
    }

    public String getStreetAddress2() {
        return _streetAddress2;
    }

    public void setStreetAddress2(String streetAddress2) {
        _streetAddress2 = streetAddress2;
    }

    public String getStreetAddress3() {
        return _streetAddress3;
    }

    public void setStreetAddress3(String streetAddress3) {
        _streetAddress3 = streetAddress3;
    }

    public String getCity() {
        return _city;
    }

    public void setCity(String city) {
        _city = city;
    }

    public String getPostalCode() {
        return _postalCode;
    }

    public void setPostalCode(String postalCode) {
        _postalCode = postalCode;
    }

    public String getState() {
        return _state;
    }

    public void setState(String state) {
        _state = state;
    }

    public String getCountryCode() {
        return _countryCode;
    }

    public void setCountryCode(String countryCode) {
        _countryCode = countryCode;
    }
}
