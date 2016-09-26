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

import net.dreceiptx.receipt.common.Contact;
import net.dreceiptx.receipt.common.ContactType;
import com.google.gson.annotations.SerializedName;
import java.util.List;

public class ReceiptContact {
    @SerializedName("contactTypeCode") private ReceiptContactType _receiptContactType;
    @SerializedName("personName") private String _contact;
    @SerializedName("communicationChannelCode") private List<Contact> _contacts;
    
    public ReceiptContact(ReceiptContactType merchantContactType)
    {
        _receiptContactType = merchantContactType;
    }

    public ReceiptContact(ReceiptContactType merchantContactType, String contactName)
    {
        _receiptContactType = merchantContactType;
        _contact = contactName;
    }
    
    /**
     * Gets the ContactTypeIdentifier. Role of the identifier.
     * Example: EDI co-ordinator
     * @return
     */
    public ReceiptContactType getReceiptContactType() {
        return _receiptContactType;
    }

    /**
     *
     * Sets the DocumentOwnerContactType. Role of the identifier.
     * Example: SALES_ADMINISTRATION
     * @param receiptContactType
     */
    public void setReceiptContactType(ReceiptContactType receiptContactType) {
        _receiptContactType = receiptContactType;
    }
    
    public String getContactName() {
        return _contact;
    }

    public void setContactName(String contact) {
        _contact = contact;
    }

    /**
     * Gets the EmailAddress. The EmailAddress, although optional, SHOULD be used, if possible.
     * @return
     */
    public String getEmailAddress() {
        for (Contact contact : _contacts) {
            if(contact.getType().equals(ContactType.EMAIL))
                return contact.getContact();
        }
        return null;
    }

    /**
     * Sets the EmailAddress. The EmailAddress, although optional, SHOULD be used, if possible.
     * @param emailAddress
     */
    public void addEmailAddress(String emailAddress) {
        _contacts.add(new Contact(ContactType.EMAIL, emailAddress));
    }

    /**
     * Gets and sets the Name of contact person or department.
     * Example: Delysha Burnet
     * @return
     */
    public String getContact() {
        return _contact;
    }

    /**
     * Gets the TelephoneNumber. A number format agreed upon between the Sender
     * and Receiver SHOULD be used. Number format expressed using [RFC3966].
     * The tel URI for Telephone Numbers? MAY be used.
     * @return
     */
    public String getTelephoneNumber() {
        for (Contact contact : _contacts) {
            if(contact.getType().equals(ContactType.TELEPHONE))
                return contact.getContact();
        }
        return null;
    }

    /**
     * Sets the TelephoneNumber. A number format agreed upon between the Sender
     * and Receiver SHOULD be used. Number format expressed using [RFC3966].
     * The tel URI for Telephone Numbers? MAY be used.
     * @param telephoneNumber
     */
    public void addTelephoneNumber(String telephoneNumber) {
        _contacts.add(new Contact(ContactType.TELEPHONE, telephoneNumber));
    }
}
