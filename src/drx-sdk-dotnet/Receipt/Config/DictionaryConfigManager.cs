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

namespace Net.Dreceiptx.Receipt.Config
{
    /**
     * Simple Dictionary Based ConfigManager
     */
    public class DictionaryConfigManager : IConfigManager
    {
        private readonly Dictionary<string, string> _config = new Dictionary<string, string>();

        public string GetConfigValue(string key)
        {
            return _config[key];
        }

        public void SetConfigValue(string key, string value)
        {
            _config.Add(key, value);
        }

        public void SetConfigValue(string key, string value, bool commit)
        {
            SetConfigValue(key, value);
        }

        public bool Exists(string key)
        {
            return _config.ContainsKey(key);
        }
    }
}