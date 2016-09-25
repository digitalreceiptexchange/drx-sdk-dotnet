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
using System.Xml.Serialization;

namespace Net.Dreceiptx.GS1.SDBH
{
    /// <summary>
    /// The UN/CEFACT standard, containing information about the routing and processing of the business 
    /// document. It also identifies the message set that is sent together with on SBDH and the 
    /// version number of the document(s) contained.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [DataContract]
    public class StandardBusinessDocumentHeader
    {
        public StandardBusinessDocumentHeader()
        {
            Sender = new List<Partner>();
            Receiver = new List<Partner>();
            DocumentIdentification = new DocumentIdentification();
        }

        //TODO: Check this. Why is it a collection?
        /// <summary>
        /// Gets and sets the sender of the message, party representing the 
        /// organization which created the standard business document
        /// </summary>
        [DataMember]
        public List<Partner> Sender { get; set; }
        
        //TODO: Why is this a collection?
        [DataMember]
        [XmlElement("Receiver")]
        public List<Partner> Receiver { get; set; }
        
        [DataMember]
        public DocumentIdentification DocumentIdentification { get; set; }
    }
}