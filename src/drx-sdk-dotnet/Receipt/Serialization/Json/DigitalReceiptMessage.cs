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

import net.dreceiptx.receipt.document.StandardBusinessDocumentHeader;
import net.dreceiptx.receipt.invoice.Invoice;
import net.dreceiptx.receipt.settlement.PaymentReceipt;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class DigitalReceiptMessage {
    @SerializedName("standardBusinessDocumentHeader")
    private StandardBusinessDocumentHeader _standardBusinessDocumentHeader;
    @SerializedName("invoice")
    private Invoice _invoice;
    @SerializedName("paymentReceipts")
    private List<PaymentReceipt> _paymentReceipts;
    
    public DigitalReceiptMessage(){
        _invoice = new Invoice();
    }
    
    public void setInvoice(Invoice invoice) {
        _invoice = invoice;
    }
    
    public Invoice getInvoice() {
        return _invoice;
    }
    
    public void setStandardBusinessDocumentHeader(StandardBusinessDocumentHeader standardBusinessDocumentHeader) {
        _standardBusinessDocumentHeader = standardBusinessDocumentHeader;
    }
    
    public StandardBusinessDocumentHeader getStandardBusinessDocumentHeader() {
        return _standardBusinessDocumentHeader;
    }
    
    public void setPaymentReceipts(List<PaymentReceipt> paymentReceipts) {
        _paymentReceipts = paymentReceipts;
    }
    
    public List<PaymentReceipt> getPaymentReceipts() {
        return _paymentReceipts;
    }
}
