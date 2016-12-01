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
    public class NewUserRegistrationRequest
    {
        [DataMember]
        public List<NewUser> Users { get; set; }

        [DataMember]
        public int UsersToRegister
        {
            get { return Users.Count; }
            set { ; }
        }
    }
    [DataContract]
    public class NewUser
    {
        private readonly bool _addAsIdentifier;
        private string _email; 
        private  List<Identifier> _identifiers = new List<Identifier>();
        private bool _addEmailAsIdentifier = true;
        private List<UserConfigurationOption> _config = new List<UserConfigurationOption>();

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

        [DataMember(Name = "UserEmail")]
        public string Email
        {
            get { return _email;}
            set
            {
                _email = value;
                if (_addEmailAsIdentifier)
                {
                    _identifiers.Add(new Identifier { Type = UserIdentifierType.Email, Value = value });
                }
            }
        }

        public void AddIdentifier( UserIdentifierType identifierType, string identifier)
        {
            _identifiers.Add(new Identifier {Type = identifierType, Value = identifier});
        }

        [DataMember]
        public List<Identifier> Identifiers
        {
            get { return _identifiers;}
            set { _identifiers = value; }
        }
    
        public void AddConfigOption( UserConfigOptionType configOptionType, string optionValue)
        {
            Config.Add(new UserConfigurationOption(configOptionType, optionValue));
        }

        [DataMember]
        public List<UserConfigurationOption> Config
        {
            get { return _config; }
            set { _config = value; }
        }
    }

    public class Identifier
    {
        public UserIdentifierType Type { get; set; }
        public string Value { get; set; }
    }

    public class UserConfigurationOption
    {
        public UserConfigurationOption()
        {
        }

        public UserConfigurationOption(UserConfigOptionType option, string value)
        {
            Option = option;
            Value = value;
        }
        public UserConfigOptionType Option { get; set; }
        public string Value { get; set; }
    }
}