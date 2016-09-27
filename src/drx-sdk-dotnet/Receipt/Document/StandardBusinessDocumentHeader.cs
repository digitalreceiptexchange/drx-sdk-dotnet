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
using Net.Dreceiptx.Receipt.Validation;

namespace Net.Dreceiptx.Receipt.Document
{
    public class StandardBusinessDocumentHeader
    {
        //@SerializedName("sender")
        private List<DocumentOwner> _sender;
        //@SerializedName("receiver")
        private List<DocumentOwner> _receiver;
        
        private DocumentOwner _merchant;
        private DocumentOwner _dRx;
        private DocumentOwner _user;

        public StandardBusinessDocumentHeader()
        {
            _sender = new List<DocumentOwner>();
            _receiver = new List<DocumentOwner>();
            DocumentIdentification = new DocumentIdentification();

            _merchant = new DocumentOwner();
            _merchant.Identifier.Authority = "GS1";
            _merchant.Identifier.Value = null;
            _sender.Add(_merchant);

            _dRx = new DocumentOwner();
            _dRx.Identifier.Authority = "GS1";
            _dRx.Identifier.Value = null;
            _receiver.Add(_dRx);

            _user = new DocumentOwner();
            _user.Identifier.Authority = "dRx";
            _user.Identifier.Value = null;
            _receiver.Add(_user);
        }

        public string MerchantGLN
        {
            get { return _merchant.Value; }
            set { _merchant.Value = value ; }
        }

        public string DrxFLN
        {
            get { return _dRx.Value; }
            set { _dRx.Value = value; }
        }

        public string UserIdentifier
        {
            get { return _user.Value; }
            set { _user.Value = value; }
        }


        public List<ReceiptContact> ClientContacts => _user.DocumentOwnerContact;

        public void AddMerchantContact(ReceiptContact contact)
        {
            _merchant.AddDocumentOwnerContact(contact);
        }

        public void AddRMSContact(ReceiptContact contact)
        {
            _user.AddDocumentOwnerContact(contact);
        }

        public List<DocumentOwner> Receiver => _receiver;

        public List<DocumentOwner> Sender => _sender;

        public void AddReceiver(DocumentOwner receiver)
        {
            _receiver.Add(receiver);
        }

        //@SerializedName("documentIdentification")
        public DocumentIdentification DocumentIdentification { get; set; }

        public ReceiptValidation Validate(ReceiptValidation receiptValidation)
        {
            if (_sender.Count == 0)
            {
                receiptValidation.AddError(ValidationErrors.MerchantGLNMustBeSet);
            }

            return receiptValidation;

        }
    }
}