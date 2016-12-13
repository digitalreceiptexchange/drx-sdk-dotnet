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
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Ecom;

namespace Net.Dreceiptx.Receipt.LineItem.Travel
{
    public class GroundTransport : LineItem
    {
        public static readonly string LineItemTypeValue = "TRAVEL0003";

        public GroundTransport(GroundTransportType groundTransportType, string provider, string shortDescription,
            string longDescription, decimal price) 
            : this(groundTransportType, provider, shortDescription, longDescription, 1, price)
        {
        }

        public GroundTransport(GroundTransportType groundTransportType, string provider, string shortDescription,
            string longDescription, int quantity, decimal price) 
            : base(provider, shortDescription, longDescription, quantity, price)
        {
            TradeItemGroupIdentificationCode = groundTransportType.Value();
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public GroundTransport(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, decimal price)
            : base(tradeItemDescriptionInformation, quantity, price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            AddTradeItemIdentification(LineItemTypeIdentifier, LineItemTypeValue);
        }

        public string Provider => BrandName;

        //public GroundTransportType getGroundTransportType()
        //{
        //    return (GroundTransportType) this.getLineItemType(GroundTransportType.class,
        //    GroundTransportType.DEFAULT)
        //    ;
        //}

        public string TripDescription => Description;

        public string PassengerName
        {
            get { return _AVPList.GetValue(AVPType.PASSENGER_NAME.Value()); }
            set { _AVPList.Add(AVPType.PASSENGER_NAME.Value(), value); }
        }

        public string DriveName
        {
            get { return _AVPList.GetValue(AVPType.DRIVER_NAME.Value()); }
            set { _AVPList.Add(AVPType.DRIVER_NAME.Value(), value); }
        }

        public string VehicleIdentifier
        {
            get { return _AVPList.GetValue(AVPType.VEHICLE_IDENTIFIER.Value()); }
            set { _AVPList.Add(AVPType.VEHICLE_IDENTIFIER.Value(), value); }
        }

        public decimal? TripDistance
        {
            set { _AVPList.Add(AVPType.TRIP_DISTANCE.Value(), value.ToString()); }
            get
            {
                if (_AVPList.Contains(AVPType.TRIP_DISTANCE.Value()))
                {
                    return decimal.Parse(_AVPList.GetValue(AVPType.TRIP_DISTANCE.Value()));
                }

                return null;
            }
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

        public string BookingNumber
        {
            get { return SerialNumber; }
            set { SerialNumber = value; }
        }

        public GeographicalCoordinates DepartureGeoLocation
        {
            get { return OriginInformation?.GeographicalCoordinates; }
            set
            {
                if (OriginInformation == null)
                {
                    OriginInformation = new LocationInformation();
                }
                OriginInformation.GeographicalCoordinates = value;
            }
        }

        public GeographicalCoordinates ArrivalGeoLocation
        {
            get { return DestinationInformation?.GeographicalCoordinates; }
            set
            {
                if (DestinationInformation == null)
                {
                    DestinationInformation = new LocationInformation();
                }
                DestinationInformation.GeographicalCoordinates = value;
            }
        }

        public void SetDepartureDetails(DateTime departureDate, GeographicalCoordinates geographicalCoordinates)
        {
            DespatchDate = departureDate;
            DepartureGeoLocation = geographicalCoordinates;
        }

        public void ArrivalDetails(DateTime arrivalDate, GeographicalCoordinates geographicalCoordinates)
        {
            ArrivalDate = arrivalDate;
            ArrivalGeoLocation = geographicalCoordinates;
        }
    }
}