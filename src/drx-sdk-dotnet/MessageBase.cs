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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Net.Dreceiptx
{
    [DataContract]
    public abstract class MessageBase
    {
        private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public virtual string SerializeToXml()
        {
            return XmlSerializer2.SerializeToString(this);
        }

        public virtual string SerializeToJson()
        {
            // Nicely formatted.
            return JsonConvert.SerializeObject(this, Formatting.Indented, JsonSettings);
            // Bog standard compress json without spaces
            //return JsonConvert.SerializeObject(this);
        }
    }
}