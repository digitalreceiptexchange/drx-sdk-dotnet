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
    public class Accommodation : LineItem
    {
        public static readonly string LineItemTypeValue = "TRAVEL0001";

        public Accommodation(AccommodationType accommodationType, string provider, string shortDescription,
            string longDescription, int nights, double rate) : base(provider, shortDescription, longDescription, nights, rate)
        {
            setTradeItemGroupIdentificationCode(accommodationType.code());
            addTradeItemIdentification(LineItem.LineItemTypeIdentifier, Accommodation.LineItemTypeValue);
        }

        public Accommodation(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price) 
            : base(tradeItemDescriptionInformation, quantity, price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            addTradeItemIdentification(LineItem.LineItemTypeIdentifier, Accommodation.LineItemTypeValue);
        }

        public string ProviderName => BrandName;

        public string ShortDescription => Name;

        public string DetailedDescription => Description;

        public AccommodationType getAccommodationType()
        {
            return (AccommodationType) getLineItemType(AccommodationType.class,
            AccommodationType.DEFAULT)
            ;
        }

        public void setPassengerName(string passengerName)
        {
            _AVPList.Add(AVPType.PASSENGER_NAME.Code(), passengerName);
        }

        public DateTime getDepartureDate()
        {
            return getDespatchDate();
        }

        public void setDepartureDate(DateTime departureDate)
        {
            setDespatchDate(departureDate);
        }

        public DateTime ArrivalDate
        {
            get { return DeliveryDate; }
            set { DeliveryDate = value; }
        }

    }
}