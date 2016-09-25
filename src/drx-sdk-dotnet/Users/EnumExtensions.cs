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

    public static TAttribute GetCustomAttribute<TAttribute,TValue>(this TValue value) where TAttribute : Attribute
    {
        object[] attributes = value.GetType().GetCustomAttributes(typeof(TAttribute), false);
        return ((TAttribute) attributes[0]);
    }
}