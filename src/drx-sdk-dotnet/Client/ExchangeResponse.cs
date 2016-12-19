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
    public class ExchangeResponse
    {
        /// <summary>
        /// Gets and sets Success to indicate if the request we 
        /// was successfull or not
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// Gets and sets the Code of the Response
        /// </summary>
        [DataMember]
        public int? Code { get; set; }

        /// <summary>
        /// Gets and sets the ExceptionMessage of the reponse if there was 
        /// one on the server
        /// </summary>
        [DataMember]
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets and sets the ResponseData
        /// </summary>
        [DataMember]
        public ResponseData ResponseData { get; set; }
        
    }
}