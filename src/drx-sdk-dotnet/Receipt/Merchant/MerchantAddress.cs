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

using System.Runtime.Serialization;
using System.Text;

namespace Net.Dreceiptx.Receipt.Merchant
{
    public class MerchantAddress
    {
        [DataMember]
        public string Buildingnumber { get; set; }

        [DataMember]
        public string Streetnumber { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string Street1 { get; set; }

        [DataMember]
        public string Street2 { get; set; }

        [DataMember]
        public string Street3 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Postcode { get; set; }

        [DataMember]
        public string Country { get; set; }

        public string ToFriendlyAddressLine()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Buildingnumber))
            {
                sb.Append(Buildingnumber).Append(" ");
            }
            if (!string.IsNullOrWhiteSpace(Streetnumber))
            {
                sb.Append(Streetnumber).Append(" ");
            }
            if (!string.IsNullOrWhiteSpace(Street))
            {
                sb.Append(Street).Append(" ");
            }
            return sb.ToString();
        }
    }
}