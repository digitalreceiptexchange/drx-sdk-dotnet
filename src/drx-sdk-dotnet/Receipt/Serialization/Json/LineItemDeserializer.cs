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


import com.google.gson.*;
import net.dreceiptx.receipt.common.DespatchInformation;
import net.dreceiptx.receipt.common.LocationInformation;
import net.dreceiptx.receipt.ecom.AVP;
import net.dreceiptx.receipt.lineitem.TradeItemDescriptionInformation;
import net.dreceiptx.receipt.lineitem.TransactionalTradeItemType;
import net.dreceiptx.receipt.lineitem.*;
import net.dreceiptx.receipt.tax.Tax;
import net.dreceiptx.receipt.validation.ReceiptDeserializationException;

import java.lang.reflect.Type;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.TimeZone;

public class LineItemDeserializer implements JsonDeserializer<LineItem>
{
    private transient TransactionalTradeItemType _transactionalTradeItemType = null;
    private TradeItemDescriptionInformation _tradeItemDescriptionInformation = null;
    private transient String _transactionalTradeItemCode = null;
    private TradeItemIdentification _tradeItemIdentification = new TradeItemIdentification();
    private LineItemFactory _lineItemFactory = new LineItemFactory();
    
    @Override
    public LineItem deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext ctx) throws JsonParseException
    {
        DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mmZ");
        dateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
        
        JsonObject lineItemObject = (JsonObject) json;
        
        LineItem lineItem = null;
        
        try{
            //Extract the main components for the standard to create the LineItem
            Integer quantity = lineItemObject.get("invoicedQuantity").getAsInt();
            Double price = lineItemObject.get("itemPriceExclusiveAllowancesCharges").getAsDouble();
            //Get the TradeItem detail
            JsonObject transactionalTradeItemObject =lineItemObject.get("transactionalTradeItem").getAsJsonObject();
            //Firstly, get the TradeItemIdentification data from the data to help create the lineitem
            if(transactionalTradeItemObject.has("additionalTradeItemIdentification")){
                JsonArray additionalTradeItemIdentifications = transactionalTradeItemObject.get("additionalTradeItemIdentification").getAsJsonArray();
                for(int i = 0; i < additionalTradeItemIdentifications.size(); i++)
                {
                    JsonObject additionalTradeItemIdentificationObject = additionalTradeItemIdentifications.get(i).getAsJsonObject();
                    _tradeItemIdentification.add(additionalTradeItemIdentificationObject.get("additionalTradeItemIdentificationType").getAsString(), 
                            additionalTradeItemIdentificationObject.get("additionalTradeItemIdentificationValue").getAsString());
                } 
            }
            
            String DRX_LINEITEM_TYPE = _tradeItemIdentification.get(LineItem.LineItemTypeIdentifier, StandardLineItem.LineItemTypeValue);

            if(transactionalTradeItemObject.has("tradeItemDescriptionInformation")){
                TradeItemDescriptionInformation tradeItemDescriptionInformation = 
                        new Gson().fromJson(transactionalTradeItemObject.get("tradeItemDescriptionInformation").getAsJsonObject(), TradeItemDescriptionInformation.class);
                lineItem = _lineItemFactory.createLineItem(DRX_LINEITEM_TYPE, tradeItemDescriptionInformation, quantity, price);
            }else{
                throw new ReceiptDeserializationException("TradeItemDescriptionInformation is required, unsupported Digital Receipt format");
            }

            if(transactionalTradeItemObject.has("gtin")){
                String gtin = transactionalTradeItemObject.get("gtin").getAsString();
                lineItem.setTransactionalTradeItemType(TransactionalTradeItemType.GTIN, gtin);

            }

            //Add the TradeItemIdentification details to the receipt
            lineItem.setTradeItemIdentification(_tradeItemIdentification);
            lineItem.setLineItemId(lineItemObject.get("lineItemNumber").getAsInt());
            
            if(lineItemObject.get("creditLineIndicator").getAsBoolean()){
                lineItem.setReturnOrExchange();
            }
            
            if(transactionalTradeItemObject.has("transactionalItemData")){
                JsonObject transactionalItemData = transactionalTradeItemObject.get("transactionalItemData").getAsJsonObject();
                if(transactionalItemData.has("batchNumber")){
                    lineItem.setBatchNumber(transactionalItemData.get("batchNumber").getAsString());  
                }
                if(transactionalItemData.has("serialNumber")){
                    lineItem.setSerialNumber(transactionalItemData.get("serialNumber").getAsString());  
                }
            }
            
            if(lineItemObject.has("invoiceLineTaxInformation")){
                JsonArray invoiceLineTaxInformation = lineItemObject.getAsJsonArray("invoiceLineTaxInformation");
                for(int i = 0; i < invoiceLineTaxInformation.size(); i++)
                {
                    lineItem.addTax((Tax) ctx.deserialize(invoiceLineTaxInformation.get(i), Tax.class));
                }
            }
            
            if(lineItemObject.has("billingCostCentre")){
                JsonObject billingCostCentre = lineItemObject.get("billingCostCentre").getAsJsonObject();
                lineItem.setBillingCostCentre(billingCostCentre.get("entityIdentification").getAsString());  
            }

            if(lineItemObject.has("shipFrom")){
                lineItem.setOriginInformation((LocationInformation) ctx.deserialize(lineItemObject.get("shipFrom"), LocationInformation.class));
            }
            if(lineItemObject.has("shipTo")){
                lineItem.setDestinationInformation((LocationInformation) ctx.deserialize(lineItemObject.get("shipTo"), LocationInformation.class));
            }

            if(lineItemObject.has("despatchInformation")){
                lineItem.setDespatchInformation((DespatchInformation) ctx.deserialize(lineItemObject.get("despatchInformation"), DespatchInformation.class));
            }

            //Get any custom data sent
            if(lineItemObject.has("avpList")){
                JsonArray avpList = lineItemObject.get("avpList").getAsJsonArray();
                for(int i = 0; i < avpList.size(); i++)
                {
                    lineItem.addEcomAVP((AVP) ctx.deserialize(avpList.get(i).getAsJsonObject(), AVP.class));
                }
            }
            
        }catch(Exception e){
            //TODO: Add error handling
            String error = e.toString();
            String otherError = e.getMessage();
        }
        
        return lineItem;
    }
}
