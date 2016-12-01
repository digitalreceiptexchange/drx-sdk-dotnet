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
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Client
{
    [DataContract]
    public class UserReponseResponseData
    {
        [DataMember]
        public int? UsersRegistered { get; set; }
        [DataMember]
        public Dictionary<string, ResponseData> UserIdentifiers { get; set; }
    }

    [DataContract]
    public class NewUserRegistrationResponseData
    {
        [DataMember]
        public int? UsersRegistered { get; set; }
        [DataMember]
        public Dictionary<string, UserResponsData> Users { get; set; }
    }

    [DataContract]
    public class UserResponsData
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public int? Code { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string Exception { get; set; }
    }
}
