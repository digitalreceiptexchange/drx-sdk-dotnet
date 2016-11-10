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
using Net.Dreceiptx.Extensions;

namespace Net.Dreceiptx.Receipt.Ecom
{
    public class AVPList : Collection<AVP>
    {
        Dictionary<string, AVP> _dictionary = new Dictionary<string, AVP>();
        protected override void InsertItem(int index, AVP item)
        {
            AVP existing = null;
            if (_dictionary.TryGetValue(item.AttributeName, out existing))
            {
                Remove(existing);
            }
            _dictionary.Add(item.AttributeName, item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            _dictionary.Remove(this[index].AttributeName);
            base.RemoveItem(index);
        }

        public void Add(string code, AVP value)
        {
            _dictionary.Add(code, value);
        }

        public void Add(string code, string value)
        {
            Add(code, new AVP(code, value));
        }

        public string GetValue(string code)
        {
            return GetValue(code, null);
        }

        public string GetValue(string code, string defaultValue)
        {
            return GetAVP(code, new AVP(code, defaultValue)).Value;
        }

        public bool Contains(string code)
        {
            return _dictionary.ContainsKey(code);
        }

        public AVP GetAVP(string code)
        {
            return GetAVP(code, null);
        }

        public AVP GetAVP(string code, AVP defaultValue)
        {
            return _dictionary.GetOrDefault(code, defaultValue);
        }

    }
}