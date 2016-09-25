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
namespace Net.Dreceiptx.Receipt.Merchant
{
    public class Merchant
    {
        private string _id;
        @SerializedName("industry")
        private string _industry;
        @SerializedName("sector")
        private string _sector;
        @SerializedName("fullName")
        private string _fullname;
        @SerializedName("commonName")
        private string _commonName;
        @SerializedName("businessTaxNumber")
        private string _businessTaxNumber;
        @SerializedName("businessRegistrationNumber")
        private string _businessRegistrationNumber;
        @SerializedName("primaryPhone")
        private string _primaryPhone;
        @SerializedName("primaryAddress")
        private MerchantAddress _primaryAddress;
        @SerializedName("contacts")
        private List<MerchantContact> _contacts;
        private transient string
        _merchantLocationHostname
        = "https://cdn.dreceiptx.net/merchant/location/";

        public string getIndustry()
        {
            return _industry;
        }

        public string getSector()
        {
            return _sector;
        }

        public string getLocationId()
        {
            return _id;
        }

        public string getFullName()
        {
            return _fullname;
        }

        public string getCommonName()
        {
            return _commonName;
        }

        public string getBusinessTaxNumber()
        {
            return _businessTaxNumber;
        }

        public string getBusinessRegistrationNumber()
        {
            return _businessRegistrationNumber;
        }

        public string getPrimaryPhone()
        {
            return _primaryPhone;
        }

        public MerchantAddress getPrimaryAddress()
        {
            return _primaryAddress;
        }

        public List<MerchantContact> getContacts()
        {
            return _contacts;
        }

        public string getMerchantLogoUrl()
        {
            return _merchantLocationHostname + _id + "/logo.jpg";
        }

        public void setMerchantLocationHostname(string merchantLocationHostname)
        {
            _merchantLocationHostname = merchantLocationHostname;
        }

    }
}