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

package net.dreceiptx.receipt.common;

import com.google.gson.annotations.SerializedName;

public class Contact {
    @SerializedName("communicationChannelCode") private ContactType _type;
    @SerializedName("communicationValue") private String _value;

    protected Contact() { }

    public Contact(ContactType type, String value)
    {
        _type = type;
        _value = value;
    }

    public ContactType getType() {
        return _type;
    }

    public void setType(ContactType type) {
        _type = type;
    }

    public String getContact() {
        return _value;
    }

    public void setContact(String value) {
        _value = value;
    }
}
