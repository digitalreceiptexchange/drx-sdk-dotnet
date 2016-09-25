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

namespace Net.Dreceiptx.Users
{
    public class Users
    {
        private readonly Dictionary<string, User> _userDictionary = new Dictionary<string, User>();

        public void Add(string identifier, User user)
        {
            _userDictionary.Add(identifier, user);
        }

        public User GetUser(string identifier)
        {
            return _userDictionary[identifier];
        }

        //TODO: Not sure why this is needed
        public List<KeyValuePair<string, User>> GetKeyValuePairs()
        {
            return _userDictionary.Select(x => x).ToList();
        }
    }

}