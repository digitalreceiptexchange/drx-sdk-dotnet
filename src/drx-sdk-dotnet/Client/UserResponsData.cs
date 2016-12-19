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

using System.Runtime.Serialization;

namespace Net.Dreceiptx.Client
{
    [DataContract]
    public class UserResponsData
    {
        /// <summary>
        /// Gets and sets Success. When true the user was successfully created
        /// when false the user failed to register and you should check the
        /// Exception property.
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// Gets and sets the Code
        /// </summary>
        [DataMember]
        public int? Code { get; set; }
        
        /// <summary>
        /// Gets and sets the Email that was attempted to or was registered
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// Gets and sets the Guid of the registered user if they were successfully regitered.
        /// </summary>
        [DataMember]
        public string Guid { get; set; }

        /// <summary>
        /// Gets and sets the Exception message if the user failed to be registered.
        /// </summary>
        [DataMember]
        public string Exception { get; set; }
    }
}