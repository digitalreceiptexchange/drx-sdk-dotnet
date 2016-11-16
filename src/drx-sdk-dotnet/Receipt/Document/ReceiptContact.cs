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
using System.Linq;
using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.Common;

namespace Net.Dreceiptx.Receipt.Document
{
    [DataContract]
    public class ReceiptContact
    {
        public ReceiptContact()
        { }
        public ReceiptContact(ReceiptContactType receiptContactType)
        {
            Contacts = new List<Contact>();
            ReceiptContactType = receiptContactType;
        }

        public ReceiptContact(ReceiptContactType receiptContactType, string contactName)
            : this(receiptContactType)
        {
            Contact = contactName;
        }

        /// <summary>
        /// Gets and sets the ContactTypeIdentifier. Role of the identifier.
        /// Example: EDI co-ordinator
        /// </summary>
        [DataMember(Name = "ContactTypeCode")]
        public ReceiptContactType ReceiptContactType { get; set; }

        //TODO: Why not just make if PersonName?
        /// <summary>
        /// Gets and sets the the Name of contact person or department.
        /// </summary>
        [DataMember(Name = "PersonName")]
        public string Contact { get; set; }

        [DataMember(Name = "CommunicationChannelCode")]
        public List<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets the EmailAddress.The EmailAddress, although optional, SHOULD be used, if possible.
        /// </summary>
        public string EmailAddress => Contacts.FirstOrDefault(x => x.Type == Common.ContactType.EMAIL)?.ContactValue;

        public void AddEmailAddress(string emailAddress)
        {
            // TODO: If we add multiple emails then the getEmail falls down. Should we replace
            // this add email with just setEmailAddress otherwise the getEMailAddress should change
            Contacts.Add(new Contact(ContactType.EMAIL, emailAddress));
        }

        /// <summary>
        /// Gets the TelephoneNumber.A number format agreed upon between the Sender
        /// and Receiver SHOULD be used.Number format expressed using [RFC3966].
        /// The tel URI for Telephone Numbers? MAY be used.
        /// </summary>
        public string TelephoneNumber => Contacts.FirstOrDefault(x => x.Type == Common.ContactType.TELEPHONE)?.ContactValue;

        /// <summary>
        ///  Sets the TelephoneNumber.A number format agreed upon between the Sender
        /// and Receiver SHOULD be used.Number format expressed using [RFC3966].
        /// The tel URI for Telephone Numbers? MAY be used.
        /// </summary>
        /// <param name="telephoneNumber"></param>
        public void AddTelephoneNumber(string telephoneNumber)
        {
            // TODO: If we add multiple telephones then the getEmail falls down. Should we replace
            // this add email with just setEmailAddress otherwise the getEMailAddress should change
            Contacts.Add(new Contact(ContactType.TELEPHONE, telephoneNumber));
        }
    }
}