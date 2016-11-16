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
using System.Collections.Generic;
using Net.Dreceiptx.Receipt.AllowanceCharge;

namespace Net.Dreceiptx.Receipt.Common
{
    public class DeliveryInformation
    {
        private readonly LocationInformation _locationInformation;
        private List<ReceiptAllowanceCharge> _deliveryFees;
        private readonly DespatchInformation _despatchInformation;

        public DeliveryInformation()
        {
            _locationInformation = new LocationInformation();
            _despatchInformation = new DespatchInformation();
            _deliveryFees = new List<ReceiptAllowanceCharge>();
        }

        public DeliveryInformation Name(string deliveryName)
        {
            _locationInformation.Address.Name = deliveryName;
            return this;
        }

        public DeliveryInformation Address(string streetAddress1, string city, string postalCode, string state,
            string countryCode)
        {
            _locationInformation.Address.StreetAddress1 = streetAddress1;
            _locationInformation.Address.City = city;
            _locationInformation.Address.PostalCode = postalCode;
            _locationInformation.Address.State = state;
            _locationInformation.Address.CountryCode = countryCode;
            return this;
        }

        public DeliveryInformation StreetAddress2(string streetAddress2)
        {
            _locationInformation.Address.StreetAddress2 = streetAddress2;
            return this;
        }

        public DeliveryInformation StreetAddress3(string streetAddress3)
        {
            _locationInformation.Address.StreetAddress3 = streetAddress3;
            return this;
        }

        public DeliveryInformation AddDeliveryFee(decimal deliveryFee, string description)
        {
            _deliveryFees.Add(ReceiptAllowanceCharge.DeliveryFee(deliveryFee, description));
            return this;
        }

        public DeliveryInformation AddDeliveryFee(decimal deliveryFee, string description, Tax.Tax tax)
        {
            _deliveryFees.Add(ReceiptAllowanceCharge.DeliveryFee(deliveryFee, description, tax));
            return this;
        }

        public DeliveryInformation DeliveryDate(DateTime deliveryDate)
        {
            _despatchInformation.DeliveryDate = deliveryDate;
            return this;
        }

        public DeliveryInformation DeliveryInstructions(string instructions)
        {
            _despatchInformation.DeliveryInstructions = instructions;
            return this;
        }

        public DeliveryInformation DespatchDate(DateTime despatchDate)
        {
            _despatchInformation.DespatchDateTime = despatchDate;
            return this;
        }

        public DeliveryInformation AddContact(ContactType type, string value)
        {
            _locationInformation.Contacts.Add(new Contact(type, value));
            return this;
        }

        public LocationInformation LocationInformation => _locationInformation;

        public List<ReceiptAllowanceCharge> DeliveryFees => _deliveryFees;

        public DespatchInformation DespatchInformation => _despatchInformation;
    }
}