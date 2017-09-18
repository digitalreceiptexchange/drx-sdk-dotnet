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
    public sealed class Region
    {
        public static Region Australasia = Add("AUS",
            "9377778071234",
            "https://aus-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        public static Region AsiaPacificCentral = Add("APC",
            "9377778071234",
            "https://apc-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        public static Region Canada = Add("CAN",
            "9377778071234",
            "https://can-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        public static Region EuroEast = Add("EUE",
            "9377778071234",
            "https://eue-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        public static Region EuroWest = Add("EUW",
            "9377778071234",
            "https://euw-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        public static Region USEast = Add("USE",
            "9377778071234",
            "https://use-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        public static Region USWest = Add("USW",
            "9377778071234",
            "https://usw-api.dreceiptx.net",
            "https://directory.dreceiptx.net");

        private static Dictionary<string, Region> _regions = new Dictionary<string, Region>();

        private static Region Add(string code, string gln, string apiEndpoint, string directoryEndpoint)
        {
            var region = new Region(code, gln, apiEndpoint, directoryEndpoint);
            _regions.Add(code, region);
            return region;
        }

        public static Region CodeOf(string code)
        {
            return _regions[code];
        }

        private  Region(string code, string gln, string apiEndpoint, string directoryEndpoint)
        {
            Code = code;
            GLN = gln;
            APIEndpoint = apiEndpoint;
            DirectoryEndpoint = directoryEndpoint;
        }

        public string Code { get; }
        public string GLN { get; }
        public string APIEndpoint { get; }
        public string DirectoryEndpoint { get; }
    }
}
