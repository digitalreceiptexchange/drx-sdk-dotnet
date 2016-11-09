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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.Common
{
    [DataContract]
    public class LocationInformation
    {
        public LocationInformation()
        {
        }
        
        public LocationInformation(Address address, Contact contact)
        {
            Address = address;
            AddContact(contact);
        }

        public LocationInformation(Address address)
        {
            Address = address;
        }

        public LocationInformation(GeographicalCoordinates geographicalCoordinates)
        {
            GeographicalCoordinates = geographicalCoordinates;
        }

        public LocationInformation(Contact contact)
        {
            AddContact(contact);
        }

        public void AddContact(Contact contact)
        {
            if (Contacts == null)
            {
                Contacts = new List<Contact>();
            }
            Contacts.Add(contact);
        }

        public GeographicalCoordinates GeographicalCoordinates
        {
            get { return Address?.GeographicalCoordinates; }
            set
            {
                if (Address != null)
                {
                    Address.GeographicalCoordinates = value;
                }
                else
                {
                    Address = new Address(value);
                }
            }
        }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public List<Contact> Contacts { get; set; }

        //TODO: Remove
        public bool gsonValidator()
        {
            return Address != null || (Contacts != null && Contacts.Count!= 0);

        }
    }
}