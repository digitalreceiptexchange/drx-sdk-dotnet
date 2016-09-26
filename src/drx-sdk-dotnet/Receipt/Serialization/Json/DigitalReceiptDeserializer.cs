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

import net.dreceiptx.receipt.invoice.Invoice;
import net.dreceiptx.receipt.lineitem.LineItem;
import com.google.gson.Gson;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

public class DigitalReceiptDeserializer implements JsonDeserializer<Invoice>
{    
    @Override
    public Invoice deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext ctx) throws JsonParseException
    {
        DateFormat dateFormatter = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ssZ");
        JsonObject invoiceObject = (JsonObject) json;
        
        try{
            Invoice invoice = new Invoice();
            invoice.setCreationDateTime(dateFormatter.parse(invoiceObject.get("creationDateTime").getAsString()));
            invoice.setInvoiceCurrencyCode(invoiceObject.get("invoiceCurrencyCode").getAsString());
            invoice.setCountryOfSupplyOfGoods(invoiceObject.get("countryOfSupplyOfGoods").getAsString());
            Type listType = new TypeToken<ArrayList<LineItem>>() {}.getType();
            List<LineItem> lineItems = new Gson().fromJson(invoiceObject.get("invoiceLineItem").getAsJsonObject(), listType);
            invoice.setInvoiceLineItems(lineItems);
            return invoice;
        }catch(Exception e){
            //TODO: Add error handling
        }
        
        return null;
    }
}
