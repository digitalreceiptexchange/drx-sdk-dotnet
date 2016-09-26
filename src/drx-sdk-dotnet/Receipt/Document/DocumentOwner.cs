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

import com.google.gson.annotations.SerializedName;
import java.util.ArrayList;
import java.util.List;

public class DocumentOwner {
    @SerializedName("identifier")  private DocumentOwnerIdentification _identifier;
    @SerializedName("contactInformation") private List<ReceiptContact> _contactInformation;
    public DocumentOwner()
    {
        _identifier = new DocumentOwnerIdentification();
    }
    
    public void setValue(String value) {
        _identifier.setValue(value);
    }
    
    public String getValue() {
       return  _identifier.getValue();
    }

    public DocumentOwnerIdentification getIdentifier() {
        return _identifier;
    }

    public void setIdentifier(DocumentOwnerIdentification identifier) {
        _identifier = identifier;
    }
    
    public void addDocumentOwnerContact(ReceiptContact documentOwnerContact) {
        if(_contactInformation == null){
            _contactInformation = new ArrayList<ReceiptContact>();
        }
        _contactInformation.add(documentOwnerContact);
    }
    
    public List<ReceiptContact> getDocumentOwnerContact() {
        return _contactInformation;
    }
}
