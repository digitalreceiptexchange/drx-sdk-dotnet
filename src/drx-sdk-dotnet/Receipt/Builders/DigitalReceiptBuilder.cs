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
using System.Threading;
using Net.Dreceiptx.Client;
using Net.Dreceiptx.Client.Exceptions;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Serialization;
using Net.Dreceiptx.Receipt.Settlement;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.Receipt.Validation;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.Builders
{
    public class DigitalReceiptBuilder
    {
        private DigitalReceiptMessage _digitalReceiptMessage;

        private static int _paymentReceiptId = 1;
        private readonly IConfigManager _configManager;
        private IExchangeClient _exchangeClient;
        private string _dRxGLN;
        private string _merchantGLN;
        private string _userGUID;
        private TaxCategory _defaultTaxCategory;
        private TaxCode _defaultTaxCode;
        private Currency _defaultCurrency;
        private string _defaultLanguage;
        private string _defaultTimeZone;
        private string _defaultCountry;

        public DigitalReceiptBuilder(Location location)
        {
            
        }

        public DigitalReceiptBuilder(IConfigManager configManager)
        {
            _configManager = configManager;
            //Create data elements
            _digitalReceiptMessage = new DigitalReceiptMessage();
            _digitalReceiptMessage.DRxDigitalReceipt = new DRxDigitalReceipt();
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice = new Invoice.Invoice(_configManager);
            _digitalReceiptMessage.DRxDigitalReceipt.PaymentReceipts = new List<PaymentReceipt>();

            //Configure default values
            try
            {
                _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.DRxGLN.Value = ValidateConfigOption("drx.gln");
                _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.MerchantGLN.Value = ValidateConfigOption("merchant.gln");
                _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.DocumentIdentification.TypeVersion = ValidateConfigOption("receipt.version");
                _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.DocumentIdentification.CreationDateAndTime = DateTime.Now;
                _defaultCurrency = CurrencyManager.GetCurrency(SetConfigOption("default.currency", "AUD"));
                _digitalReceiptMessage.DRxDigitalReceipt.Invoice.InvoiceCurrencyCode = _defaultCurrency.Value();
                _defaultCountry = SetConfigOption("default.country", "AUS");
                _digitalReceiptMessage.DRxDigitalReceipt.Invoice.CountryOfSupplyOfGoods = _defaultCountry;
                _defaultLanguage = SetConfigOption("default.language", "ENG");
                _defaultTimeZone = SetConfigOption("default.timezone", "AEDT");
                _defaultTaxCategory = TaxCategoryManager.GetTaxCategory(SetConfigOption("default.taxCategory", TaxCategory.APPLICABLE.Value()));
                _defaultTaxCode = TaxCodeManager.GetTaxCode(SetConfigOption("default.taxCode", TaxCode.GoodsAndServicesTax.Value()));
            }
            catch (Exception e)
            {
                throw new DRXRuntimeException("Failed to create DigitalReceiptBuilder due to configuration issue", e);
            }
        }

        public bool DryRunReceipt { get; set; }

        public void SetMerchantGLN(string merchantGLN)
        {
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.MerchantGLN.Value = merchantGLN;
        }
    
        public void SetUserGUID(UserIdentifierType userIdentifierType, string userIdentifierValue)
        {
            _userGUID = $"{userIdentifierType.Value()}:{userIdentifierValue}";
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.UserIdentifier.Value = _userGUID;
        }
    
        public void SetMerchantReference(string merchantReference)
        {
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = merchantReference;
        
            if(_digitalReceiptMessage.DRxDigitalReceipt.Invoice.InvoiceIdentification == null){
                _digitalReceiptMessage.DRxDigitalReceipt.Invoice.InvoiceIdentification = new Identification(merchantReference);
            }
        }

        public void SetReceiptCurrency(Currency currency)
        {
            _defaultCurrency = currency;
        }

        public void SetReceiptLanguage(string languageCode)
        {
            _defaultLanguage = languageCode;
        }
    
        public void SetReceiptDateTime(DateTime invoiceDate)
        {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.CreationDateTime = invoiceDate;
        }
    
        public void SetPurchaseOrderNumber(string purchaseOrder)
        {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.PurchaseOrder = new Identification(purchaseOrder);
        }

        public void SetCustomerReferenceNumber(string customerReference)
        {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.CustomerReference = new Identification(customerReference);
        }
    
        public void AddClientRecipientContact(string name, string email, string phone)
        {
            AddRMSContact(ReceiptContactType.RECIPIENT_CONTACT, name, email, phone);
        }
    
        public void AddClientRecipientContact(string name, string email)
        {
            AddRMSContact(ReceiptContactType.RECIPIENT_CONTACT, name, email, null);
        }
    
        public void AddClientRecipientContact(string name)
        {
            AddRMSContact(ReceiptContactType.RECIPIENT_CONTACT, name, null, null);
        }
    
        public void AddClientPurchasingContact(string name, string email, string phone)
        {
            AddRMSContact(ReceiptContactType.PURCHASING_CONTACT, name, email, phone);
        }
    
        public void AddClientPurchasingContact(string name, string email)
        {
            AddRMSContact(ReceiptContactType.PURCHASING_CONTACT, name, email, null);
        }
    
        public void AddClientPurchasingContact(string name)
        {
            AddRMSContact(ReceiptContactType.PURCHASING_CONTACT, name, null, null);
        }
    
        private void AddRMSContact(ReceiptContactType type, string name, string email, string phone){
            ReceiptContact rmsContact = new ReceiptContact(type, name);
            if (email != null)
            {
                rmsContact.AddEmailAddress(email);
            }
            if (phone != null)
            {
                rmsContact.AddTelephoneNumber(phone);
            }
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.AddRMSContact(rmsContact);
        }
    
        public void AddMerchantCustomerRelationsContact(string name, string email, string phone){
            AddMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS, name, email, phone);
        }
    
        public void AddMerchantCustomerRelationsContact(string name, string email){
            AddMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS, name, email, null);
        }
    
        public void AddMerchantCustomerRelationsContact(string name){
            AddMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS, name, null, null);
        }
    
        public void AddMerchantDeliveryContact(string name, string email, string phone){
            AddMerchantContact(ReceiptContactType.DELIVERY_CONTACT, name, email, phone);
        }
    
        public void AddMerchantDeliveryContact(string name, string email){
            AddMerchantContact(ReceiptContactType.DELIVERY_CONTACT, name, email, null);
        }
    
        public void AddMerchantDeliveryContact(string name){
            AddMerchantContact(ReceiptContactType.DELIVERY_CONTACT, name, null, null);
        }
    
        public void AddMerchantSalesAssistantContact(string name, string email, string phone){
            AddMerchantContact(ReceiptContactType.SALES_ADMINISTRATION, name, email, phone);
        }
    
        public void AddMerchantSalesAssistantContact(string name, string email){
            AddMerchantContact(ReceiptContactType.SALES_ADMINISTRATION, name, email, null);
        }
    
        public void AddMerchantSalesAssistantContact(string name){
            AddMerchantContact(ReceiptContactType.SALES_ADMINISTRATION, name, null, null);
        }
    
        private void AddMerchantContact(ReceiptContactType type, string name, string email, string phone){
            ReceiptContact merchantContact = new ReceiptContact(type, name);
            if(email != null){merchantContact.AddEmailAddress(email);}
            if(phone != null){merchantContact.AddTelephoneNumber(phone);}
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.AddMerchantContact(merchantContact);
        }
    
        public void SetReceiptNumber(string receiptNumber){
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.InvoiceIdentification = new Identification(receiptNumber);
        
            if(_digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier == null){
                _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = receiptNumber;
            }
        }

        public int AddLineItem(LineItem.LineItem lineItem) {
            foreach (Tax.Tax lineItemTax in lineItem.Taxes) {
                ConfigureTax(lineItemTax);
            }
            return _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddLineItem(lineItem);
        }
    
        public int AddLineItem(string brand, string name, int quantity, decimal price) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, "", quantity, price);
            return _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddLineItem(lineItem);
        }
    
        public int AddLineItem(string brand, string name, int quantity, decimal price, Tax.Tax tax) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, "", quantity, price);
            lineItem.AddTax(ConfigureTax(tax));
            return _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddLineItem(lineItem);
        }

        public int AddLineItem(string brand, string name, string description, int quantity, decimal price) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, description, quantity, price);
            return _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddLineItem(lineItem);
        }

        public int AddLineItem(string brand, string name, string description, int quantity, decimal price, Tax.Tax tax) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, description, quantity, price);
            lineItem.AddTax(ConfigureTax(tax)); 
            return _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddLineItem(lineItem);
        }

        public int AddLineItem(string brand, string name, string description, int quantity, decimal price, decimal taxRate, bool taxInclusive) {
            LineItem.LineItem lineItem;

            if (taxInclusive)
            {
                decimal netPrice = price * (1 - taxRate);
                decimal total = quantity * netPrice;
                Tax.Tax tax = new Tax.Tax(_defaultTaxCategory, _defaultTaxCode, total, taxRate);
                lineItem = new StandardLineItem(brand, name, description, quantity, price);
                lineItem.AddTax(tax);
            } else {
                decimal netPrice = price;
                decimal total = quantity * netPrice;
                Tax.Tax tax = new Tax.Tax(_defaultTaxCategory, _defaultTaxCode, total, taxRate);
                lineItem = new StandardLineItem(brand, name, description, quantity, price);
                lineItem.AddTax(tax);
            }

            return _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddLineItem(lineItem);
        }

        public int AddPaymentReceipt(PaymentMethodType paymentMethodCode, decimal paymentAmount) {
            PaymentReceipt paymentReceipt = new PaymentReceipt(paymentMethodCode, paymentAmount);
            paymentReceipt.SettlementCurrency = _defaultCurrency;
            paymentReceipt.Id = Interlocked.Add(ref _paymentReceiptId, 1);
            _digitalReceiptMessage.DRxDigitalReceipt.PaymentReceipts.Add(paymentReceipt);
            return paymentReceipt.Id;
        }

        public int AddPaymentReceipt(PaymentReceipt paymentReceipt) {
            paymentReceipt.Id = Interlocked.Add(ref _paymentReceiptId, 1);
            paymentReceipt.SettlementCurrency = _defaultCurrency;
            _digitalReceiptMessage.DRxDigitalReceipt.PaymentReceipts.Add(paymentReceipt);
            return paymentReceipt.Id;
        }

        public void RemovePaymentReceipt(int paymentId) {
            PaymentReceipt item = null;
            foreach (PaymentReceipt paymentReceipt in _digitalReceiptMessage.DRxDigitalReceipt.PaymentReceipts)
            {
                if (paymentReceipt.Id == paymentId) {
                    item = paymentReceipt;
                    break;
                }
            }
            if (item != null)
            {
                _digitalReceiptMessage.DRxDigitalReceipt.PaymentReceipts.Remove(item);
            }
        }

        public void AddGeneralDiscount(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(amount, description));
        }
    
        public void AddGeneralDiscount(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(amount, description, ConfigureTax(tax)));
        }

        public void AddTip(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.Tip(amount, description));
        }
    
        public void AddTip(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.Tip(amount, description, ConfigureTax(tax)));
        }

        public void AddPackagingFee(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.PackagingFee(amount, description));
        }
    
        public void AddPackagingFee(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.PackagingFee(amount, description, ConfigureTax(tax)));
        }

        public void AddDeliveryFee(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.DeliveryFee(amount, description));
        }
    
        public void AddDeliveryFee(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.DeliveryFee(amount, description, ConfigureTax(tax)));
        }

        public void AddFrieghtFee(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.FreightFee(amount, description));
        }
    
        public void AddFrieghtFee(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.FreightFee(amount, description, ConfigureTax(tax)));
        }

        public void AddProcessingFee(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.ProcessingFee(amount, description));
        }
    
        public void AddProcessingFee(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.ProcessingFee(amount, description, ConfigureTax(tax)));
        }
    
        public void AddBookingFee(decimal amount, string description) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(amount, description));
        }
    
        public void AddBookingFee(decimal amount, string description, Tax.Tax tax) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(amount, description, ConfigureTax(tax)));
        }

        public void SetDeliveryInformation(DeliveryInformation deliveryInformation) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DestinationInformation = deliveryInformation.LocationInformation;
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AllowanceOrCharges.AddRange(deliveryInformation.DeliveryFees);
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DespatchInformation = deliveryInformation.DespatchInformation;
        }

        public void SetDeliveryAddress(Address address) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DestinationInformation.Address = address;
        }

        public void SetDeliveryAddress(Address address, Contact contact) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DestinationInformation.Address = address;
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DestinationInformation.AddContact(contact);
        }

        public void SetDestinationCoordinates(GeographicalCoordinates geographicalCoordinates) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DestinationInformation.GeographicalCoordinates = geographicalCoordinates;
        }

        public void SetDeliveryDate(DateTime deliverDate) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.DespatchInformation.DeliveryDate = deliverDate;
        }

        public void SetOriginAddress(Address address) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.OriginInformation.Address = address;
        }

        public void SetOriginAddress(Address address, Contact contact) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.OriginInformation.Address = address;
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.OriginInformation.AddContact(contact);
        }

        public void SetOriginCoordinates(GeographicalCoordinates geographicalCoordinates) {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.OriginInformation.GeographicalCoordinates = geographicalCoordinates;
        }

        public void Validate() {
            ReceiptValidation receiptValidation = new ReceiptValidation();
            _digitalReceiptMessage.DRxDigitalReceipt.StandardBusinessDocumentHeader.Validate(receiptValidation);
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.Validate(receiptValidation);
        }
    
        private Tax.Tax ConfigureTax(Tax.Tax tax){
            if(tax.TaxCategory == null){
                tax.TaxCategory = _defaultTaxCategory;
            }
        
            if(tax.TaxCode== null){
                tax.TaxCode = _defaultTaxCode;
            }
            return tax;
        }

        public DigitalReceiptMessage GetDigitalReceiptMessage() {
            return _digitalReceiptMessage;
        }

        private string ValidateConfigOption(string configParameter)
        {
            if(_configManager.Exists(configParameter)){
                return _configManager.GetConfigValue(configParameter);
            }else{
                throw new ExchangeClientException(101, "Required config parameter "+configParameter+" not supplied");
            }
        }

        private string SetConfigOption(string configParameter, string defaultValue)
        {
            if (_configManager.Exists(configParameter))
            {
                return _configManager.GetConfigValue(configParameter);
            }
            else
            {
                return defaultValue;
            }
        }

        public void AddAdminFee(decimal amount, string description)
        {
            _digitalReceiptMessage.DRxDigitalReceipt.Invoice.AddAllowanceOrCharge(
                ReceiptAllowanceCharge.AdminFee(amount, description));
        }

        public DigitalReceiptMessage BuildReceiptPostRequest()
        {
            //TODO:
            return null;
        }
    }
}