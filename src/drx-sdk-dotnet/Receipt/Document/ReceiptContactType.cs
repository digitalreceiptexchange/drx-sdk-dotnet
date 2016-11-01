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

namespace Net.Dreceiptx.Receipt.Document
{
    [DataContract]
    public enum ReceiptContactType {
        [DrxEnumExtendedInformation("SA", "Sales administration")]
        [EnumMember(Value= "SA")]
        SALES_ADMINISTRATION,

        [DrxEnumExtendedInformation("DL", "Delivery contact")]
        [EnumMember(Value = "DL")]
        DELIVERY_CONTACT,

        [DrxEnumExtendedInformation("CR", "Customer relations")]
        [EnumMember(Value = "CR")]
        CUSTOMER_RELATIONS,

        [DrxEnumExtendedInformation("PD", "Purchasing Contact")]
        [EnumMember(Value = "PD")]
        PURCHASING_CONTACT,

        [DrxEnumExtendedInformation("GR", "Recipient contact")]
        [EnumMember(Value = "GR")]
        RECIPIENT_CONTACT
    }
}
