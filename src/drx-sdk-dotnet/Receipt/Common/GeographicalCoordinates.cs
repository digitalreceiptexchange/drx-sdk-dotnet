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

public class GeographicalCoordinates {
    @SerializedName("latitude") private String _latitude;
    @SerializedName("longitude") private String _longitude;
    
    public GeographicalCoordinates(String latitude, String longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public String getLatitude() {
        return _latitude;
    }
    
    public String getLongitude() {
        return _longitude;
    }
}
