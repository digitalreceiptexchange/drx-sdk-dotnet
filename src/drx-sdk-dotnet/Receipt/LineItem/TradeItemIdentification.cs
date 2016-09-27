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

using System.Collections.Generic;
using System.Linq;
using Net.Dreceiptx.Extensions;

namespace Net.Dreceiptx.Receipt.LineItem
{
    public class TradeItemIdentification
    {
        private Dictionary<string, string> _additionalTradeItemIdentification = new Dictionary<string, string>();

        public void Add(string code, string value)
        {
            _additionalTradeItemIdentification.Add(code, value);
        }

        public string Get(string code)
        {
            return Get(code, null);
        }

        public string Get(string code, string defaultValue)
        {
            return _additionalTradeItemIdentification.GetOrDefault(code, defaultValue);
        }

        public bool Contains(string code)
        {
            return _additionalTradeItemIdentification.ContainsKey(code);
        }

        public List<KeyValuePair<string, string>> KeyValuePairs => _additionalTradeItemIdentification.ToList();
    }

}