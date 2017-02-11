using System;
using System.Reflection;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.LineItem.Construction;
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
        private bool canRead = true;
        private bool canWrite = false;

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

        public override bool CanRead
        {
            get {
                return canRead;
            }
        }

        public override bool CanWrite {
            get
            {
                return canWrite;
            }
        }

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

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(LineItemObject));
        }
    }
}