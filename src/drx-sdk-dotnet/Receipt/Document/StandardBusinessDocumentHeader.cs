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

package net.dreceiptx.receipt.document;

import net.dreceiptx.receipt.validation.ReceiptValidation;
import java.util.ArrayList;
import java.util.List;
import com.google.gson.annotations.SerializedName;
import net.dreceiptx.receipt.validation.ValidationErrors;

public class StandardBusinessDocumentHeader {
    @SerializedName("sender") private List<DocumentOwner> _sender;
    @SerializedName("receiver") private List<DocumentOwner> _receiver;
    @SerializedName("documentIdentification") private DocumentIdentification _documentIdentification;
    
    public StandardBusinessDocumentHeader()
    {
        _sender = new ArrayList<DocumentOwner>();
        _receiver = new ArrayList<DocumentOwner>();
        _documentIdentification = new DocumentIdentification();
        
        DocumentOwner merchant = new DocumentOwner();
        merchant.getIdentifier().setAuthority("GS1");
        merchant.getIdentifier().setValue(null);
        _sender.add(0, merchant);
        
        DocumentOwner dRx = new DocumentOwner();
        dRx.getIdentifier().setAuthority("GS1");
        dRx.getIdentifier().setValue(null);
        _receiver.add(0, dRx);
        
        DocumentOwner user = new DocumentOwner();
        user.getIdentifier().setAuthority("dRx");
        user.getIdentifier().setValue(null);
        _receiver.add(1, user);
    }

    public void setMerchantGLN(String merchantGLN) {
        _sender.get(0).setValue(merchantGLN);
    }
    
    public String getMerchantGLN() {
        return _sender.get(0).getValue();
    }
    
    public void setdRxGLN(String dRxGLN) {
        _receiver.get(0).setValue(dRxGLN);
    }
    
    public String getdRxGLN() {
        return _receiver.get(0).getValue();
    }
    
    public void setUserIdentifier(String userIdentifier) {
        _receiver.get(1).setValue(userIdentifier);
    }
    
    public String getUserIdentifier() {
        return _receiver.get(1).getValue();
    }
    
    public List<ReceiptContact> getClientContacts() {
        return _receiver.get(1).getDocumentOwnerContact();
    }
    
    public void addMerchantContact(ReceiptContact contact) {
        _sender.get(0).addDocumentOwnerContact(contact);
    }
    
    public void addRMSContact(ReceiptContact contact) {
        _receiver.get(1).addDocumentOwnerContact(contact);
    }

    public List<DocumentOwner> getReceiver() {
        return _receiver;
    }
    
    public List<DocumentOwner> getSender() {
        return _sender;
    }

    public void addReceiver(DocumentOwner receiver) {
        _receiver.add(receiver);
    }

    public DocumentIdentification getDocumentIdentification() {
        return _documentIdentification;
    }

    public void setDocumentIdentification(DocumentIdentification documentIdentification) {
        _documentIdentification = documentIdentification;
    }
    
    public ReceiptValidation validate(ReceiptValidation receiptValidation) {
        if (_sender.isEmpty()) {
            receiptValidation.AddError(ValidationErrors.MerchantGLNMustBeSet);
        }

        return receiptValidation;

    }
}
