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

namespace Net.Dreceiptx.Receipt.Merchant
{
    public class Merchant
    {
        //transient
        private string _merchantLocationHostname = "https://cdn.dreceiptx.net/merchant/location/";
        //@SerializedName("industry")
        public string Industry { get; }
        //@SerializedName("sector")
        public string Sector { get; }

        public string LocationId { get; }

        //@SerializedName("fullName")
        public string FullName { get; }
        //@SerializedName("commonName")
        public string CommonName { get; }
        //@SerializedName("businessTaxNumber")
        public string BusinessTaxNumber { get; }
        //@SerializedName("businessRegistrationNumber")
        public string BusinessRegistrationNumber { get; }
        //@SerializedName("primaryPhone")
        public string PrimaryPhone { get; }
        //@SerializedName("primaryAddress")
        public MerchantAddress PrimaryAddress { get; }
        //@SerializedName("contacts")
        public List<MerchantContact> Contacts { get; }

        public string MerchantLogoUrl => _merchantLocationHostname + LocationId + "/logo.jpg";

        //TODO: No getter?
        public void setMerchantLocationHostname(string merchantLocationHostname)
        {
            _merchantLocationHostname = merchantLocationHostname;
        }

    }
}