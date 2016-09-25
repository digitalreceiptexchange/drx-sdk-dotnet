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
using System.IO;
using System.Xml.Serialization;

namespace Net.Dreceiptx
{
    /// <summary>
    /// Provides generic methods to save and load a serializable object to and from XML 
    /// </summary>
    /// <typeparam name="T">Object type to be serialized</typeparam>    
    public class XmlSerializer<T> where T : class, new()
    {

        /// <summary>
        /// Reads and deserializes object from specified xml data text
        /// </summary>
        /// <param name="xml">XML text</param>
        public static T DeserializeFromString(string xml)
        {
            T result;
            if (string.IsNullOrEmpty(xml))
            {
                result = new T();
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return result;
        }

        /// <summary>
        /// Reads and deserializes object from specified file name
        /// </summary>
        /// <param name="fileName">Schema list file name</param>
        public static T DeserializeFromFile(string fileName)
        {
            T result = null;
            if (File.Exists(fileName))
            {
                string xmlData = File.ReadAllText(fileName);
                result = DeserializeFromString(xmlData);
            }
            return result;
        }

        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fileName"></param>
        public static void SerializeToFile(T item, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer sr = new XmlSerializer(item.GetType());
                sr.Serialize(fileStream, item);
            }
        }

        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        public static string SerializeToString(T item)
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer sr = new XmlSerializer(item.GetType());
                sr.Serialize(writer, item);
                return writer.ToString();
            }
        }
    }

    public class XmlSerializer2
    {
        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        public static string SerializeToString(object item)
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer sr = new XmlSerializer(item.GetType());
                sr.Serialize(writer, item);
                return writer.ToString();
            }
        }
    }
}