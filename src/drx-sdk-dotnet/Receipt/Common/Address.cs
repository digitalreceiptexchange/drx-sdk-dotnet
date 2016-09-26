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
namespace Net.Dreceiptx.Receipt.Common
{
    public class Address
    {
        //@SerializedName("name")
        public string Name { get; set; }
        //@SerializedName("streetAddressOne")
        public string StreetAddress1 { get; set; }
        //@SerializedName("streetAddressTwo")
        public string StreetAddress2 { get; set; }
        //@SerializedName("streetAddressThree")
        public string StreetAddress3 { get; set; }
        //@SerializedName("city")
        public string City { get; set; }
        //@SerializedName("postalCode")
        public string PostalCode { get; set; }
        //@SerializedName("state")
        public string State { get; set; }
        //@SerializedName("countryCode")
        public string CountryCode { get; set; }
        //@SerializedName("geographicalCoordinates")
        public GeographicalCoordinates GeographicalCoordinates { get; set; }

        public Address()
        {
        }

        public Address(string streetAddress1, string city, string postalCode, string state, string countryCode)
        {
            StreetAddress1 = streetAddress1;
            City = city;
            PostalCode = postalCode;
            State = state;
            CountryCode = countryCode;
        }

        public Address(GeographicalCoordinates geographicalCoordinates)
        {
            GeographicalCoordinates = geographicalCoordinates;
        }

        public Address(string name, string streetAddress1, string streetAddress2, string streetAddress3,
            string city, string postalCode, string state, string countryCode)
        {
            Name = name;
            StreetAddress1 = streetAddress1;
            StreetAddress2 = streetAddress2;
            StreetAddress3 = streetAddress3;
            City = city;
            PostalCode = postalCode;
            State = state;
            CountryCode = countryCode;
        }
    }
}