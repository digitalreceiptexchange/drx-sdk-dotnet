using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Net.Dreceiptx
{
    public class JsonSerializer
    {
        private static readonly JsonSerializerSettings Settings;

        static  JsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };
            Settings.Converters.Add(new StringEnumConverter());

        }

        public static JsonSerializerSettings JsonSettings => Settings;

        /// <summary>
        /// Serializes the given object T to a JSON string
        /// </summary>
        /// <param name="item">The item to be serialized</param>
        public static string SerializeToString<T>(T item)
        {
            return JsonConvert.SerializeObject(item, Settings);
        }

        /// <summary>
        /// Deserializes the given json string into object of type T
        /// </summary>
        /// <param name="json">The JSON string to be deserialized</param>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
}
