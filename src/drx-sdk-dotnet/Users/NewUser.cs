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
using System.Runtime.Serialization;

namespace Net.Dreceiptx.Users
{
    [DataContract]
    public class NewUser
    {
        private readonly bool _addAsIdentifier;
        private string _email;
        private bool _addEmailAsIdentifier = true;

        public NewUser()
        {
        }

        public NewUser(string email)
        {
            Email = email;
        }

        public NewUser(string email, bool addEmailAsIdentifier)
        {
            _addAsIdentifier = addEmailAsIdentifier;
            _addEmailAsIdentifier = _addAsIdentifier;
            Email = email;
        }
    
    
        public void SetEmail(string email, bool addAsIdentifier){
            _addEmailAsIdentifier = addAsIdentifier;
            Email = email;
        }

        /// <summary>
        /// Gets and sets the user Email address
        /// </summary>
        [DataMember(Name = "UserEmail")]
        public string Email
        {
            get { return _email;}
            set
            {
                _email = value;
                if (_addEmailAsIdentifier)
                {
                    Identifiers.Add(new Identifier { Type = UserIdentifierType.Email, Value = value });
                }
            }
        }

        public void AddIdentifier( UserIdentifierType identifierType, string identifier)
        {
            Identifiers.Add(new Identifier {Type = identifierType, Value = identifier});
        }

        [DataMember]
        public List<Identifier> Identifiers { get; set; } = new List<Identifier>();

        public void AddConfigOption( UserConfigOptionType configOptionType, string optionValue)
        {
            Config.Add(new UserConfigurationOption(configOptionType, optionValue));
        }

        /// <summary>
        /// Gets and sets the 
        /// </summary>
        [DataMember(Name = "Config")]
        public List<UserConfigurationOption> Config { get; set; } = new List<UserConfigurationOption>();
    }
}