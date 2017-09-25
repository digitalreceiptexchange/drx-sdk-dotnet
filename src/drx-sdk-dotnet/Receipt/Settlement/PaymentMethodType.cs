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
        [EnumMember(Value = "Cash")]
        [DrxEnumExtendedInformation("Cash", "Cash Payment")]
        Cash,

        [EnumMember(Value = "CreditCard")]
        [DrxEnumExtendedInformation("CreditCard", "Credit Card Payment")]
        CreditCard,

        [EnumMember(Value = "DEBIT CARD")]
        [DrxEnumExtendedInformation("DebitCard", "Debit Card Payment")]
        DebitCard,

        [EnumMember(Value = "CHEQUE")]
        [DrxEnumExtendedInformation("CHEQUE", "Cheque payment")]
        Cheque,

        [EnumMember(Value = "OTHER")]
        [DrxEnumExtendedInformation("OTHER", "Other")]
        Other
    }
}