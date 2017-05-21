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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Net.Dreceiptx.Receipt.Serialization.Json
{
    public class JsonSerializer
    {
        static JsonSerializer()
        {
            JsonSettings = CreateDefaultSettings();
        }

        public static JsonSerializerSettings CreateDefaultSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };
            return settings;
        }

        public static JsonSerializerSettings JsonSettings { get; }

        /// <summary>
        /// Serializes the given object T to a JSON string
        /// </summary>
        /// <param name="item">The item to be serialized</param>
        public static string SerializeToString<T>(T item)
        {
            var settings = CreateDefaultSettings();
            settings.Converters.Add(new StringEnumConverter());
            //Settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(item, JsonSettings);
        }

        /// <summary>
        /// Deserializes the given json string into object of type T
        /// </summary>
        /// <param name="json">The JSON string to be deserialized</param>
        public static T Deserialize<T>(string json)
        {
            var settings = CreateDefaultSettings();
            settings.Converters.Add(new StringEnumConverter());
            settings.Converters.Add(new LineItemCreater());
            //Settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            return JsonConvert.DeserializeObject<T>(json, JsonSettings);
        }
    }
}
