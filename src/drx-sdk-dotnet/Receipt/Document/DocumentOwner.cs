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

namespace Net.Dreceiptx.Receipt.Document
{
    public class DocumentOwner
    {
        //@SerializedName("contactInformation")
        private List<ReceiptContact> _contactInformation;

        public DocumentOwner()
        {
            Identifier = new DocumentOwnerIdentification();
        }

        //TODO: Not sure id we need this here. Should just do via the DocumentOwnderIdentification instance
        public string Value
        {
            get { return Identifier.Value; }
            set { Identifier.Value = value; }
        }


        //@SerializedName("identifier")
        public DocumentOwnerIdentification Identifier { get; set; }

        public void AddDocumentOwnerContact(ReceiptContact documentOwnerContact)
        {
            if (_contactInformation == null)
            {
                _contactInformation = new List<ReceiptContact>();
            }
            _contactInformation.Add(documentOwnerContact);
        }

        public List<ReceiptContact> DocumentOwnerContact => _contactInformation;
    }
}