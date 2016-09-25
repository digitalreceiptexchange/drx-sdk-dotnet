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

namespace Net.Dreceiptx.Users
{
    public class NewUser
    {
        private readonly bool _addAsIdentifier;
        private string _email; 
        private readonly Dictionary<UserIdentifierType, string> _identifiers = new Dictionary<UserIdentifierType, string>();
        private bool _addEmailAsIdentifier = true;
    
        public NewUser()
        {
        }

        public NewUser(string email)
        {
            Email = email;
        }

        public NewUser(string email, bool addAsIdentifier)
        {
            _addAsIdentifier = addAsIdentifier;
            _addEmailAsIdentifier = _addAsIdentifier;
            Email = email;
        }
    
    
        public void SetEmail(string email, bool addAsIdentifier){
            _addEmailAsIdentifier = addAsIdentifier;
            Email = email;
        }

        public string Email
        {
            get { return _email;}
            set
            {
                _email = value;
                if (_addEmailAsIdentifier)
                {
                    _identifiers.Add(UserIdentifierType.EMAIL, _email);
                }
            }
        }

        public void AddIdentifier( UserIdentifierType identifierType, string identifier)
        {
            _identifiers.Add(identifierType, identifier);
        }
    
        public Dictionary<UserIdentifierType, string> Identifiers
        {
            get { return _identifiers;}
        }
    
        public void AddConfigOption( UserConfigOptionType configOptionType, string optionValue)
        {
            Config.Add(configOptionType, optionValue);
        }
    
        public Dictionary<UserConfigOptionType, string> Config { get; } = new Dictionary<UserConfigOptionType, string>();
    }
}