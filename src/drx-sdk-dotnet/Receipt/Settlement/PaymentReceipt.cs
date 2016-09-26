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
package net.dreceiptx.receipt.settlement;

import net.dreceiptx.receipt.common.Currency;

public class PaymentReceipt {
    private int _id;
    private PaymentMethodType _paymentMethodType;
    private double _paymentAmount;
    private Currency _settlementCurrency;
    private PaymentAuthorisation _paymentAuthorisation;

    public PaymentReceipt(PaymentMethodType paymentMethodCode, Double paymentAmount) {
        _paymentMethodType = paymentMethodCode;
        _paymentAmount = paymentAmount;
    }

    public PaymentReceipt(PaymentMethodType paymentMethodCode, Double paymentAmount,Currency settlementCurrency) {
        this(paymentMethodCode, paymentAmount);
        _settlementCurrency = settlementCurrency;
        _paymentAuthorisation = null;
    }
    
    public void setId(int id){
        _id = id;
    }
    
    public int getId(){
        return _id;
    }

    public PaymentMethodType getPaymentMethodType() {
        return _paymentMethodType;
    }

    public void setPaymentMethodType(PaymentMethodType paymentMethodType) {
        _paymentMethodType = paymentMethodType;
    }

    public double getPaymentAmount() {
        return _paymentAmount;
    }

    public void setPaymentAmount(double paymentAmount) {
        _paymentAmount = paymentAmount;
    }
    
    public Currency getSettlementCurrency() {
        return _settlementCurrency;
    }

    public void setSettlementCurrency(Currency settlementCurrency) {
        _settlementCurrency = settlementCurrency;
    }
    
    public bool hasPaymentAuthorisation() {
        return !(_paymentAuthorisation == null);
    }
    
    public PaymentAuthorisation getPaymentAuthorisation() {
        return _paymentAuthorisation;
    }

    public void setPaymentAuthorisation(PaymentAuthorisation paymentAuthorisation) {
        _paymentAuthorisation = paymentAuthorisation;
    }
}