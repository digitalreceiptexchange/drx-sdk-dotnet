/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Net.Dreceiptx.GS1.SDBH;

namespace Net.Dreceiptx.Receipt
{
    public class DigitalReceiptGenerator
    {
    //@SerializedName("standardBusinessDocumentHeader")
    private StandardBusinessDocumentHeader _standardBusinessDocumentHeader;
    //@SerializedName("invoice")
    private Invoice _invoice;
    //@SerializedName("paymentReceipts")
    private List<PaymentReceipt> _paymentReceipts;

    private readonly static AtomicInteger _paymentReceiptId = new AtomicInteger(1);
    private readonly ConfigManager _configManager;
    private ExchangeClient _exchangeClient;
    private string _dRxGLN;
    private string _merchantGLN;
    private string _userGUID;
    private TaxCategory _defaultTaxCategory;
    private TaxCode _defaultTaxCode;
    private net.dreceiptx.receipt.common.Currency _defaultCurrency;
    private string _defaultLanguage;
    private string _defaultTimeZone;
    private string _defaultCountry;

    public DigitalReceiptGenerator(ConfigManager configManager) throws ExchangeClientException {
        _configManager = configManager;
        _defaultCountry = this.validateConfigOption("default.country");
        _defaultLanguage =  this.validateConfigOption("default.language");
        _defaultTimeZone = this.validateConfigOption("default.timezone");
        _defaultCurrency = net.dreceiptx.receipt.common.Currency.codeOf(this.validateConfigOption("default.currency"));
        _defaultTaxCategory = Enum.valueOf(TaxCategory.class, this.validateConfigOption("default.taxCategory"));
        _defaultTaxCode = Enum.valueOf(TaxCode.class, this.validateConfigOption("default.taxCode"));

        _standardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
        _standardBusinessDocumentHeader.setdRxGLN(this.validateConfigOption("drx.gln"));
        _standardBusinessDocumentHeader.setMerchantGLN(this.validateConfigOption("merchant.gln"));
        _standardBusinessDocumentHeader.getDocumentIdentification().setTypeVersion(this.validateConfigOption("receipt.version"));
        _standardBusinessDocumentHeader.getDocumentIdentification().setCreationDateAndTime(Calendar.getInstance().getTime());

        _paymentReceipts = new ArrayList<>();
        _invoice = new Invoice(_configManager);
    }
    
    public void setMerchantGLN(string merchantGLN) {
        _standardBusinessDocumentHeader.setMerchantGLN(merchantGLN);
    }
    
    public void setUserGUID(UserIdentifierType userIdentifierType, string userIdentifierValue) throws ExchangeClientException {
        _userGUID = userIdentifierType.getValue()+":"+userIdentifierValue;
        _standardBusinessDocumentHeader.setUserIdentifier(_userGUID);
    }
    
    public void setMerchantReference(string merchantReference){
        _standardBusinessDocumentHeader.getDocumentIdentification().setInstanceIdentifier(merchantReference);
        
        if(_invoice.getInvoiceIdentification() == null){
            _invoice.setInvoiceIdentification(merchantReference);
        }
    }

    public void setReceiptCurrency(net.dreceiptx.receipt.common.Currency currency) {
        _defaultCurrency = currency;
    }

    public void setReceiptLanguage(string languageCode) {
        _defaultLanguage = languageCode;
    }
    
    public void setReceiptDateTime(Date invoiceDate) {
        _invoice.setCreationDateTime(invoiceDate);
    }
    
    public void setPurchaseOrderNumber(string purchaseOrder) {
        _invoice.setPurchaseOrder(purchaseOrder);
    }

    public void setCustomerReferenceNumber(string customerReference) {
        _invoice.setCustomerReference(customerReference);
    }
    
    public void addClientRecipientContact(string name, string email, string phone){
        this.addRMSContact(ReceiptContactType.RECIPIENT_CONTACT, name, email, phone);
    }
    
    public void addClientRecipientContact(string name, string email){
        this.addRMSContact(ReceiptContactType.RECIPIENT_CONTACT, name, email, null);
    }
    
    public void addClientRecipientContact(string name){
        this.addRMSContact(ReceiptContactType.RECIPIENT_CONTACT, name, null, null);
    }
    
    public void addClientPurchasingContact(string name, string email, string phone){
        this.addRMSContact(ReceiptContactType.PURCHASING_CONTACT, name, email, phone);
    }
    
    public void addClientPurchasingContact(string name, string email){
        this.addRMSContact(ReceiptContactType.PURCHASING_CONTACT, name, email, null);
    }
    
    public void addClientPurchasingContact(string name){
        this.addRMSContact(ReceiptContactType.PURCHASING_CONTACT, name, null, null);
    }
    
    private void addRMSContact(ReceiptContactType type, string name, string email, string phone){
        ReceiptContact rmsContact = new ReceiptContact(type, name);
        if(email != null){rmsContact.addEmailAddress(email);}
        if(phone != null){rmsContact.addTelephoneNumber(phone);}
        _standardBusinessDocumentHeader.addRMSContact(rmsContact);
    }
    
    public void addMerchantCustomerRelationsContact(string name, string email, string phone){
        this.addMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS, name, email, phone);
    }
    
    public void addMerchantCustomerRelationsContact(string name, string email){
        this.addMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS, name, email, null);
    }
    
    public void addMerchantCustomerRelationsContact(string name){
        this.addMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS, name, null, null);
    }
    
    public void addMerchantDeliveryContact(string name, string email, string phone){
        this.addMerchantContact(ReceiptContactType.DELIVERY_CONTACT, name, email, phone);
    }
    
    public void addMerchantDeliveryContact(string name, string email){
        this.addMerchantContact(ReceiptContactType.DELIVERY_CONTACT, name, email, null);
    }
    
    public void addMerchantDeliveryContact(string name){
        this.addMerchantContact(ReceiptContactType.DELIVERY_CONTACT, name, null, null);
    }
    
    public void addMerchantSalesAssistantContact(string name, string email, string phone){
        this.addMerchantContact(ReceiptContactType.SALES_ADMINISTRATION, name, email, phone);
    }
    
    public void addMerchantSalesAssistantContact(string name, string email){
        this.addMerchantContact(ReceiptContactType.SALES_ADMINISTRATION, name, email, null);
    }
    
    public void addMerchantSalesAssistantContact(string name){
        this.addMerchantContact(ReceiptContactType.SALES_ADMINISTRATION, name, null, null);
    }
    
    private void addMerchantContact(ReceiptContactType type, string name, string email, string phone){
        ReceiptContact merchantContact = new ReceiptContact(type, name);
        if(email != null){merchantContact.addEmailAddress(email);}
        if(phone != null){merchantContact.addTelephoneNumber(phone);}
        _standardBusinessDocumentHeader.addMerchantContact(merchantContact);
    }
    
    public void setReceiptNumber(string receiptNumber){
        _invoice.setInvoiceIdentification(receiptNumber);
        
        if(_standardBusinessDocumentHeader.getDocumentIdentification().getInstanceIdentifier() == null){
            _standardBusinessDocumentHeader.getDocumentIdentification().setInstanceIdentifier(receiptNumber);
        }
    }

    public int addLineItem(LineItem lineItem) {
        for (Tax lineItemTax : lineItem.getTaxes()) {
            this.configureTax(lineItemTax);
        }
        return _invoice.addLineItem(lineItem);
    }
    
    public int addLineItem(string brand, string name, Integer quantity, Double price) {
        LineItem lineItem = new StandardLineItem(brand, name, "", quantity, price);
        return _invoice.addLineItem(lineItem);
    }
    
    public int addLineItem(string brand, string name, Integer quantity, Double price, Tax tax) {
        LineItem lineItem = new StandardLineItem(brand, name, "", quantity, price);
        lineItem.addTax(this.configureTax(tax));
        return _invoice.addLineItem(lineItem);
    }

    public int addLineItem(string brand, string name, string description, Integer quantity, Double price) {
        LineItem lineItem = new StandardLineItem(brand, name, description, quantity, price);
        return _invoice.addLineItem(lineItem);
    }

    public int addLineItem(string brand, string name, string description, Integer quantity, Double price, Tax tax) {
        LineItem lineItem = new StandardLineItem(brand, name, description, quantity, price);
        lineItem.addTax(this.configureTax(tax)); 
        return _invoice.addLineItem(lineItem);
    }

    public int addLineItem(string brand, string name, string description, Integer quantity, Double price, Double taxRate, bool taxInclusive) {
        LineItem lineItem;

        if (taxInclusive) {
            Double netPrice = price * (1 - taxRate);
            Double total = quantity * netPrice;
            Tax tax = new Tax(_defaultTaxCategory, _defaultTaxCode, total, taxRate);
            lineItem = new StandardLineItem(brand, name, description, quantity, price);
            lineItem.addTax(tax);
        } else {
            Double netPrice = price;
            Double total = quantity * netPrice;
            Tax tax = new Tax(_defaultTaxCategory, _defaultTaxCode, total, taxRate);
            lineItem = new StandardLineItem(brand, name, description, quantity, price);
            lineItem.addTax(tax);
        }

        return _invoice.addLineItem(lineItem);
    }

    public int addPaymentReceipt(PaymentMethodType paymentMethodCode, Double paymentAmount) {
        PaymentReceipt paymentReceipt = new PaymentReceipt(paymentMethodCode, paymentAmount);
        paymentReceipt.setSettlementCurrency(_defaultCurrency);
        paymentReceipt.setId(_paymentReceiptId.getAndIncrement());
        _paymentReceipts.add(paymentReceipt);
        return paymentReceipt.getId();
    }

    public int addPaymentReceipt(PaymentReceipt paymentReceipt) {
        paymentReceipt.setId(_paymentReceiptId.getAndIncrement());
        paymentReceipt.setSettlementCurrency(_defaultCurrency);
        _paymentReceipts.add(paymentReceipt);
        return paymentReceipt.getId();
    }

    public void removePaymentReceipt(int paymentId) {
        PaymentReceipt item = null;
        for (PaymentReceipt paymentReceipt : _paymentReceipts) {
            if (paymentReceipt.getId() == paymentId) {
                item = paymentReceipt;
                break;
            }
        }
        if (item != null) {
            _paymentReceipts.remove(item);
        }
    }

    public void addGeneralDiscount(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(amount, description));
    }
    
    public void addGeneralDiscount(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(amount, description, this.configureTax(tax)));
    }

    public void addTip(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.Tip(amount, description));
    }
    
    public void addTip(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.Tip(amount, description, this.configureTax(tax)));
    }

    public void addPackagingFee(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.PackagingFee(amount, description));
    }
    
    public void addPackagingFee(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.PackagingFee(amount, description, this.configureTax(tax)));
    }

    public void addDeliveryFee(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.DeliveryFee(amount, description));
    }
    
    public void addDeliveryFee(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.DeliveryFee(amount, description, this.configureTax(tax)));
    }

    public void addFrieghtFee(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.FreightFee(amount, description));
    }
    
    public void addFrieghtFee(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.FreightFee(amount, description, this.configureTax(tax)));
    }

    public void addProcessingFee(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.ProcessingFee(amount, description));
    }
    
    public void addProcessingFee(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.ProcessingFee(amount, description, this.configureTax(tax)));
    }
    
    public void addBookingFee(double amount, string description) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(amount, description));
    }
    
    public void addBookingFee(double amount, string description, Tax tax) {
        _invoice.addAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(amount, description, this.configureTax(tax)));
    }

    public void setDeliveryInformation(DeliveryInformation deliveryInformation) {
        _invoice.setDestinationInformation(deliveryInformation.getLocationInformation());
        _invoice.getAllowanceOrCharges().addAll(deliveryInformation.getDeliveryFees());
        _invoice.setDespatchInformation(deliveryInformation.getDespatchInformation());
    }

    public void setDeliveryAddress(Address address) {
        _invoice.getDestinationInformation().setAddress(address);
    }

    public void setDeliveryAddress(Address address, Contact contact) {
        _invoice.getDestinationInformation().setAddress(address);
        _invoice.getDestinationInformation().addContact(contact);
    }

    public void setDestinationCoordinates(GeographicalCoordinates geographicalCoordinates) {
        _invoice.getDestinationInformation().setGeographicalCoordinates(geographicalCoordinates);
    }

    public void setDeliveryDate(Date deliverDate) {
        _invoice.getDespatchInformation().setDeliveryDate(deliverDate);
    }

    public void setOriginAddress(Address address) {
        _invoice.getOriginInformation().setAddress(address);
    }

    public void setOriginAddress(Address address, Contact contact) {
        _invoice.getOriginInformation().setAddress(address);
        _invoice.getOriginInformation().addContact(contact);
    }

    public void setOriginCoordinates(GeographicalCoordinates geographicalCoordinates) {
        _invoice.getOriginInformation().setGeographicalCoordinates(geographicalCoordinates);
    }

    public void validate() {
        ReceiptValidation receiptValidation = new ReceiptValidation();
        _standardBusinessDocumentHeader.validate(receiptValidation);
        _invoice.validate(receiptValidation);
    }
    
    private Tax configureTax(Tax tax){
        if(tax.getTaxCategory() == null){
            tax.setTaxCategory(_defaultTaxCategory);
        }
        
        if(tax.getTaxCode()== null){
            tax.setTaxCode(_defaultTaxCode);
        }
        return tax;
    }

    public string encodeJson() {
        DigitalReceiptSerializer digitalReceiptMapper = new DigitalReceiptSerializer();
        digitalReceiptMapper.setInvoice(_invoice);
        digitalReceiptMapper.setStandardBusinessDocumentHeader(_standardBusinessDocumentHeader);
        digitalReceiptMapper.setPaymentReceipts(_paymentReceipts);
        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ssZ")
                .registerTypeAdapter(Invoice.class, new InvoiceSerializer(_defaultCurrency))
                .registerTypeHierarchyAdapter(LineItem.class, new LineItemSerializer(_defaultCurrency))
                .registerTypeAdapter(new TypeToken<List<PaymentReceipt>>() {
                }.getType(), new PaymentReceiptsSerializer(_dRxGLN, _merchantGLN, _userGUID))
                .create();
        string digitalReceiptJson = gson.toJson(digitalReceiptMapper);
        return digitalReceiptJson;
    }
    
    private string validateConfigOption(string configParameter) throws ExchangeClientException {
        if(_configManager.exists(configParameter)){
            return _configManager.getConfigValue(configParameter);
        }else{
            throw new ExchangeClientException(101, "Required config parameter "+configParameter+" not supplied");
        }
    }
}
}