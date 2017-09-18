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

namespace Net.Dreceiptx.Receipt.Merchant
{
    [DataContract]
    public class Merchant
    {
        //transient
        private string _merchantLocationHostname = "https://merchants.dreceiptx.net/location/";

        [DataMember]
        public string Industry { get; set; }

        [DataMember]
        public string Sector { get; set; }

        public string Id { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string CommonName { get; set; }

        [DataMember]
        public string BusinessTaxNumber { get; set; }

        [DataMember]
        public string BusinessTaxNumberType { get; set; }

        [DataMember]
        public string BusinessRegistrationNumber { get; set; }

        [DataMember]
        public string PrimaryPhone { get; set; }

        [DataMember]
        public MerchantAddress PrimaryAddress { get; set; }

        [DataMember]
        public List<MerchantContact> Contacts { get; set; }

        [DataMember]
        public MerchantStatus Status { get; set; }

        public string MerchantLogoUrl => $"{_merchantLocationHostname}{Id}/logo.jpg";

        //TODO: No getter?
        public void setMerchantLocationHostname(string merchantLocationHostname)
        {
            _merchantLocationHostname = merchantLocationHostname;
        }

    }

    public enum MerchantStatus
    {
        InActive,
        Active
    }
}