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

namespace Net.Dreceiptx.Receipt.Document
{
    [DataContract]
    public class DocumentOwner
    {
        public DocumentOwner()
        {
            Identifier = new DocumentOwnerIdentification();
        }

        [DataMember]
        public DocumentOwnerIdentification Identifier { get; set; }

        [DataMember(Name = "ContactInformation")]
        public List<ReceiptContact> DocumentOwnerContact { get; set; }

        //TODO: Not sure id we need this here. Should just do via the DocumentOwnderIdentification instance
        public string Value
        {
            get { return Identifier.Value; }
            set { Identifier.Value = value; }
        }

        public void AddDocumentOwnerContact(ReceiptContact documentOwnerContact)
        {
            if (DocumentOwnerContact == null)
            {
                DocumentOwnerContact = new List<ReceiptContact>();
            }
            DocumentOwnerContact.Add(documentOwnerContact);
        }


    }
}