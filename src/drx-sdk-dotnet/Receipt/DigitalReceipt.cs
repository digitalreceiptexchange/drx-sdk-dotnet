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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.Settlement;
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt
{

    public class DigitalReceipt
    {
        //@SerializedName("standardBusinessDocumentHeader")
        private StandardBusinessDocumentHeader _standardBusinessDocumentHeader;
        //@SerializedName("invoice")
        private Invoice.Invoice _invoice;
        //@SerializedName("paymentReceipts")
        private List<PaymentReceipt> _paymentReceipts;

        public DigitalReceipt(string digitalReceiptJson)
        {
            //JsonParser parser = new JsonParser();
            //JsonObject digitalReceiptObject = parser.parse(digitalReceiptJson).getAsJsonObject().getAsJsonObject("dRxDigitalReceipt");

            //Gson gson = new GsonBuilder()
            //        .registerTypeAdapter(Invoice.class, new InvoiceDeserializer())
            //        .registerTypeHierarchyAdapter(LineItem.class, new LineItemDeserializer())
            //        .registerTypeAdapter(new TypeToken<List<PaymentReceipt>>() {
            //        }.getType(), new PaymentReceiptDeserializer())
            //        .create();

            //this._standardBusinessDocumentHeader = gson.fromJson(digitalReceiptObject.get("standardBusinessDocumentHeader").toString(), StandardBusinessDocumentHeader.class);
            //this._invoice = gson.fromJson(digitalReceiptObject.get("invoice").toString(), Invoice.class);
            //this._paymentReceipts = gson.fromJson(digitalReceiptObject.get("paymentReceipts").toString(), new TypeToken<List<PaymentReceipt>>() {
            //}.getType());

            //if( _invoice == null
            //        || this._standardBusinessDocumentHeader.equals(null)
            //        || this._paymentReceipts.equals(null)){
            //    throw new ReceiptDeserializationException("Failed to deserialize Digital Receipt data from source");
            //}
        }
    
        public string UserGUID => _standardBusinessDocumentHeader.UserIdentifier;

        public string MerchantName => _invoice.MerchantName;
    
        public string GetCompanyTaxNumber(TaxCode taxCode)
        {
            return _invoice.GetCompanyTaxNumber(taxCode);
        }
    
        public string MerchantLocationReference => _standardBusinessDocumentHeader.MerchantGLN;
    
        public DateTime? ReceiptDate => _invoice.CreationDateTime;
    
        public string ReceiptReference =>_invoice.InvoiceIdentification?.EntityIdentification;
    
        public string PurchaseOrderNumber => _invoice.PurchaseOrder?.EntityIdentification;

        public string CustomerReferenceNumber => _invoice.CustomerReference?.EntityIdentification;
    
        public List<ReceiptContact> MerchantCustomerRelations => GetMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS);
    
        public List<ReceiptContact> MerchantDeliveryContact => GetMerchantContact(ReceiptContactType.DELIVERY_CONTACT);
    
        public List<ReceiptContact> MerchantSalesAssistant => GetMerchantContact(ReceiptContactType.SALES_ADMINISTRATION);
    
        private List<ReceiptContact> GetMerchantContact(ReceiptContactType receiptContactType)
        {
            List<ReceiptContact> contacts = new List<ReceiptContact>();
            foreach (DocumentOwner sender in _standardBusinessDocumentHeader.Sender)
            {
                if(sender.Identifier.Authority == "GS1")
                {
                    foreach (ReceiptContact contact in sender.DocumentOwnerContact)
                    {
                        if(contact.ReceiptContactType == receiptContactType)
                        {
                            contacts.Add(contact);
                        }
                    }
                }
            }
            return contacts;
        }
    
        public List<ReceiptContact> ClientRecipientContact => GetRMSContact(ReceiptContactType.RECIPIENT_CONTACT);
    
        public List<ReceiptContact> ClientPurchasingContact => GetRMSContact(ReceiptContactType.PURCHASING_CONTACT);
    
        private List<ReceiptContact> GetRMSContact(ReceiptContactType contactType)
        {
            List<ReceiptContact> contacts = new List<ReceiptContact>();
            foreach (DocumentOwner receiver in _standardBusinessDocumentHeader.Receiver)
            {
                if(receiver.Identifier.Authority == "dRx")
                {
                    if(receiver.DocumentOwnerContact == null)
                    {
                        return contacts;
                    }

                    foreach (ReceiptContact contact in receiver.DocumentOwnerContact) 
                    {
                        if(contact.ReceiptContactType == contactType)
                        {
                            contacts.Add(contact);
                        }
                    }
                }
            }
            return contacts;
        }

        public Net.Dreceiptx.Receipt.Common.Address DeliveryAddress => _invoice.DestinationInformation.Address;

        public List<Contact> CustomerDeliveryContactDetails => _invoice.DestinationInformation.Contacts;

        public Net.Dreceiptx.Receipt.Common.Address OriginAddress => _invoice.OriginInformation.Address;

        public List<Contact> OriginContact => _invoice.OriginInformation.Contacts;

        public GeographicalCoordinates OriginCoordinates => _invoice.OriginInformation.Address.GeographicalCoordinates;
    
        public GeographicalCoordinates DestinationCoordinates => _invoice.DestinationInformation.Address.GeographicalCoordinates;
    
        public List<LineItem.LineItem> LineItems => _invoice.InvoiceLineItems;
    
        public List<ReceiptAllowanceCharge> Charges => _invoice.AllowanceOrCharges.Where(x => x.IsCharge).ToList();

        public List<ReceiptAllowanceCharge> Allowances => _invoice.AllowanceOrCharges.Where(x => x.IsAllowance).ToList();
    
        public decimal Total => _invoice.Total;
    
        public decimal SubTotal => _invoice.SubTotal;
    
        public decimal GetTaxTotal(TaxCode taxCode) {
            return _invoice.TaxesTotalByTaxCode(taxCode);
        }

        public string SalesOrderReference
        {
            get { return _invoice.SalesOrderReference?.EntityIdentification; }
            set
            {
                if (_invoice.SalesOrderReference == null)
                {
                    _invoice.SalesOrderReference = new Identification();
                }
                _invoice.SalesOrderReference.EntityIdentification = value;
            }
        }
    }
}