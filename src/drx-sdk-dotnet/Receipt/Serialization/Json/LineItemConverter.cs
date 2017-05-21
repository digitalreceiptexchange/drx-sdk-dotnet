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
using System;
using Net.Dreceiptx.Receipt.LineItem;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace Net.Dreceiptx.Receipt.Serialization.Json
{
    public class LineItemCreater : CustomCreationConverter<LineItem.LineItem>
    {
        public override LineItem.LineItem Create(Type objectType)
        {
            //return new StandardLineItem();
            return new LineItemObject();
        }
    }

    public class LineItemConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject joLineItem = JObject.Load(reader);
            JObject joTransactionalTradeItem = (JObject)joLineItem["transactionalTradeItem"];
            TransactionalTradeItem transactionalTradeItem = serializer.Deserialize<TransactionalTradeItem>(joTransactionalTradeItem.CreateReader());

            JArray joAdditionalTradeItemIdentification = (JArray)joTransactionalTradeItem["additionalTradeItemIdentification"];
            string typeCode = StandardLineItem.LineItemTypeValue;
            foreach (JObject item in joAdditionalTradeItemIdentification)
            {
                if (item["additionalTradeItemIdentificationType"].ToString() == LineItem.LineItem.LineItemTypeIdentifier)
                {
                    typeCode = item["additionalTradeItemIdentificationValue"].ToString();
                }
            }
            LineItem.LineItem result = LineItemFactory.CreateLineItem(typeCode, 
                transactionalTradeItem.TradeItemDescriptionInformation,
                (int)joLineItem["invoicedQuantity"],
                (decimal)joLineItem["itemPriceExclusiveAllowancesCharges"]);

            using (var subReader = joLineItem.CreateReader())
                serializer.Populate(subReader, result);

            return result;
        }

        public override bool CanRead { get; } = true;

        public override bool CanWrite { get; } = false;

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(LineItem.LineItem));
        }
    }

    public class LineItemObjectConverter : JsonConverter
    {
        private bool canRead = true;

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            return serializer.Deserialize<LineItemObject>(reader);
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(LineItemObject));
        }
    }
}