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

import net.dreceiptx.receipt.common.Currency;
import net.dreceiptx.receipt.ecom.AVP;
import net.dreceiptx.receipt.lineitem.LineItem;
import com.google.gson.Gson;
import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

import java.lang.reflect.Type;
import java.util.Map;

public class LineItemSerializer implements JsonSerializer<LineItem>
{
    private Currency _lineItemCurrency;
    
    public LineItemSerializer(Currency currency) {
        _lineItemCurrency = currency;
    }
    
    @Override
    public JsonElement serialize(LineItem lineitem, Type type, JsonSerializationContext jsc) {
        JsonObject tree = (JsonObject)new Gson().toJsonTree(lineitem);
        tree.addProperty("lineItemNumber", lineitem.getLineItemId());
        tree.addProperty("creditLineIndicator", lineitem.isReturnOrExchange());
        tree.addProperty("invoicedQuantity", lineitem.getQuantity());
        tree.addProperty("itemPriceExclusiveAllowancesCharges", lineitem.getPrice());
        tree.addProperty("amountInclusiveAllowancesCharges", lineitem.getNetTotal());
        tree.addProperty("amountExclusiveAllowancesCharges", lineitem.getSubTotal());
        
        if(lineitem.getBillingCostCentre()!= null){
            JsonObject billingCostCentre = new JsonObject();
            billingCostCentre.addProperty("entityIdentification", lineitem.getBillingCostCentre());
            tree.add("billingCostCentre", billingCostCentre);
        }
        
        JsonObject transactionalTradeItem = new JsonObject();
        transactionalTradeItem.add("tradeItemDescriptionInformation", jsc.serialize(lineitem.getTradeItemDescriptionInformation()));
        
        switch (lineitem.getTransactionalTradeItemType()) {
            case GTIN :  
                transactionalTradeItem.addProperty("gtin", lineitem.getTransactionalTradeItemCode());
                break;
            case MANUAL:  
                break;
        }
        
        JsonArray additionalTradeItemIdentification = new JsonArray();
        for (Map.Entry<String, String> tradeItemIdentification : lineitem.getTradeItemIdentification().getEntrySet()) {
            JsonObject tradeItemIdentificationObject = new JsonObject();
            tradeItemIdentificationObject.addProperty("additionalTradeItemIdentificationType", tradeItemIdentification.getKey());
            tradeItemIdentificationObject.addProperty("additionalTradeItemIdentificationValue", tradeItemIdentification.getValue());
            additionalTradeItemIdentification.add(tradeItemIdentificationObject);
        }
        
        if(additionalTradeItemIdentification.size() != 0){
            transactionalTradeItem.add("additionalTradeItemIdentification", additionalTradeItemIdentification);
        }
        
        JsonObject transactionalItemData = new JsonObject();
        if(lineitem.getSerialNumber() != null){
            transactionalItemData.addProperty("serialNumber", lineitem.getSerialNumber());
        }
        
        if(lineitem.getBatchNumber()!= null){
            transactionalItemData.addProperty("batchNumber", lineitem.getBatchNumber());  
        }
        
        if(!transactionalItemData.entrySet().isEmpty()){
            transactionalTradeItem.add("transactionalItemData", transactionalItemData);
        }
        
        tree.add("transactionalTradeItem", jsc.serialize(transactionalTradeItem));
        
        if(lineitem.hasTaxes()){
            tree.add("invoiceLineTaxInformation", jsc.serialize(lineitem.getTaxes()));
        }

        if(lineitem.getOriginInformation().gsonValidator()){
            tree.add("shipFrom", jsc.serialize(lineitem.getOriginInformation()));
        }

        if(lineitem.getDestinationInformation().gsonValidator()){
            tree.add("shipTo", jsc.serialize(lineitem.getDestinationInformation()));
        }

        if(lineitem.getDespatchInformation().gsonValidator()){
            tree.add("despatchInformation", jsc.serialize(lineitem.getDespatchInformation()));
        }

        JsonArray avpList  = new JsonArray();
        for (Map.Entry<String, AVP> avp : lineitem.getEcomAVPList().getEntrySet()) {
            avpList.add(jsc.serialize(avp.getValue()));
        }

        if(avpList.size() != 0){
            tree.add("avpList", avpList);
        }
        
        tree.add("invoiceAllowanceCharge", jsc.serialize(lineitem.getReceiptAllowanceCharges()));
        
        return tree;
    }
}
