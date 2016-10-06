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

namespace Net.Dreceiptx.Client
{
    public class UriParameters
    {
        private readonly Dictionary<string, string> _uriParameters = new Dictionary<string, string>();

        public void Add( string name, string value)
        {
            _uriParameters.Add(name, value);
        }

        public string Get(string name){
            return _uriParameters[name];
        }

        public string Get(string name, string defaultValue)
        {
            if(_uriParameters.ContainsKey(name)){
                return _uriParameters[name];
            }
            return defaultValue;
        }

        public bool Has(string name){
            return _uriParameters.ContainsKey(name);
        }
    
        public List<KeyValuePair<string, string>> GetKeyValuePairs()
        {
            return _uriParameters.Select(x => x).ToList();
        }
    }
}