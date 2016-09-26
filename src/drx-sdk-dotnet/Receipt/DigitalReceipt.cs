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

package net.dreceiptx.receipt;

import net.dreceiptx.receipt.allowanceCharge.ReceiptAllowanceCharge;
import net.dreceiptx.receipt.common.Address;
import net.dreceiptx.receipt.common.Contact;
import net.dreceiptx.receipt.common.GeographicalCoordinates;
import net.dreceiptx.receipt.document.DocumentOwner;
import net.dreceiptx.receipt.document.ReceiptContact;
import net.dreceiptx.receipt.document.ReceiptContactType;
import net.dreceiptx.receipt.document.StandardBusinessDocumentHeader;
import net.dreceiptx.receipt.invoice.Invoice;
import net.dreceiptx.receipt.lineitem.LineItem;
import net.dreceiptx.receipt.serialization.json.InvoiceDeserializer;
import net.dreceiptx.receipt.serialization.json.LineItemDeserializer;
import net.dreceiptx.receipt.serialization.json.PaymentReceiptDeserializer;
import net.dreceiptx.receipt.settlement.PaymentReceipt;
import net.dreceiptx.receipt.tax.TaxCode;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.annotations.SerializedName;
import com.google.gson.reflect.TypeToken;
import net.dreceiptx.receipt.validation.ReceiptDeserializationException;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class DigitalReceipt {
    @SerializedName("standardBusinessDocumentHeader")
    private StandardBusinessDocumentHeader _standardBusinessDocumentHeader;
    @SerializedName("invoice")
    private Invoice _invoice;
    @SerializedName("paymentReceipts")
    private List<PaymentReceipt> _paymentReceipts;

    public DigitalReceipt(String digitalReceiptJson) throws ReceiptDeserializationException {
        JsonParser parser = new JsonParser();
        JsonObject digitalReceiptObject = parser.parse(digitalReceiptJson).getAsJsonObject().getAsJsonObject("dRxDigitalReceipt");

        Gson gson = new GsonBuilder()
                .registerTypeAdapter(Invoice.class, new InvoiceDeserializer())
                .registerTypeHierarchyAdapter(LineItem.class, new LineItemDeserializer())
                .registerTypeAdapter(new TypeToken<List<PaymentReceipt>>() {
                }.getType(), new PaymentReceiptDeserializer())
                .create();

        this._standardBusinessDocumentHeader = gson.fromJson(digitalReceiptObject.get("standardBusinessDocumentHeader").toString(), StandardBusinessDocumentHeader.class);
        this._invoice = gson.fromJson(digitalReceiptObject.get("invoice").toString(), Invoice.class);
        this._paymentReceipts = gson.fromJson(digitalReceiptObject.get("paymentReceipts").toString(), new TypeToken<List<PaymentReceipt>>() {
        }.getType());

        if( _invoice == null
                || this._standardBusinessDocumentHeader.equals(null)
                || this._paymentReceipts.equals(null)){
            throw new ReceiptDeserializationException("Failed to deserialize Digital Receipt data from source");
        }
    }
    
    public String getUserGUID() {
        return _standardBusinessDocumentHeader.getUserIdentifier();
    }
    
    public String getMerchantName() {
        return _invoice.getMerchantName();
    }
    
    public String getCompanyTaxNumber(TaxCode taxCode) {
        return _invoice.getCompanyTaxNumber(taxCode);
    }
    
    public String getMerchantLocationReference() {
        return _standardBusinessDocumentHeader.getMerchantGLN();
    }
    
    public Date getReceiptDate() {
        return _invoice.getCreationDateTime();
    }
    
    public String getReceiptReference() {
        return _invoice.getInvoiceIdentification();
    }
    
    public String getPurchaseOrderNumber() {
        return _invoice.getPurchaseOrder();
    }

    public String getCustomerReferenceNumber() {
        return _invoice.getCustomerReference();
    }
    
    public List<ReceiptContact> getMerchantCustomerRelations(){
        return this.getMerchantContact(ReceiptContactType.CUSTOMER_RELATIONS);
    }
    
    public List<ReceiptContact> getMerchantDeliveryContact(){
        return this.getMerchantContact(ReceiptContactType.DELIVERY_CONTACT);
    }
    
    public List<ReceiptContact> getMerchantSalesAssistant(){
        return this.getMerchantContact(ReceiptContactType.SALES_ADMINISTRATION);
    }
    
    private List<ReceiptContact> getMerchantContact(ReceiptContactType receiptContactType){
        List<ReceiptContact> contacts = new ArrayList<ReceiptContact>();
        for (DocumentOwner sender : _standardBusinessDocumentHeader.getSender()) {
            if(sender.getIdentifier().getAuthority().equals("GS1")){
                for (ReceiptContact contact : sender.getDocumentOwnerContact()) {
                    if(contact.getReceiptContactType().equals(receiptContactType)){
                        contacts.add(contact);
                    }
                }
            }
        }
        return contacts;
    }
    
    public List<ReceiptContact> getClientRecipientContact(){
        return this.getRMSContact(ReceiptContactType.RECIPIENT_CONTACT);
    }
    
    public List<ReceiptContact> getClientPurchasingContact(){
        return this.getRMSContact(ReceiptContactType.PURCHASING_CONTACT);
    }
    
    private List<ReceiptContact> getRMSContact(ReceiptContactType contactType){
        List<ReceiptContact> contacts = new ArrayList<ReceiptContact>();
        for (DocumentOwner receiver : _standardBusinessDocumentHeader.getReceiver()) {
            if(receiver.getIdentifier().getAuthority().equals("dRx")){
                if(receiver.getDocumentOwnerContact() == null){
                    return contacts;
                }
                
                for (ReceiptContact contact : receiver.getDocumentOwnerContact()) {
                    if(contact.getReceiptContactType().equals(contactType)){
                        contacts.add(contact);
                    }
                }
            }
        }
        return contacts;
    }

    public Address getDeliveryAddress() {
        return _invoice.getDestinationInformation().getAddress();
    }

    public List<Contact> getCustomerDeliveryContactDetails() {
        return _invoice.getDestinationInformation().getContacts();
    }

    public Address getOriginAddress() {
        return _invoice.getOriginInformation().getAddress();
    }

    public List<Contact> getOriginContact() {
        return _invoice.getOriginInformation().getContacts();
    }

    public GeographicalCoordinates getOriginCoordinates() {
        return _invoice.getOriginInformation().getAddress().getGeographicalCoordinates();
    }
    
    public GeographicalCoordinates getDestinationCoordinates() {
        return _invoice.getDestinationInformation().getAddress().getGeographicalCoordinates();
    }
    
    public List<LineItem> getLineItems() {
        return _invoice.getInvoiceLineItems();
    }
    
    public List<ReceiptAllowanceCharge> getCharges() {
        List<ReceiptAllowanceCharge> charges = new ArrayList<ReceiptAllowanceCharge>();
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _invoice.getAllowanceOrCharges()) {
            if(receiptAllowanceCharge.isCharge()){
                charges.add(receiptAllowanceCharge);
            }
        }
        return charges;
    }
    
    public List<ReceiptAllowanceCharge> getAllowances() {
        List<ReceiptAllowanceCharge> allowances = new ArrayList<ReceiptAllowanceCharge>();
        for (ReceiptAllowanceCharge receiptAllowanceCharge : _invoice.getAllowanceOrCharges()) {
            if(receiptAllowanceCharge.isAllowance()){
                allowances.add(receiptAllowanceCharge);
            }
        }
        return allowances;
    }
    
    public double getTotal() {
        return _invoice.getTotal();
    }
    
    public double getSubTotal() {
        return _invoice.getSubTotal();
    }
    
    public double getTaxTotal(TaxCode taxCode) {
        return _invoice.getTaxesTotal(taxCode);
    }
}
