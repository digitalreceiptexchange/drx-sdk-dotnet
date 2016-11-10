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

namespace Net.Dreceiptx.Receipt.AllowanceCharge
{
    public enum SettlementType {
        [EnumMember(Value = "ADZ")]
        [DrxEnumExtendedInformation("ADZ", "Delivery fee")]
        DeliveryFee,

        [EnumMember(Value = "FC")]
        [DrxEnumExtendedInformation("FC", "Freight fee")]
        FreightFee,

        [EnumMember(Value = "TIP")]
        [DrxEnumExtendedInformation("TIP", "Tip or Gratuity fee")]
        TIP,

        [EnumMember(Value = "PC")]
        [DrxEnumExtendedInformation("PC", "Packaging fee")]
        PackagingFee,

        [EnumMember(Value = "DI")]
        [DrxEnumExtendedInformation("DI", "General discount")]
        GeneralDiscount,

        [EnumMember(Value = "MB")]
        [DrxEnumExtendedInformation("MB", "Multi-buy discount")]
        MultiBuyDiscount,

        [EnumMember(Value = "FI")]
        [DrxEnumExtendedInformation("FI", "Admin or processing fee")]
        ProcessingFee,

        [EnumMember(Value = "BOK")]
        [DrxEnumExtendedInformation("BOK", "Booking fee")]
        BookingFee
    }

}