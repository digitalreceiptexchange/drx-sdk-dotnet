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
    public class Flight : LineItem
    {
        public static readonly string LineItemTypeValue = "TRAVEL0002";

        public Flight(FlightType flightType, string airline, string shortItinerary, string longItinerary, int quantity,
            decimal price) : base(airline, shortItinerary, longItinerary, quantity, price)
        {
            TradeItemGroupIdentificationCode = flightType.Value();
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public Flight(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, decimal price)
                :base(tradeItemDescriptionInformation, quantity, price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public string Airline => BrandName;

        public string Itinerary => Name;

        public string ItineraryDescription => Description;

    //    public FlightType getFlightType()
    //    {
    //        return (FlightType)this.getLineItemType(FlightType.class,
    //    FlightType.DEFAULT)
    //    ;
    //}

    public string PassengerName
        {
            get { return _AVPList.GetValue(AVPType.PASSENGER_NAME.Value()); }
            set { _AVPList.Add(AVPType.PASSENGER_NAME.Value(), value); }
        }

        public string PassengerNameRecord
        {
            get { return _AVPList.GetValue(AVPType.PASSENGER_NAME_RECORD.Value()); }
            set { _AVPList.Add(AVPType.PASSENGER_NAME_RECORD.Value(), value); }
        }

        public DateTime DepartureDate
        {
            get { return DespatchDate; }
            set { DespatchDate = value; }
        }

        public string TicketNumber
        {
            get {  return SerialNumber; }
            set { SerialNumber = value; }
        }

        public FlightDestinationType? FlightDestinationType
        {
            get
            {
                string flightType = _AVPList.GetAVP(AVPType.FLIGHT_DESTINATION_TYPE.Value())?.Value;
                if (!string.IsNullOrWhiteSpace(flightType))
                {
                    return EnumExtensions.FlightDestinationType(flightType);
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    _AVPList.Add(AVPType.FLIGHT_DESTINATION_TYPE.Value(), value.Value());
                }
                else
                {
                    _AVPList.Remove(AVPType.FLIGHT_DESTINATION_TYPE.Value());
                }
            }
            
        }
    }
}