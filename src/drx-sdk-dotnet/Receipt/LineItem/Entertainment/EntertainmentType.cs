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
    public enum EntertainmentType
    {
        [DrxEnumExtendedInformation("ENT0000", "Standard")]
        [EnumMember(Value = "ENT0000")]
        Standard,

        [DrxEnumExtendedInformation("ENT0001", "Beverages and refreshments")]
        [EnumMember(Value = "ENT0001")]
        BeveragesAndRefreshments,

        [DrxEnumExtendedInformation("ENT0002", "Light meals and lunches")]
        [EnumMember(Value = "ENT0002")]
        LightMeals,

        [DrxEnumExtendedInformation("ENT0003", "Employee recreation activities")]
        [EnumMember(Value = "ENT0003")]
        EmployeeRecreation,

        [DrxEnumExtendedInformation("ENT0004", "Employee business meals")]
        [EnumMember(Value = "ENT0004")]
        EmployeeMeal,

        [DrxEnumExtendedInformation("ENT0005", "Client business meals")]
        [EnumMember(Value = "ENT0005")]
        ClientMeal,

        [DrxEnumExtendedInformation("ENT0010", "Social activity functions provided to employees")]
        [EnumMember(Value = "ENT0010")]
        SocialEventOrParty
    }
}