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
using System;
using System.Collections.Generic;
using Net.Dreceiptx.Users;

public static class EnumExtensions
{
    private static Dictionary<UserConfigOptionType, DrxEnumExtendedInformationAttribute> userConfigTypeDictionary;

    public static DrxEnumExtendedInformationAttribute ExtendedInformation(this UserConfigOptionType userConfigOptionType)
    {
        if (userConfigTypeDictionary == null)
        {
            userConfigTypeDictionary = new Dictionary<UserConfigOptionType, DrxEnumExtendedInformationAttribute>();
            foreach (UserConfigOptionType instance in Enum.GetValues(typeof(UserConfigOptionType)))
            {
                userConfigTypeDictionary.Add(instance, instance.GetCustomAttribute<DrxEnumExtendedInformationAttribute, UserConfigOptionType>());
            }
        }
        return userConfigTypeDictionary[userConfigOptionType];
    }

    public static string Value(this UserConfigOptionType userConfigOptionType)
    {
        return userConfigOptionType.ExtendedInformation().Value;
    }

    public static string Description(this UserConfigOptionType userConfigOptionType)
    {
        return userConfigOptionType.ExtendedInformation().Description;
    }

    public static DrxEnumExtendedInformationAttribute ExtendedInformation(this UserIdentifierType enumValue)
    {
        return enumValue.GetCustomAttribute<DrxEnumExtendedInformationAttribute, UserIdentifierType>();
    }

    public static string Value(this UserIdentifierType enumValue)
    {
        return enumValue.ExtendedInformation().Value;
    }

    

    public static TAttribute GetCustomAttribute<TAttribute,TValue>(this TValue value) where TAttribute : Attribute
    {
        object[] attributes = value.GetType().GetCustomAttributes(typeof(TAttribute), false);
        return ((TAttribute) attributes[0]);
    }
}