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

namespace Net.Dreceiptx.Receipt.Settlement
{
    public enum PaymentMethodType
    {
        [EnumMember(Value = "CASH")]
        [DrxEnumExtendedInformation("CASH", "Cash Payment")]
        CASH,

        [EnumMember(Value = "CREDIT_CARD")]
        [DrxEnumExtendedInformation("CREDIT_CARD", "Credit Card Payment")]
        CREDIT_CARD,

        [EnumMember(Value = "DEBIT CARD")]
        [DrxEnumExtendedInformation("DEBIT_CARD", "Debit Card Payment")]
        DEBIT_CARD,

        [EnumMember(Value = "CHEQUE")]
        [DrxEnumExtendedInformation("CHEQUE", "Cheque payment")]
        CHEQUE,

        [EnumMember(Value = "OTHER")]
        [DrxEnumExtendedInformation("OTHER", "Other")]
        OTHER
    }
}