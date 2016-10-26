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
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Settlement;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt
{
    public class DigitalReceiptGenerator
    {
        //@SerializedName("standardBusinessDocumentHeader")
        private StandardBusinessDocumentHeader _standardBusinessDocumentHeader;
        //@SerializedName("invoice")
        private Invoice.Invoice _invoice;
        //@SerializedName("paymentReceipts")
        private List<PaymentReceipt> _paymentReceipts;

        private static int _paymentReceiptId = 1;
        private readonly IConfigManager _configManager;
        private IExchangeClient _exchangeClient;
        private string _dRxGLN;
        private string _merchantGLN;
        private string _userGUID;
        private Net.Dreceiptx.Receipt.Tax.TaxCategory _defaultTaxCategory;
        private Net.Dreceiptx.Receipt.Tax.TaxCode _defaultTaxCode;
        private Currency _defaultCurrency;
        private string _defaultLanguage;
        private string _defaultTimeZone;
        private string _defaultCountry;

        public DigitalReceiptGenerator(IConfigManager configManager)
        {
            _configManager = configManager;
            //_defaultCountry = validateConfigOption("default.country");
            //_defaultLanguage =  validateConfigOption("default.language");
            //_defaultTimeZone = validateConfigOption("default.timezone");
            //_defaultCurrency = net.dreceiptx.receipt.common.Currency.codeOf(validateConfigOption("default.currency"));
            //_defaultTaxCategory = Enum.valueOf(TaxCategory.class, validateConfigOption("default.taxCategory"));
            //_defaultTaxCode = Enum.valueOf(TaxCode.class, validateConfigOption("default.taxCode"));

            _standardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
            _standardBusinessDocumentHeader.DrxFLN = ValidateConfigOption("drx.gln");
            _standardBusinessDocumentHeader.MerchantGLN = ValidateConfigOption("merchant.gln");
            _standardBusinessDocumentHeader.DocumentIdentification.TypeVersion = ValidateConfigOption("receipt.version");
            _standardBusinessDocumentHeader.DocumentIdentification.CreationDateAndTime = DateTime.Now;

            _paymentReceipts = new List<PaymentReceipt>();
            _invoice = new Invoice.Invoice(_configManager);
        }
    
        public void SetMerchantGLN(string merchantGLN)
        {
            _standardBusinessDocumentHeader.MerchantGLN = merchantGLN;
        }
    
        public void SetUserGUID(UserIdentifierType userIdentifierType, string userIdentifierValue)
        {
            _userGUID = $"{userIdentifierType.Value()}:{userIdentifierValue}";
            _standardBusinessDocumentHeader.UserIdentifier = _userGUID;
        }
    
        public void SetMerchantReference(string merchantReference)
        {
            _standardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = merchantReference;
        
            if(_invoice.InvoiceIdentification == null){
                _invoice.InvoiceIdentification = merchantReference;
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
            _invoice.CreationDateTime = invoiceDate;
        }
    
        public void SetPurchaseOrderNumber(string purchaseOrder)
        {
            _invoice.PurchaseOrder = purchaseOrder;
        }

        public void SetCustomerReferenceNumber(string customerReference)
        {
            _invoice.CustomerReference = customerReference;
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
            _standardBusinessDocumentHeader.AddRMSContact(rmsContact);
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
            _standardBusinessDocumentHeader.AddMerchantContact(merchantContact);
        }
    
        public void SetReceiptNumber(string receiptNumber){
            _invoice.InvoiceIdentification = receiptNumber;
        
            if(_standardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier == null){
                _standardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = receiptNumber;
            }
        }

        public int addLineItem(LineItem.LineItem lineItem) {
            foreach (Tax.Tax lineItemTax in lineItem.Taxes) {
                ConfigureTax(lineItemTax);
            }
            return _invoice.AddLineItem(lineItem);
        }
    
        public int addLineItem(string brand, string name, int quantity, decimal price) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, "", quantity, price);
            return _invoice.AddLineItem(lineItem);
        }
    
        public int addLineItem(string brand, string name, int quantity, decimal price, Tax.Tax tax) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, "", quantity, price);
            lineItem.AddTax(ConfigureTax(tax));
            return _invoice.AddLineItem(lineItem);
        }

        public int addLineItem(string brand, string name, string description, int quantity, decimal price) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, description, quantity, price);
            return _invoice.AddLineItem(lineItem);
        }

        public int addLineItem(string brand, string name, string description, int quantity, decimal price, Tax.Tax tax) {
            LineItem.LineItem lineItem = new StandardLineItem(brand, name, description, quantity, price);
            lineItem.AddTax(ConfigureTax(tax)); 
            return _invoice.AddLineItem(lineItem);
        }

        public int addLineItem(string brand, string name, string description, int quantity, decimal price, decimal taxRate, bool taxInclusive) {
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

            return _invoice.AddLineItem(lineItem);
        }

        public int addPaymentReceipt(PaymentMethodType paymentMethodCode, decimal paymentAmount) {
            PaymentReceipt paymentReceipt = new PaymentReceipt(paymentMethodCode, paymentAmount);
            paymentReceipt.SettlementCurrency = _defaultCurrency;
            paymentReceipt.Id = Interlocked.Add(ref _paymentReceiptId, 1);
            _paymentReceipts.Add(paymentReceipt);
            return paymentReceipt.Id;
        }

        public int addPaymentReceipt(PaymentReceipt paymentReceipt) {
            paymentReceipt.Id = Interlocked.Add(ref _paymentReceiptId, 1);
            paymentReceipt.SettlementCurrency = _defaultCurrency;
            _paymentReceipts.Add(paymentReceipt);
            return paymentReceipt.Id;
        }

        public void removePaymentReceipt(int paymentId) {
            PaymentReceipt item = null;
            foreach (PaymentReceipt paymentReceipt in _paymentReceipts)
            {
                if (paymentReceipt.Id == paymentId) {
                    item = paymentReceipt;
                    break;
                }
            }
            if (item != null)
            {
                _paymentReceipts.Remove(item);
            }
        }

        public void AddGeneralDiscount(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(amount, description));
        }
    
        public void AddGeneralDiscount(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(amount, description, ConfigureTax(tax)));
        }

        public void AddTip(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.Tip(amount, description));
        }
    
        public void AddTip(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.Tip(amount, description, ConfigureTax(tax)));
        }

        public void AddPackagingFee(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.PackagingFee(amount, description));
        }
    
        public void AddPackagingFee(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.PackagingFee(amount, description, ConfigureTax(tax)));
        }

        public void AddDeliveryFee(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.DeliveryFee(amount, description));
        }
    
        public void AddDeliveryFee(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.DeliveryFee(amount, description, ConfigureTax(tax)));
        }

        public void AddFrieghtFee(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.FreightFee(amount, description));
        }
    
        public void AddFrieghtFee(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.FreightFee(amount, description, ConfigureTax(tax)));
        }

        public void AddProcessingFee(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.ProcessingFee(amount, description));
        }
    
        public void AddProcessingFee(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.ProcessingFee(amount, description, ConfigureTax(tax)));
        }
    
        public void AddBookingFee(decimal amount, string description) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(amount, description));
        }
    
        public void AddBookingFee(decimal amount, string description, Tax.Tax tax) {
            _invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(amount, description, ConfigureTax(tax)));
        }

        public void SetDeliveryInformation(DeliveryInformation deliveryInformation) {
            _invoice.DestinationInformation = deliveryInformation.LocationInformation;
            _invoice.AllowanceOrCharges.AddRange(deliveryInformation.DeliveryFees);
            _invoice.DespatchInformation = deliveryInformation.DespatchInformation;
        }

        public void SetDeliveryAddress(Address address) {
            _invoice.DestinationInformation.Address = address;
        }

        public void SetDeliveryAddress(Address address, Contact contact) {
            _invoice.DestinationInformation.Address = address;
            _invoice.DestinationInformation.AddContact(contact);
        }

        public void SetDestinationCoordinates(GeographicalCoordinates geographicalCoordinates) {
            _invoice.DestinationInformation.GeographicalCoordinates = geographicalCoordinates;
        }

        public void SetDeliveryDate(DateTime deliverDate) {
            _invoice.DespatchInformation.DeliveryDate = deliverDate;
        }

        public void SetOriginAddress(Address address) {
            _invoice.OriginInformation.Address = address;
        }

        public void SetOriginAddress(Address address, Contact contact) {
            _invoice.OriginInformation.Address = address;
            _invoice.OriginInformation.AddContact(contact);
        }

        public void SetOriginCoordinates(GeographicalCoordinates geographicalCoordinates) {
            _invoice.OriginInformation.GeographicalCoordinates = geographicalCoordinates;
        }

        public void Validate() {
            ReceiptValidation receiptValidation = new ReceiptValidation();
            _standardBusinessDocumentHeader.Validate(receiptValidation);
            _invoice.Validate(receiptValidation);
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

        public string encodeJson() {
            //    DigitalReceiptSerializer digitalReceiptMapper = new DigitalReceiptSerializer();
            //    digitalReceiptMapper.setInvoice(_invoice);
            //    digitalReceiptMapper.setStandardBusinessDocumentHeader(_standardBusinessDocumentHeader);
            //    digitalReceiptMapper.setPaymentReceipts(_paymentReceipts);
            //    Gson gson = new GsonBuilder()
            //            .setDateFormat("yyyy-MM-dd'T'HH:mm:ssZ")
            //            .registerTypeAdapter(Invoice.class, new InvoiceSerializer(_defaultCurrency))
            //            .registerTypeHierarchyAdapter(LineItem.class, new LineItemSerializer(_defaultCurrency))
            //            .registerTypeAdapter(new TypeToken<List<PaymentReceipt>>().getType(), new PaymentReceiptsSerializer(_dRxGLN, _merchantGLN, _userGUID))
            //            .create();
            //    string digitalReceiptJson = gson.toJson(digitalReceiptMapper);
            //    return digitalReceiptJson;
            return null;
        }

        private string ValidateConfigOption(string configParameter)
        {
            if(_configManager.Exists(configParameter)){
                return _configManager.GetConfigValue(configParameter);
            }else{
                throw new ExchangeClientException(101, "Required config parameter "+configParameter+" not supplied");
            }
        }
    }
}