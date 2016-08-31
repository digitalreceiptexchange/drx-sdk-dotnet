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