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
namespace Net.Dreceiptx.Receipt.LineItem
{
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

        //@SerializedName("descriptionShort")
        public string DescriptionShort { get; set; }

        //@SerializedName("isTradeItemAService")
        public bool IsTradeItemAService { get; set; } = false;

        //@SerializedName("isTradeItemReconditioned")
        public bool IsTradeItemReconditioned { get; set; } = false;

        //@SerializedName("brandName")
        public string BrandName { get; set; }

        //@SerializedName("tradeItemDescription")
        public string TradeItemDescription { get; set; }

        //@SerializedName("tradeItemGroupIdentificationCode")
        public string TradeItemGroupIdentificationCode { get; set; }
    }
}