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

namespace Net.Dreceiptx.Receipt.LineItem
{
    [DataContract]
    public class TradeItemDescriptionInformation
    {
        protected TradeItemDescriptionInformation()
        {
        }

        public TradeItemDescriptionInformation(string brandName, string descriptionShort, string tradeItemDescription)
        {
            BrandName = brandName;
            DescriptionShort = descriptionShort;
            TradeItemDescription = tradeItemDescription;
        }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string DescriptionShort { get; set; }

        [DataMember]
        public string TradeItemDescription { get; set; }

        [DataMember]
        public bool IsTradeItemAService { get; set; } = false;

        [DataMember]
        public bool IsTradeItemReconditioned { get; set; } = false;

        [DataMember]
        public string TradeItemGroupIdentificationCode { get; set; }

        [DataMember]
        public string Gtin { get; set; }

        [DataMember(Name = "AdditionalTradeItemIdentification")]
        public TradeItemIdentification ItemIdentification { get; set; } = new TradeItemIdentification();
    }
}