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

import net.dreceiptx.receipt.common.Amount;
import net.dreceiptx.receipt.common.Currency;
import net.dreceiptx.receipt.invoice.Invoice;
import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

import java.lang.reflect.Type;

public class InvoiceSerializer implements JsonSerializer<Invoice>
{
    private Currency _invoiceCurrency;

    public InvoiceSerializer(Currency currency) {
        _invoiceCurrency = currency;
    }

    @Override
    public JsonElement serialize(Invoice invoice, Type type, JsonSerializationContext jsc) {
        JsonObject tree = (JsonObject)new Gson().toJsonTree(invoice);
        tree.addProperty("creationDateTime", invoice.getCreationDateTimeString());
        
        JsonObject invoiceIdentification = new JsonObject();
        invoiceIdentification.addProperty("entityIdentification", invoice.getInvoiceIdentification());
        tree.add("invoiceIdentification", invoiceIdentification);
        
        if(invoice.getCustomerReference() != null){
            JsonObject customerReference = new JsonObject();
            customerReference.addProperty("entityIdentification", invoice.getCustomerReference());
            tree.add("customerReference", customerReference);
        }
        
        if(invoice.getPurchaseOrder() != null){
            JsonObject purchaseOrder = new JsonObject();
            purchaseOrder.addProperty("entityIdentification", invoice.getPurchaseOrder());
            tree.add("purchaseOrder", purchaseOrder);
        }
        
        JsonObject invoiceTotals = new JsonObject();
        invoiceTotals.add("totalInvoiceAmount", (JsonObject)new Gson().toJsonTree(new Amount(_invoiceCurrency, invoice.getSubTotal())));
        invoiceTotals.add("totalLineAmountInclusiveAllowancesCharges", (JsonObject)new Gson().toJsonTree(new Amount(_invoiceCurrency, invoice.getTotal())));
        invoiceTotals.add("totalTaxAmount", (JsonObject)new Gson().toJsonTree(new Amount(_invoiceCurrency, invoice.getTaxesTotal())));
        tree.add("invoiceTotals", invoiceTotals);
        tree.add("invoiceLineItem", jsc.serialize(invoice.getInvoiceLineItems()));
        tree.add("invoiceAllowanceCharge", jsc.serialize(invoice.getAllowanceOrCharges()));
        
        if(invoice.getOriginInformation().gsonValidator()){
            tree.add("shipFrom", jsc.serialize(invoice.getOriginInformation()));
        }
        if(invoice.getDestinationInformation().gsonValidator()){
            tree.add("shipTo", jsc.serialize(invoice.getDestinationInformation()));
        }
        if(invoice.getDespatchInformation().gsonValidator()){
            tree.add("despatchInformation", jsc.serialize(invoice.getDespatchInformation()));
        }
        
        return tree;
    }
}
