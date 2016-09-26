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

package net.dreceiptx.receipt.serialization.json;

import net.dreceiptx.receipt.allowanceCharge.ReceiptAllowanceCharge;
import net.dreceiptx.receipt.common.LocationInformation;
import net.dreceiptx.receipt.common.DespatchInformation;
import net.dreceiptx.receipt.invoice.Invoice;
import net.dreceiptx.receipt.lineitem.LineItem;
import com.google.gson.JsonArray;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

import java.lang.reflect.Type;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.TimeZone;

public class InvoiceDeserializer implements JsonDeserializer<Invoice>
{    
    @Override
    public Invoice deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext ctx) throws JsonParseException
    {
        DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ssZ");
        dateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
        
        JsonObject invoiceObject = (JsonObject) json;

        try{
            Invoice invoice = new Invoice();
            invoice.setCreationDateTime(dateFormat.parse(invoiceObject.get("creationDateTime").getAsString()));
            invoice.setInvoiceCurrencyCode(invoiceObject.get("invoiceCurrencyCode").getAsString());
            invoice.setCountryOfSupplyOfGoods(invoiceObject.get("countryOfSupplyOfGoods").getAsString());
            
            if(invoiceObject.has("invoiceIdentification")){
                JsonObject invoiceIdentification = invoiceObject.get("invoiceIdentification").getAsJsonObject();
                invoice.setInvoiceIdentification(invoiceIdentification.get("entityIdentification").getAsString());  
            }
            
            if(invoiceObject.has("customerReference")){
                JsonObject customerReference = invoiceObject.get("customerReference").getAsJsonObject();
                invoice.setCustomerReference(customerReference.get("entityIdentification").getAsString());  
            }
            
            if(invoiceObject.has("purchaseOrder")){
                JsonObject purchaseOrder = invoiceObject.get("purchaseOrder").getAsJsonObject();
                invoice.setPurchaseOrder(purchaseOrder.get("entityIdentification").getAsString());  
            }
            
            JsonArray lineItems = invoiceObject.getAsJsonArray("invoiceLineItem");
            for(int i = 0; i < lineItems.size(); i++)
            {
                invoice.addLineItem((LineItem) ctx.deserialize(lineItems.get(i), LineItem.class));
            }
            if(invoiceObject.has("invoiceAllowanceCharge")){
                JsonArray invoiceAllowanceCharges = invoiceObject.getAsJsonArray("invoiceAllowanceCharge");
                for(int i = 0; i < invoiceAllowanceCharges.size(); i++)
                {
                    invoice.addAllowanceOrCharge((ReceiptAllowanceCharge) ctx.deserialize(invoiceAllowanceCharges.get(i), ReceiptAllowanceCharge.class));
                }
            }
            if(invoiceObject.has("shipFrom")){
                invoice.setOriginInformation((LocationInformation) ctx.deserialize(invoiceObject.get("shipFrom"), LocationInformation.class));
            }
            if(invoiceObject.has("shipTo")){
                invoice.setDestinationInformation((LocationInformation) ctx.deserialize(invoiceObject.get("shipTo"), LocationInformation.class));
            }
            if(invoiceObject.has("despatchInformation")){
                invoice.setDespatchInformation((DespatchInformation) ctx.deserialize(invoiceObject.get("despatchInformation"), DespatchInformation.class));
            }
            
            if(invoiceObject.has("seller")){
                JsonObject sellerObject = invoiceObject.get("seller").getAsJsonObject();
                if(sellerObject.has("organisationDetails")){
                    JsonObject organisationDetailsObject = sellerObject.get("organisationDetails").getAsJsonObject();
                    invoice.setMerchantName(organisationDetailsObject.get("organisationName").getAsString());
                }
                if(sellerObject.has("dutyFeeTaxRegistration")){
                    JsonObject dutyFeeTaxRegistrationObject = sellerObject.get("dutyFeeTaxRegistration").getAsJsonObject();
                    invoice.addCompanyTaxNumber(dutyFeeTaxRegistrationObject.get("dutyFeeTaxTypeCode").getAsString(), dutyFeeTaxRegistrationObject.get("dutyFeeTaxRegistrationID").getAsString());
                }
            }
            
            return invoice;
        }catch(Exception e){
            //TODO: Add error handling
            String exception = e.toString();
            System.out.println(exception);
            String error = e.getLocalizedMessage();
            System.out.println(error);
            String stack = e.getStackTrace().toString();
            System.out.println(stack);
        }

        return null;
    }
}
