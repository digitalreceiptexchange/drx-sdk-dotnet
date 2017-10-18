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
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.Serialization;
using Net.Dreceiptx.Receipt.Serialization.Json;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.Receipt.Settlement;

namespace Net.Dreceiptx.Receipt
{

    public class DigitalReceipt
    {
        private DRxDigitalReceipt _digitalReceipt;
        private string _sourceData;

        public DigitalReceipt()
        {
            _digitalReceipt = new DRxDigitalReceipt();
        }

        public static DigitalReceipt FromJson(string digitalReceiptJson)
        {
            DigitalReceipt receipt = new DigitalReceipt();
            DigitalReceiptMessage _digitalReceiptMessage = JsonSerializer.Deserialize<DigitalReceiptMessage>(digitalReceiptJson);
            receipt._digitalReceipt =  _digitalReceiptMessage.DRxDigitalReceipt;
            return receipt;
        }
    
        public string UserGUID => _digitalReceipt.StandardBusinessDocumentHeader.UserIdentifier.Value;

        //public string MerchantName => _invoice.MerchantName;
    
        public string GetCompanyTaxNumber(TaxCode taxCode)
        {
            return _digitalReceipt.Invoice.GetCompanyTaxNumber(taxCode);
        }
    
        public string MerchantLocationReference => _digitalReceipt.StandardBusinessDocumentHeader.MerchantGLN.Value;
    
        public DateTime? ReceiptDate => _digitalReceipt.Invoice.CreationDateTime;
    
        public string ReceiptReference => _digitalReceipt.Invoice.InvoiceIdentification?.EntityIdentification;
    
        public string PurchaseOrderNumber => _digitalReceipt.Invoice.PurchaseOrder?.EntityIdentification;

        public string CustomerReferenceNumber => _digitalReceipt.Invoice.CustomerReference?.EntityIdentification;
    
        public List<ReceiptContact> MerchantCustomerRelations => GetMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS);
    
        public List<ReceiptContact> MerchantDeliveryContact => GetMerchantContact(ReceiptContactType.DELIVERY_CONTACT);
    
        public List<ReceiptContact> MerchantSalesAssistant => GetMerchantContact(ReceiptContactType.SALES_ADMINISTRATION);
    
        private List<ReceiptContact> GetMerchantContact(ReceiptContactType receiptContactType)
        {
            List<ReceiptContact> contacts = new List<ReceiptContact>();
            foreach (DocumentOwner sender in _digitalReceipt.StandardBusinessDocumentHeader.Sender)
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
            foreach (DocumentOwner receiver in _digitalReceipt.StandardBusinessDocumentHeader.Receiver)
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

        public Net.Dreceiptx.Receipt.Common.Address DeliveryAddress => _digitalReceipt.Invoice.DestinationInformation.Address;

        public List<Contact> CustomerDeliveryContactDetails => _digitalReceipt.Invoice.DestinationInformation.Contacts;

        public Net.Dreceiptx.Receipt.Common.Address OriginAddress => _digitalReceipt.Invoice.OriginInformation.Address;

        public List<Contact> OriginContact => _digitalReceipt.Invoice.OriginInformation.Contacts;

        public GeographicalCoordinates OriginCoordinates => _digitalReceipt.Invoice.OriginInformation.Address.GeographicalCoordinates;
    
        public GeographicalCoordinates DestinationCoordinates => _digitalReceipt.Invoice.DestinationInformation.Address.GeographicalCoordinates;
    
        public List<LineItem.LineItem> LineItems => _digitalReceipt.Invoice.InvoiceLineItems;
    
        public List<ReceiptAllowanceCharge> Charges => _digitalReceipt.Invoice.AllowanceOrCharges.Where(x => x.IsCharge).ToList();

        public List<ReceiptAllowanceCharge> Allowances => _digitalReceipt.Invoice.AllowanceOrCharges.Where(x => x.IsAllowance).ToList();

        public Currency Currency => CurrencyManager.GetCurrency(_digitalReceipt.Invoice.InvoiceCurrencyCode);

        public decimal Total => _digitalReceipt.Invoice.Total;

        public decimal NetTotal => _digitalReceipt.Invoice.NetTotal;

        public decimal SubTotal => _digitalReceipt.Invoice.GetSubTotal();

        public decimal TotalTax => _digitalReceipt.Invoice.TaxesTotal;

        public decimal GetTaxTotal(TaxCode taxCode) {
            return _digitalReceipt.Invoice.TaxesTotalByTaxCode(taxCode);
        }

        public decimal AllowancesTotal => _digitalReceipt.Invoice.SubTotalAllowances;

        public decimal ChargesTotal => _digitalReceipt.Invoice.SubTotalCharges;

        public string SalesOrderReference
        {
            get => _digitalReceipt.Invoice.SalesOrderReference?.EntityIdentification;
            set
            {
                if (_digitalReceipt.Invoice.SalesOrderReference == null)
                {
                    _digitalReceipt.Invoice.SalesOrderReference = new Identification();
                }
                _digitalReceipt.Invoice.SalesOrderReference.EntityIdentification = value;
            }
        }

        public List<PaymentReceipt> PaymentReceipts => _digitalReceipt.PaymentReceipts;

        public string SourceData => _sourceData;
        public string MerchantName => _digitalReceipt.Invoice.MerchantName;

        public string ToJson()
        {
            DigitalReceiptMessage message = new DigitalReceiptMessage();
            message.DRxDigitalReceipt = _digitalReceipt;
            return JsonSerializer.SerializeToString(message);
        }
    }
}