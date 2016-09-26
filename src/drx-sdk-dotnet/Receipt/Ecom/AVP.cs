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

package net.dreceiptx.receipt.ecom;

import com.google.gson.annotations.SerializedName;
import net.dreceiptx.receipt.common.Address;
import net.dreceiptx.receipt.common.Contact;
import net.dreceiptx.receipt.common.GeographicalCoordinates;

import java.util.ArrayList;
import java.util.List;

public class AVP {
    @SerializedName("attributeName") private String _name = null;
    @SerializedName("qualifierCodeList") private String _codeList = null;
    @SerializedName("qualifierCodeListVersion") private String _codeListVersion = null;
    @SerializedName("qualifierCodeName") private String _codeName = null;
    @SerializedName("value") private String _value = null;

    public AVP(String _name, String _value) {
        this._name = _name;
        this._value = _value;
    }

    public String getAttributeName() {
        return _name;
    }

    public void setAttributeName(String _name) {
        this._name = _name;
    }

    public String getCodelist() {
        return _codeList;
    }

    public void setCodelist(String _Codelist) {
        this._codeList = _Codelist;
    }

    public String getCodelistVersion() {
        return _codeListVersion;
    }

    public void setCodelistVersion(String _CodelistVersion) {
        this._codeListVersion = _CodelistVersion;
    }

    public String getCodeName() {
        return _codeName;
    }

    public void setCodeName(String _CodeName) {
        this._codeName = _CodeName;
    }

    public String getValue() {
        return _value;
    }

    public void setValue(String _value) {
        this._value = _value;
    }
}
