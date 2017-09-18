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
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt.Config
{
    public sealed class Location
    {
        public static Location Australia = Add(
            "AUS", Country.Australia, Currency.AustralianDollar, Region.Australasia, TaxCode.GoodsAndServicesTax,
            Language.English, TimeZoneInfo.FindSystemTimeZoneById("Australia/Sydney"));


        public static Location Canada = Add(
            "CAN", Country.Canada, Currency.CanadianDollar, Region.Canada, TaxCode.GoodsAndServicesTax,
            Language.English, TimeZoneInfo.FindSystemTimeZoneById("Canada/Eastern"));


        public static Location France = Add(
            "FRA", Country.France, Currency.Euro, Region.EuroWest, TaxCode.ValueAddedTax, 
            Language.French, TimeZoneInfo.FindSystemTimeZoneById("Europe/Paris"));


        public static Location Ireland = Add(
            "IRE", Country.Ireland, Currency.Euro, Region.EuroWest, TaxCode.ValueAddedTax,
            Language.English, TimeZoneInfo.FindSystemTimeZoneById("Europe/Dublin"));


        public static Location NewZealand = Add(
            "NZL", Country.NewZealand, Currency.NewZealandDollar, Region.Australasia, TaxCode.GoodsAndServicesTax,
            Language.English, TimeZoneInfo.FindSystemTimeZoneById("NZ"));


        public static Location UnitedKingdom = Add(
            "GBR", Country.UnitedKingdom, Currency.BritishSterling, Region.EuroWest, TaxCode.ValueAddedTax,
            Language.English, TimeZoneInfo.FindSystemTimeZoneById("Europe/London"));


        public static Location USEastCoast = Add(
            "USEastCoast", Country.UnitedStatesOfAmerica, Currency.UsDollar, Region.USEast, TaxCode.ValueAddedTax,
            Language.English, TimeZoneInfo.FindSystemTimeZoneById("America/New_York"));


        public static Location USWestCoast = Add(
                "USWestCoast", Country.UnitedStatesOfAmerica, Currency.UsDollar, Region.USWest, TaxCode.ValueAddedTax,
                Language.English, TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles"));


        private static Dictionary<string, Location> _locations = new Dictionary<string, Location>();

        private static Location Add(string code, Country country, Currency currency,
            Region region, TaxCode salesTaxCode, Language language, TimeZoneInfo timeZone)
        {
            var location = new Location(code, country, currency, region, salesTaxCode, language, timeZone);
            _locations.Add(code, location);
            return location;
        }

        public static Location CodeOf(string code)
        {
            return _locations[code];
        }

        private Location(string code, Country country, Currency currency,
            Region region, TaxCode salesTaxCode, Language language, TimeZoneInfo timeZone)
        {
            Code = code;
            Country = country;
            Currency = currency;
            SalesTaxCode = salesTaxCode;
            Region = region;
            Language = language;
            TimeZone = timeZone;
        }
        public string Code { get; }
        public Country Country { get; }
        public Currency Currency { get; }
        public TaxCode SalesTaxCode { get; }
        public Region Region { get; }

        public Language Language { get; }
        public TimeZoneInfo TimeZone { get; }
    }
}
