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
using Net.Dreceiptx.Receipt.Validation;

namespace Net.Dreceiptx.Receipt.Document
{
    [DataContract]
    public class StandardBusinessDocumentHeader
    {
        public StandardBusinessDocumentHeader()
        {
            Sender = new List<DocumentOwner>();
            Receiver = new List<DocumentOwner>();
            DocumentIdentification = new DocumentIdentification();

            //DocumentOwner merchant = new DocumentOwner();
            //merchant.Identifier.Authority = "GS1";
            //merchant.Identifier.Value = null;
            //Sender.Add(merchant);

            //DocumentOwner dRx = new DocumentOwner();
            //dRx.Identifier.Authority = "GS1";
            //dRx.Identifier.Value = null;
            //Receiver.Add(dRx);

            //DocumentOwner user = new DocumentOwner();
            //user.Identifier.Authority = "dRx";
            //user.Identifier.Value = null;
            //Receiver.Add(user);
        }

        [DataMember]
        public List<DocumentOwner> Receiver { get; set; }

        [DataMember]
        public List<DocumentOwner> Sender { get; set; }

        public void AddReceiver(DocumentOwner receiver)
        {
            Receiver.Add(receiver);
        }

        [DataMember]
        public DocumentIdentification DocumentIdentification { get; set; }

        public DocumentOwner MerchantGLN
        {
            get
            {
                var merchant = Sender.Find(x => x.Identifier.Authority == "GS1");
                if (merchant == null)
                {
                    merchant = new DocumentOwner();
                    merchant.Identifier.Authority = "GS1";
                    Sender.Add(merchant);
                }
                return merchant;
            }
        }


        public DocumentOwner DrxFLN
        {
            get
            {
                var dRx = Receiver.Find(x => x.Identifier.Authority == "GS1");
                if (dRx == null)
                {
                    dRx = new DocumentOwner();
                    dRx.Identifier.Authority = "GS1";
                    Receiver.Add(dRx);
                }
                return dRx;
            }
        }

        public DocumentOwner UserIdentifier
        {
            get
            {
                var user = Receiver.Find(x => x.Identifier.Authority == "dRx");
                if (user == null)
                {
                    user = new DocumentOwner();
                    user.Identifier.Authority = "dRx";
                    Receiver.Add(user);
                }
                return user;
            }
        }


        public List<ReceiptContact> ClientContacts => Receiver[1].DocumentOwnerContact;

        public void AddMerchantContact(ReceiptContact contact)
        {
            Sender[0].AddDocumentOwnerContact(contact);
        }

        public void AddRMSContact(ReceiptContact contact)
        {
            Receiver[1].AddDocumentOwnerContact(contact);
        }

        public ReceiptValidation Validate(ReceiptValidation receiptValidation)
        {
            if (Sender.Count == 0)
            {
                receiptValidation.AddError(ValidationErrors.MerchantGLNMustBeSet);
            }

            return receiptValidation;
        }

    }
}