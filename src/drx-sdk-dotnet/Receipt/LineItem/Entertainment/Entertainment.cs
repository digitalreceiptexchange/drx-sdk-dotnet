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
using Net.Dreceiptx.Receipt.Ecom;

namespace Net.Dreceiptx.Receipt.LineItem.Travel
{
    public class Entertainment : LineItem
    {
        public static readonly string LineItemTypeValue = "TRAVEL0001";

        public Entertainment(AccommodationType accommodationType, string provider, string shortDescription,
            string longDescription, int nights, decimal rate) 
            : base(provider, shortDescription, longDescription, (double)nights, rate)
        {
            TradeItemGroupIdentificationCode = accommodationType.Value();
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public Entertainment(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity,
            decimal price) : this(tradeItemDescriptionInformation, (double)quantity, price)
        {
            
        }
        public Entertainment(TradeItemDescriptionInformation tradeItemDescriptionInformation, double quantity, decimal price) 
            : base(tradeItemDescriptionInformation, quantity, price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public string ProviderName => BrandName;

        public string ShortDescription => Name;

        public string DetailedDescription => Description;

        public EntertainmentType GetEntertainmentType()
        {
            return (EntertainmentType)this.GetLineItemType(typeof(EntertainmentType), EntertainmentType.Standard);
        }

        public string PassengerName
        {
            get { return _AVPList.GetValue(AVPType.PASSENGER_NAME.Value()); }
            set { _AVPList.Add(AVPType.PASSENGER_NAME.Value(), value); }
        }

        public Boolean? IncludesAlcohol
        {
            get {
                if (_AVPList.Contains(AVPType.INCLUDES_ALCOHOL.Value()))
                {
                    return Boolean.Parse(_AVPList.GetValue(AVPType.INCLUDES_ALCOHOL.Value()));
                }
                else
                {
                    return null;
                }
                
            }
            set { _AVPList.Add(AVPType.INCLUDES_ALCOHOL.Value(), value.ToString()); }
        }

        public DateTime? DepartureDate
        {
            get { return DespatchDate; }
            set { DespatchDate = value; }
        }

        public DateTime? ArrivalDate
        {
            get { return DeliveryDate; }
            set { DeliveryDate = value; }
        }

    }
}