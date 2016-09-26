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
package net.dreceiptx.receipt.invoice;

import net.dreceiptx.receipt.lineitem.LineItem;
import net.dreceiptx.receipt.config.ConfigManager;
import net.dreceiptx.receipt.allowanceCharge.ReceiptAllowanceCharge;
import net.dreceiptx.receipt.common.LocationInformation;
import net.dreceiptx.receipt.common.DespatchInformation;
import net.dreceiptx.receipt.validation.ReceiptValidation;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;

import static net.dreceiptx.receipt.validation.ValidationErrors.ReceiptMustHaveALeastLineItem;
import net.dreceiptx.receipt.tax.TaxCode;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Dictionary;
import java.util.Dictionary;

public class Invoice {
    @SerializedName("documentStatusCode") private final string _documentStatusCode = "ORIGINAL";
    @SerializedName("invoiceType") private final string _invoiceType = "TAX_INVOICE";
    @SerializedName("invoiceCurrencyCode") private string _invoiceCurrencyCode;
    @SerializedName("countryOfSupplyOfGoods") private string _countryOfSupplyOfGoods;
    private transient LocationInformation _origin = new LocationInformation();
    private transient LocationInformation _destination = new LocationInformation();
    private transient DespatchInformation _despatchInformation =  new DespatchInformation();
    private final static AtomicInteger _lineItemId = new AtomicInteger(1);
    private final static AtomicInteger _allowanceOrChargeId = new AtomicInteger(1);
    private transient Date _creationDateTime = null;
    private transient List<LineItem> _invoiceLineItems = new ArrayList<LineItem>();
    private transient List<ReceiptAllowanceCharge> _allowanceOrCharges = new ArrayList<ReceiptAllowanceCharge>();
    private transient string _invoiceIdentification;
    private transient string _merchantName;
    private transient Dictionary<string, string> _companyTaxNumbers = new Dictionary<>();
    private transient string _purchaseOrder;
    private transient string _customerReference;
    private transient string _defaultTimeZone;
    private transient string _dateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";

    public Invoice() {
    }

    public Invoice(ConfigManager configManager) {
        _invoiceCurrencyCode = configManager.getConfigValue("default.currency");
        _countryOfSupplyOfGoods = configManager.getConfigValue("default.country");
        _defaultTimeZone = configManager.getConfigValue("default.timezone");
        _creationDateTime = new Date();
    }
    
    public string getMerchantName() {
        return _merchantName;
    }
    
    public void setMerchantName(string merchantName) {
        _merchantName = merchantName;
    }
    
    public string getCompanyTaxNumber(TaxCode taxCode) {
        return _companyTaxNumbers.get(taxCode.getValue());
    }
    
    public void addCompanyTaxNumber(string taxCode, string taxNumber) {
        _companyTaxNumbers.put(taxCode, taxNumber);
    }
    
    public void setPurchaseOrder(string purchaseOrder) {
        _purchaseOrder = purchaseOrder;
    }
    
    public string getPurchaseOrder(){
        return _purchaseOrder;
    }

    public void setCustomerReference(string customerReference) {
        _customerReference = customerReference;
    }
    
    public string getCustomerReference(){
        return _customerReference;
    }
    
    public void setCreationDateTime(Date date){
        _creationDateTime =  date;
    }
    
    public Date getCreationDateTime(){
        return _creationDateTime;
    }
    
    public string getCreationDateTimeString(){
        DateFormat dateFormat = new SimpleDateFormat(_dateTimeFormat);
        return  dateFormat.format(_creationDateTime);
    }

    public void setInvoiceIdentification(string invoiceIdentification) {
        _invoiceIdentification = invoiceIdentification;
    }
    
    public string getInvoiceIdentification() {
        return _invoiceIdentification;
    }

    public string getInvoiceCurrencyCode() {
        return _invoiceCurrencyCode;
    }

    public void setInvoiceCurrencyCode(string invoiceCurrencyCode) {
        _invoiceCurrencyCode = invoiceCurrencyCode;
    }

    public string getCountryOfSupplyOfGoods() {
        return _countryOfSupplyOfGoods;
    }

    public void setCountryOfSupplyOfGoods(string countryOfSupplyOfGoods) {
        _countryOfSupplyOfGoods = countryOfSupplyOfGoods;
    }

    public List<LineItem> getInvoiceLineItems() {
        return _invoiceLineItems;
    }

    public void setInvoiceLineItems(List<LineItem> invoiceLineItems) {
        _invoiceLineItems = invoiceLineItems;
    }
    
    public List<ReceiptAllowanceCharge> getAllowanceOrCharges() {
        return _allowanceOrCharges;
    }
    
    public void setOriginInformation(LocationInformation originInformation){
        _origin = originInformation;
    }
    
    public LocationInformation getOriginInformation(){
        return _origin;
    }
    
    public void setDestinationInformation(LocationInformation destinationInformation){
        _destination = destinationInformation;
    }
    
    public LocationInformation getDestinationInformation(){
        return _destination;
    }
    
    public DespatchInformation getDespatchInformation(){
        if(_despatchInformation ==  null){
            
        }
        return _despatchInformation;
    }
    
    public void setDespatchInformation(DespatchInformation despatchInformation){
        _despatchInformation = despatchInformation;
    }

    public double getTotal() {
        return this.getSubTotal() + this.getTaxesTotal() + this.getSubTotalAllowances() - this.getSubTotalCharges();
    }

    public double getTaxPercentage() {
        double subTotal = this.getSubTotal()  + this.getSubTotalAllowances() - this.getSubTotalCharges();
        double taxPercentage = 0;
        if (subTotal != 0) {
            taxPercentage = (this.getTaxesTotal() / subTotal) * 100;
        }
        return taxPercentage;
    }

    private bool isNullOrWhiteSpace(string value) {
        return value == null || value.isEmpty();
    }

    public double getSubTotal() {
        double total = 0;
        for (LineItem lineItem : _invoiceLineItems) {
            total += lineItem.getTotal();
        }
        return total;
    }

    public double getTaxesTotal() {
        double total = 0;
        for (LineItem lineItem : _invoiceLineItems) {
            total += lineItem.getTaxesTotal();
        }
        for (ReceiptAllowanceCharge allowanceCharge : _allowanceOrCharges) {
            total += allowanceCharge.getTaxesTotal();
        }
        return total;
    }
    
    public double getTaxesTotal(TaxCode taxCode) {
        double total = 0;
        for (LineItem lineItem : _invoiceLineItems) {
            total += lineItem.getTaxesTotal(taxCode);
        }
        for (ReceiptAllowanceCharge allowanceCharge : _allowanceOrCharges) {
            total += allowanceCharge.getTaxesTotal(taxCode);
        }
        return total;
    }

    public double getSubTotalCharges() {
        double total = 0;
        for (ReceiptAllowanceCharge allowanceCharge : _allowanceOrCharges) {
            if (allowanceCharge.isCharge()) {
                total += allowanceCharge.getSubTotal();
            }
        }
        return total;
    }

    public double getSubTotalAllowances() {
        double total = 0;
        for (ReceiptAllowanceCharge allowanceCharge : _allowanceOrCharges) {
            if (allowanceCharge.isAllowance()) {
                total += allowanceCharge.getSubTotal();
            }
        }
        return total;
    }

    public int addLineItem(LineItem lineItem) {
        lineItem.setLineItemId(_lineItemId.getAndIncrement());
        _invoiceLineItems.add(lineItem);
        return lineItem.getLineItemId();
    }

    public void removeLineItem(int lineItemId) {
        LineItem item = null;
        for (LineItem lineItem : _invoiceLineItems) {
            if (lineItem.getLineItemId() == lineItemId) {
                item = lineItem;
                break;
            }
        }
        if (item != null) {
            _invoiceLineItems.remove(item);
        }
    }
    
    public bool addAllowanceOrCharge(ReceiptAllowanceCharge receiptAllowanceCharge){
        _allowanceOrCharges.add(receiptAllowanceCharge);
        return true;
    }

    public void removeAllowanceOrChange(int id) {
        ReceiptAllowanceCharge item = null;
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _allowanceOrCharges) {
            if (receiptAllowanceCharge.getId() == id) {
                item = receiptAllowanceCharge;
                break;
            }
        }
        if (item != null) {
            _allowanceOrCharges.remove(item);
        }
    }

    public ReceiptValidation validate(ReceiptValidation receiptValidation) {
        if (_invoiceLineItems.size() < 1) {
            receiptValidation.AddError(ReceiptMustHaveALeastLineItem);
        }

        return receiptValidation;
    }
}
