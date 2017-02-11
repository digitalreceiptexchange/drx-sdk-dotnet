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
using Net.Dreceiptx.Receipt.Serialization.Json;

namespace Net.Dreceiptx.Client
{
    [DataContract]
    public class NewUserRegistrationResponse
    {
        /// <summary>
        /// Gets and sets Success. When Success is true it does 
        /// not mean that all the Users have been registered. It is possible
        /// one or more users may fail to register. Could should check the 
        /// ResponseData property to ensure each user has been created successfully.
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// Gets and sets the Code
        /// </summary>
        [DataMember]
        public int? Code { get; set; }

        /// <summary>
        /// Gets and sets the ExceptionMessage if there was an error on the server.
        /// </summary>
        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public NewUserRegistrationResponseData ResponseData { get; set; }

        /// <summary>
        /// Serializes the message to a JSON format
        /// </summary>
        /// <returns>JSON message as a string</returns>
        public string SerializeToJson()
        {
            return JsonSerializer.SerializeToString(this);
        }

        /// <summary>
        /// Deserializes the given JSON string to a NewUserRegistrationResponse instance
        /// </summary>
        /// <param name="json">The JSON to be deserialized</param>
        /// <returns></returns>
        public static NewUserRegistrationResponse DeserializeFromJson(string json)
        {
            return JsonSerializer.Deserialize<NewUserRegistrationResponse>(json);
        }
    }
}