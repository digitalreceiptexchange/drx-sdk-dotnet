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

namespace Net.Dreceiptx.Receipt.Common.Measurements
{
    public enum MeasurementType
    {
        [EnumMember(Value = "MMT")]
        [DrxEnumExtendedInformation("MMT", "mm")]
        MILLIMETRE,
        [EnumMember(Value = "CMT")]
        [DrxEnumExtendedInformation("CMT", "cm")]
        CENTIMETRE,
        [EnumMember(Value = "FOT")]
        [DrxEnumExtendedInformation("FOT", "ft")]
        FOOT,
        [EnumMember(Value = "YRD")]
        [DrxEnumExtendedInformation("YRD", "yd")]
        YARD,
        [EnumMember(Value = "MTQ")]
        [DrxEnumExtendedInformation("MTQ", "m3")]
        CUBIC_METRE,
        [EnumMember(Value = "LTR")]
        [DrxEnumExtendedInformation("LTR", "l")]
        LITRE,
        [EnumMember(Value = "GLI")]
        [DrxEnumExtendedInformation("GLI", "gal (UK)")]
        GALLOON_UK,
        [EnumMember(Value = "GLL")]
        [DrxEnumExtendedInformation("GLL", "gal (US)")]
        GALLOON_US,
        [EnumMember(Value = "TNE")]
        [DrxEnumExtendedInformation("TNE", "t")]
        TON_METRIC,
        [EnumMember(Value = "KGM")]
        [DrxEnumExtendedInformation("KGM", "kg")]
        KILOGRAM,
        [EnumMember(Value = "MGM")]
        [DrxEnumExtendedInformation("MGM", "mg")]
        MILLIGRAM,
        [EnumMember(Value = "LBR")]
        [DrxEnumExtendedInformation("LBR", "lb")]
        POUND
    }
}
