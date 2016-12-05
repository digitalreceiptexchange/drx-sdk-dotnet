using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Net.Dreceiptx
{
    public class JsonSerializer
    {
        private static readonly JsonSerializerSettings _settings;

        static  JsonSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };
            _settings.Converters.Add(new StringEnumConverter());

        }
        ///// <summary>
        ///// Reads and deserializes object from specified xml data text
        ///// </summary>
        ///// <param name="xml">XML text</param>
        //public static T DeserializeFromString(string json)
        //{
        //    T result;
        //    if (string.IsNullOrEmpty(json))
        //    {
        //        result = null;
        //    }
        //    else
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(T));
        //        using (StringReader reader = new StringReader(xml))
        //        {
        //            result = (T)serializer.Deserialize(reader);
        //        }
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Reads and deserializes object from specified file name
        ///// </summary>
        ///// <param name="fileName">Schema list file name</param>
        //public static T DeserializeFromFile(string fileName)
        //{
        //    T result = null;
        //    if (File.Exists(fileName))
        //    {
        //        string xmlData = File.ReadAllText(fileName);
        //        result = DeserializeFromString(xmlData);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Serializes and saves object to specified file name
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="fileName"></param>
        //public static void SerializeToFile<T>(T item, string fileName) where T : class, new()
        //{
        //    using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        //    {
        //        XmlSerializer sr = new XmlSerializer(item.GetType());
        //        sr.Serialize(fileStream, item);
        //    }
        //}

        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        public static string SerializeToString<T>(T item)
        {
            return JsonConvert.SerializeObject(item, _settings);
        }

        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
