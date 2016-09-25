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
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    /// <summary>
    /// Name of the contact person or department for the sending Party.
    /// The element ontactInformation, although optional, SHOULD be used, if possible.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class ContactInformation
    {
        
        [DataMember]
        public string RepId { get; set; }
        
        [DataMember]
        public string RepName { get; set; }

        /// <summary>
        /// Gets and sets the Name of contact person or department.
        /// The element „Contact‟, although optional, SHOULD be used, if possible.
        /// Example: Delysha Burnet
        /// </summary>
        [DataMember]
        public string Contact { get; set; }

        /// <summary>
        /// Gets and sets the EmailAddress. The EmailAddress, although optional, SHOULD be used, if possible.
        /// </summary>
        [DataMember]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets and sets the FaxNumber. A number format agreed upon between the Sender
        ///  and Receiver SHOULD be used. Number format expressed using [RFC3966]. 
        /// The tel URI for Telephone Numbers MAY be used. 
        /// </summary>
        [DataMember]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets and sets the TelephoneNumber. A number format agreed upon between the Sender
        /// and Receiver SHOULD be used. Number format expressed using [RFC3966].
        /// The tel URI for Telephone Numbers‟ MAY be used. 
        /// </summary>
        [DataMember]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Gets and sets the ContactTypeIdentifier. Role of the identifier.
        /// Example: EDI co-ordinator
        /// </summary>
        [DataMember]
        public string ContactTypeIdentifier { get; set; }

    }
}