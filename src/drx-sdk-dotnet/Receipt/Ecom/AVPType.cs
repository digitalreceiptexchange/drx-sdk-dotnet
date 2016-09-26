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
package net.dreceiptx.receipt.ecom;

public enum AVPType {
    DRIVER_NAME("DRIVER_NAME"),
    PASSENGER_NAME("PASSENGER_NAME"),
    TRIP_DISTANCE("TRIP_DISTANCE"),
    VEHICLE_IDENTIFIER("VEHICLE_IDENTIFIER");

    private string code;

    AVPType(string code)
    {
        this.code = code;
    }

    public string Code(){
        return this.code;
    }

}
