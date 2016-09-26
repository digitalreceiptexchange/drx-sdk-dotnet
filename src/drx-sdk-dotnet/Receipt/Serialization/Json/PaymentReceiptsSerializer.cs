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
import net.dreceiptx.receipt.settlement.PaymentMethodType;
import net.dreceiptx.receipt.settlement.PaymentReceipt;
import com.google.gson.Gson;
import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

public class PaymentReceiptsSerializer implements JsonSerializer<List<PaymentReceipt>>
{
    private final String _dRxGLN;
    private final String _merchantId;
    private final String _userGuid;
    
    public PaymentReceiptsSerializer(String dRxGLN, String merchantId, String userGuid){
        _dRxGLN = dRxGLN;
        _merchantId = merchantId;
        _userGuid = userGuid;
    }
    
    @Override
    public JsonElement serialize(List<PaymentReceipt> paymentReceipts, Type type, JsonSerializationContext jsc) {
        
        JsonArray tree = new JsonArray();
        
        for (PaymentMethodType paymentMethodType : PaymentMethodType.values()) {
            List<Currency> loadedCurrencies = new ArrayList<Currency>();
            
            for (PaymentReceipt paymentReceiptOuter : paymentReceipts) {
                if(paymentReceiptOuter.getPaymentMethodType().equals(paymentMethodType) 
                        && !loadedCurrencies.contains(paymentReceiptOuter.getSettlementCurrency())){
                    JsonArray settlementLineItems = new JsonArray();
                    Double settlementLineItemsTotal = 0.0;
                    for (PaymentReceipt paymentReceiptInner : paymentReceipts) {
                        if(paymentReceiptInner.getSettlementCurrency().equals(paymentReceiptOuter.getSettlementCurrency())){
                            JsonObject settlementLineItem = new JsonObject();
                            settlementLineItem.addProperty("lineItemNumber", paymentReceiptInner.getId());
                            settlementLineItem.add("amountPaid", (JsonObject)new Gson().toJsonTree(
                                    new Amount(paymentReceiptInner.getPaymentAmount(),paymentReceiptOuter.getSettlementCurrency()))
                            );
                            if(paymentReceiptInner.hasPaymentAuthorisation()){
                                JsonObject settlementParty = new JsonObject();
                                settlementParty.addProperty("partyRoleCode", "CENTRAL_PAYMENT_SERVICE");
                                settlementParty.addProperty("value", paymentReceiptInner.getPaymentAuthorisation().getPaymentProvider());
                                settlementLineItem.add("settlementParty", settlementParty);

                                JsonArray transactionalReference = new JsonArray();
                                JsonObject authorisationNumber = new JsonObject();
                                authorisationNumber.addProperty("transactionalReferenceTypeCode", "ANJ");
                                authorisationNumber.addProperty("entityIdentification", paymentReceiptInner.getPaymentAuthorisation().getAuthorisationNumber());
                                JsonObject authorisationCode = new JsonObject();
                                authorisationCode.addProperty("transactionalReferenceTypeCode", "AKO");
                                authorisationCode.addProperty("entityIdentification", paymentReceiptInner.getPaymentAuthorisation().getAuthorisationNumber());
                                transactionalReference.add(authorisationNumber);
                                transactionalReference.add(authorisationCode);
                                settlementLineItem.add("transactionalReference", transactionalReference);
                            }

                            settlementLineItems.add(settlementLineItem);
                            settlementLineItemsTotal =+ paymentReceiptInner.getPaymentAmount();
                        }
                    }
                    
                    JsonObject paymentReceiptObject = new JsonObject();
                    paymentReceiptObject.addProperty("settlementCurrency", paymentReceiptOuter.getSettlementCurrency().getValue());
                    paymentReceiptObject.addProperty("settlementIdentification", paymentMethodType.getValue());
                    paymentReceiptObject.addProperty("totalAmount", settlementLineItemsTotal);
                    
                    JsonObject paymentMethod = new JsonObject();
                    paymentMethod.addProperty("paymentMethodCode", paymentMethodType.getValue());
                    paymentReceiptObject.add("paymentMethod", paymentMethod);
                    
                    paymentReceiptObject.addProperty("totalAmount", settlementLineItemsTotal);
                    paymentReceiptObject.addProperty("settlementHandlingTypeCode", "REMITTANCE_ONLY");
                    
                    JsonObject payee = new JsonObject();
                    payee.addProperty("gln", _merchantId);
                    paymentReceiptObject.add("payee", payee);
                    
                    JsonObject payer = new JsonObject();
                    payer.addProperty("gln", _dRxGLN);
                    JsonObject additionalPartyIdentification = new JsonObject();
                    additionalPartyIdentification.addProperty("Authority", "dRX");
                    additionalPartyIdentification.addProperty("value", _userGuid);
                    payer.add("additionalPartyIdentification", additionalPartyIdentification);
                    paymentReceiptObject.add("payer", payer);
                    
                    paymentReceiptObject.add("settlementLineItem", settlementLineItems);
                    
                    tree.add(paymentReceiptObject);
                    loadedCurrencies.add(paymentReceiptOuter.getSettlementCurrency());
                }
            }
        }
        
        return tree;
    }
}
