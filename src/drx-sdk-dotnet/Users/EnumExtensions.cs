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
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.LineItem.Travel;
using Net.Dreceiptx.Users;

public static class EnumExtensions
{
    public static DrxEnumExtendedInformationAttribute ExtendedInformation(this Enum enumValue)
    {
        return enumValue.GetCustomAttribute<DrxEnumExtendedInformationAttribute>();
    }

    public static string Value(this Enum enumValue)
    {
        return enumValue.GetCustomAttribute<DrxEnumExtendedInformationAttribute>()?.Value;
    }

    public static string Description(this Enum enumValue)
    {
        return enumValue.GetCustomAttribute<DrxEnumExtendedInformationAttribute>()?.Description;
    }

    public static string Value(this UserIdentifierType enumValue)
    {
        return enumValue.ExtendedInformation().Value;
    }

    public static TAttribute GetCustomAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var type = value.GetType();
        var memInfo = type.GetMember(value.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(TAttribute), false);
        return (attributes.Length > 0) ? (TAttribute)attributes[0] : null;
    }

    private static Dictionary<string, Net.Dreceiptx.Receipt.Common.Currency> _currencyDictionary;

    private static DrxEnumExtendedInformationHelper<Currency> _currencyConverted = new DrxEnumExtendedInformationHelper<Currency>();
    public static Net.Dreceiptx.Receipt.Common.Currency Currency(string currencyCode)
    {
        return _currencyConverted.GetByValue(currencyCode);
    }

    public class DrxEnumExtendedInformationHelper<T> where T: struct
    {
        private static Dictionary<string, T> _enumDictionary;

        static DrxEnumExtendedInformationHelper()
        {
            _enumDictionary = new Dictionary<string, T>();
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                var type = enumValue.GetType();
                var memInfo = type.GetMember(enumValue.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DrxEnumExtendedInformationAttribute), false);
                var result = (attributes.Length > 0) ? (DrxEnumExtendedInformationAttribute)attributes[0] : null;
                _enumDictionary.Add(result?.Value, enumValue);
            }
        }

        public T GetByValue(string value)
        {
            T result;
            if (value == null || !_enumDictionary.TryGetValue(value, out result))
            {
                throw new InvalidOperationException($"{typeof(T).Name} {value} is invalid");
            }
            return result;
        }

    }

    private static Dictionary<string, FlightDestinationType> _flightDestinationTypes;

    public static FlightDestinationType FlightDestinationType(string flightDestinationType)
    {
        if (_flightDestinationTypes == null)
        {
            _flightDestinationTypes = new Dictionary<string, FlightDestinationType>();
            foreach (FlightDestinationType flightDestination in Enum.GetValues(typeof(FlightDestinationType)))
            {
                _flightDestinationTypes.Add(flightDestination.Value(), flightDestination);
            }
        }
        FlightDestinationType result;
        if (flightDestinationType == null || !_flightDestinationTypes.TryGetValue(flightDestinationType, out result))
        {
            throw new InvalidOperationException($"FlightDestinationType {flightDestinationType} is invalid");
        }
        return result;
    }
}