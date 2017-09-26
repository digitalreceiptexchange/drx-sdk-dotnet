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
    public enum AccommodationType
    {
        [DrxEnumExtendedInformation("ACC0000", "Standard")]
        [EnumMember(Value = "ACC0000")]
        Standard,

        [DrxEnumExtendedInformation("ACC0001", "Hotel")]
        [EnumMember(Value = "ACC0001")]
        Hotel,

        [DrxEnumExtendedInformation("ACC0002", "Hostel")]
        [EnumMember(Value = "ACC0002")]
        Hostel,

        [DrxEnumExtendedInformation("ACC0003", "Motel")]
        [EnumMember(Value = "ACC0003")]
        Motel,

        [DrxEnumExtendedInformation("ACC0004", "Bed and Breakfast")]
        [EnumMember(Value = "ACC0004")]
        BedAndBreakfast,

        [DrxEnumExtendedInformation("ACC0005", "Guesthouse")]
        [EnumMember(Value = "ACC0005")]
        Guesthouse,

        [DrxEnumExtendedInformation("ACC0006", "Apartment")]
        [EnumMember(Value = "ACC0006")]
        Apartment,

        [DrxEnumExtendedInformation("ACC0010", "Commercial Rental")]
        [EnumMember(Value = "ACC0010")]
        CommercialRental,

        [DrxEnumExtendedInformation("ACC0011", "Private Rental")]
        [EnumMember(Value = "ACC0011")]
        PrivateRental
    }
}