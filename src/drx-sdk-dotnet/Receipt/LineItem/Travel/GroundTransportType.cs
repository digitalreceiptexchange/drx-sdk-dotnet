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

namespace Net.Dreceiptx.Receipt.LineItem.Travel
{
    public enum GroundTransportType
    {
        [DrxEnumExtendedInformation("GTP0000", "Standard")]
        [EnumMember(Value = "GTP0000")]
        Standard,

        [DrxEnumExtendedInformation("GTP0001", "Taxi")]
        [EnumMember(Value = "GTP0001")]
        Taxi,

        [DrxEnumExtendedInformation("GTP0002", "WaterTaxi")]
        [EnumMember(Value = "GTP0002")]
        WaterTaxi,

        [DrxEnumExtendedInformation("GTP0010", "Ride Sharing")]
        [EnumMember(Value = "GTP0010")]
        RideSharing,

        [DrxEnumExtendedInformation("GTP0011", "Car Pooling")]
        [EnumMember(Value = "GTP0011")]
        CarPooling,

        [DrxEnumExtendedInformation("GTP0012", "Car Rental")]
        [EnumMember(Value = "GTP0012")]
        CarRental,

        [DrxEnumExtendedInformation("GTP0013", "Private Car Rental")]
        [EnumMember(Value = "GTP0013")]
        PrivateCarRental,

        [DrxEnumExtendedInformation("GTP0020", "Train")]
        [EnumMember(Value = "GTP0020")]
        Train,

        [DrxEnumExtendedInformation("GTP0021", "Local and metropolitan train")]
        [EnumMember(Value = "GTP0021")]
        MetroTrain,

        [DrxEnumExtendedInformation("GTP0022", "National and regional train")]
        [EnumMember(Value = "GTP0022")]
        NationalTrain,

        [DrxEnumExtendedInformation("GTP0023", "International and intercontinental train")]
        [EnumMember(Value = "GTP0023")]
        InternationalTrain,

        [DrxEnumExtendedInformation("GTP0024", "Light rail, light rail transit or tram")]
        [EnumMember(Value = "GTP0024")]
        TramOrLightRail,

        [DrxEnumExtendedInformation("GTP0030", "Bus")]
        [EnumMember(Value = "GTP0030")]
        Bus,

        [DrxEnumExtendedInformation("GTP0031", "Shuttle Bus")]
        [EnumMember(Value = "GTP0031")]
        ShuttleBus,

        [DrxEnumExtendedInformation("GTP0032", "Local and metropolitan bus")]
        [EnumMember(Value = "GTP0032")]
        MetroBus,

        [DrxEnumExtendedInformation("GTP0033", "National and regional bus")]
        [EnumMember(Value = "GTP0033")]
        NationalBus,

        [DrxEnumExtendedInformation("GTP0034", "International and intercontinental bus")]
        [EnumMember(Value = "GTP0034")]
        InternationalBus,

        [DrxEnumExtendedInformation("GTP0040", "Ferry")]
        [EnumMember(Value = "GTP0040")]
        Ferry
    }
}
