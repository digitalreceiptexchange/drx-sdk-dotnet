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

using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.AllowanceCharge
{ 
    public enum AllowanceChargeType
    {
        //@SerializedName("CREDIT_CUSTOMER_ACCOUNT")
        [DrxEnumExtendedInformation("CREDIT_CUSTOMER_ACCOUNT", "CREDIT CUSTOMERACCOUNT")]
        CREDIT_CUSTOMER_ACCOUNT,
        //@SerializedName("CHARGE_TO_BE_PAID_BY_CUSTOMER")
        [DrxEnumExtendedInformation("CHARGE_TO_BE_PAID_BY_CUSTOMER", "CHARGE TO BE PAID BY CUSTOMER")]
        CHARGE_TO_BE_PAID_BY_CUSTOMER
    }
}