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
package net.dreceiptx.receipt.document;

import com.google.gson.annotations.SerializedName;

public class DocumentOwnerIdentification {
    @SerializedName("authority")  private string _authority;
    @SerializedName("value")  private string _value;

    /**
     * Gets and sets the Authority agency of the identification key.
     * The "Authority" attribute, will be either set to GS1 or dRx
     */
    public string getAuthority() {
        return _authority;
    }

    /**
     * Sets the Authority agency of the identification key
     * @param authority
     */
    public void setAuthority(string authority) {
        _authority = authority;
    }

    public string getValue() {
        return _value;
    }

    public void setValue(string value) {
        _value = value;
    }
}
