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

namespace Net.Dreceiptx.Receipt.Ecom
{

    public class AVPList
    {
        private Dictionary<string, AVP> _avpList = new Dictionary<string, AVP>();

        public void Add(string code, AVP avp)
        {
            _avpList.Add(code, avp);
        }

        public void Add(AVP avp)
        {
            _avpList.Add(avp.AttributeName, avp);
        }

        public void Add(string code, string value)
        {
            _avpList.Add(code, new AVP(code, value));
        }

        public AVP GetAVP(string code)
        {
            return GetAVP(code, null);
        }

        public AVP GetAVP(string code, AVP defaultValue)
        {
            AVP value;
            if (!_avpList.TryGetValue(code, out value))
            {
                value = defaultValue;
            }
            return value;
        }

        public string GetAVPValue(string code)
        {
            return GetAVPValue(code, null);
        }

        public string GetAVPValue(string code, string defaultValue)
        {
            AVP value;
            if (!_avpList.TryGetValue(code, out value))
            {
                return defaultValue;
            }
            return value.Value;
        }

        public bool Contains(string code)
        {
            return _avpList.ContainsKey(code);
        }

        public List<KeyValuePair<string, AVP>> KeyValuePairs => _avpList.ToList();
    }
}