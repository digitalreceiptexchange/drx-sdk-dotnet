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
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Net.Dreceiptx.Extensions;

namespace Net.Dreceiptx.Receipt.LineItem
{
    public class TradeItemIdentification : Collection<TradeItemIdentificationInformation>
    {
        Dictionary<string, TradeItemIdentificationInformation> _dictionary = new Dictionary<string, TradeItemIdentificationInformation>();
        protected override void InsertItem(int index, TradeItemIdentificationInformation item)
        {
            TradeItemIdentificationInformation existing = null;
            if (_dictionary.TryGetValue(item.AdditionalTradeItemIdentificationType, out existing))
            {
                Remove(existing);
            }
            _dictionary.Add(item.AdditionalTradeItemIdentificationType, item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            _dictionary.Remove(this[index].AdditionalTradeItemIdentificationType);
            base.RemoveItem(index);
        }

        public void Add(string code, string value)
        {
            Add(new TradeItemIdentificationInformation(code, value));
        }

        public string Get(string code)
        {
            return Get(code, null);
        }

        public string Get(string code, string defaultValue)
        {
            return _dictionary.GetOrDefault(code, new TradeItemIdentificationInformation("Code", defaultValue)).AdditionalTradeItemIdentificationValue;
        }

        public bool Contains(string code)
        {
            return _dictionary.ContainsKey(code);
        }
    }

    [DataContract]
    public class TradeItemIdentificationInformation
    {
        public TradeItemIdentificationInformation(string type, string value)
        {
            AdditionalTradeItemIdentificationType = type;
            AdditionalTradeItemIdentificationValue = value;
        }

        [DataMember]
        public string AdditionalTradeItemIdentificationType { get; set; }
        [DataMember]
        public string AdditionalTradeItemIdentificationValue { get; set; }
    }

}