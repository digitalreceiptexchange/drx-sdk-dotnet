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

using System.Runtime.Serialization;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.Ecom
{
    public enum AVPType
    {
        [DrxEnumExtendedInformation("DRIVER_NAME", null)]
        [EnumMember(Value = "DRIVER_NAME")]
        DRIVER_NAME,

        [DrxEnumExtendedInformation("PASSENGER_NAME", null)]
        [EnumMember(Value = "PASSENGER_NAME")]
        PASSENGER_NAME,

        [DrxEnumExtendedInformation("PNR", null)]
        [EnumMember(Value = "PNR")]
        PASSENGER_NAME_RECORD,

        [DrxEnumExtendedInformation("TRIP_DISTANCE", null)]
        [EnumMember(Value = "TRIP_DISTANCE")]
        TRIP_DISTANCE,

        [DrxEnumExtendedInformation("VEHICLE_IDENTIFIER", null)]
        [EnumMember(Value = "VEHICLE_IDENTIFIER")]
        VEHICLE_IDENTIFIER,

        [DrxEnumExtendedInformation("FLIGHT_DESTINATION_TYPE", null)]
        [EnumMember(Value = "FLIGHT_DESTINATION_TYPE")]
        FLIGHT_DESTINATION_TYPE

    }
}