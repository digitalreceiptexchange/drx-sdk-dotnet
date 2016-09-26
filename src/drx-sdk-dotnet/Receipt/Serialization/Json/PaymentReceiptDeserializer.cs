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
import net.dreceiptx.receipt.settlement.PaymentAuthorisation;
import net.dreceiptx.receipt.settlement.PaymentMethodType;
import net.dreceiptx.receipt.settlement.PaymentReceipt;
import com.google.gson.JsonArray;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParseException;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

public class PaymentReceiptDeserializer implements JsonDeserializer<List<PaymentReceipt>>
{
    
    @Override
    public List<PaymentReceipt> deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext ctx) throws JsonParseException
    {
        List<PaymentReceipt> paymentReceipts = new ArrayList<>();
        JsonArray paymentRecieptsJson = (JsonArray) json;
        
        for(int oi = 0; oi < paymentRecieptsJson.size(); oi++)
        {
            JsonObject paymentReceiptJson = paymentRecieptsJson.get(oi).getAsJsonObject();
            PaymentMethodType paymentMethodType = PaymentMethodType.valueOf(paymentReceiptJson.get("settlementIdentification").getAsString());
            Currency settlementCurrency = Currency.codeOf(paymentReceiptJson.get("settlementCurrency").getAsString());
            JsonArray settlementLineItemsJson = new JsonArray();
            if(paymentReceiptJson.has("settlementLineItem")){
                settlementLineItemsJson = paymentReceiptJson.get("settlementLineItem").getAsJsonArray();
            }

            for(int ii = 0; ii < settlementLineItemsJson.size(); ii++)
            {
                JsonObject settlementLineItemJson = settlementLineItemsJson.get(ii).getAsJsonObject();
                JsonObject settlementLineItemAmountJson = settlementLineItemJson.get("amountPaid").getAsJsonObject();
                PaymentReceipt paymentReceipt = new PaymentReceipt(paymentMethodType, settlementLineItemAmountJson.get("value").getAsDouble());
                paymentReceipt.setSettlementCurrency(settlementCurrency);
                if(settlementLineItemJson.has("settlementParty") && settlementLineItemJson.has("transactionalReference")){
                    JsonObject settlementPartyJson = settlementLineItemJson.get("settlementParty").getAsJsonObject();
                    JsonArray transactionalReferences = settlementLineItemJson.get("transactionalReference").getAsJsonArray();
                    
                    String authorisationNumber = "Undefined";
                    String authorisationCode = "Undefined";
                    for(int tri = 0; tri < transactionalReferences.size(); tri++)
                    {
                        JsonObject transactionalReference = transactionalReferences.get(tri).getAsJsonObject();
                        switch (transactionalReference.get("transactionalReferenceTypeCode").getAsString()) {
                            case "ANJ" :  
                                authorisationNumber = transactionalReference.get("entityIdentification").getAsString();
                                break;
                            case "AKO" :  
                                authorisationCode = transactionalReference.get("entityIdentification").getAsString();
                        }
                    }
                    
                    PaymentAuthorisation paymentAuthorisation = new PaymentAuthorisation(
                            settlementPartyJson.get("value").getAsString(),
                            authorisationNumber,
                            authorisationCode
                    );
                    
                    paymentReceipt.setPaymentAuthorisation(paymentAuthorisation);
                }
                
                paymentReceipts.add(paymentReceipt);
            }
        }
        
        return paymentReceipts;
    }
}
