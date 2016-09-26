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
package net.dreceiptx.receipt.ecom;

import com.google.gson.annotations.SerializedName;
import net.dreceiptx.receipt.common.Address;
import net.dreceiptx.receipt.common.Contact;
import net.dreceiptx.receipt.common.GeographicalCoordinates;

import java.util.ArrayList;
import java.util.List;

public class AVP {
    @SerializedName("attributeName") private string _name = null;
    @SerializedName("qualifierCodeList") private string _codeList = null;
    @SerializedName("qualifierCodeListVersion") private string _codeListVersion = null;
    @SerializedName("qualifierCodeName") private string _codeName = null;
    @SerializedName("value") private string _value = null;

    public AVP(string _name, string _value) {
        this._name = _name;
        this._value = _value;
    }

    public string getAttributeName() {
        return _name;
    }

    public void setAttributeName(string _name) {
        this._name = _name;
    }

    public string getCodelist() {
        return _codeList;
    }

    public void setCodelist(string _Codelist) {
        this._codeList = _Codelist;
    }

    public string getCodelistVersion() {
        return _codeListVersion;
    }

    public void setCodelistVersion(string _CodelistVersion) {
        this._codeListVersion = _CodelistVersion;
    }

    public string getCodeName() {
        return _codeName;
    }

    public void setCodeName(string _CodeName) {
        this._codeName = _CodeName;
    }

    public string getValue() {
        return _value;
    }

    public void setValue(string _value) {
        this._value = _value;
    }
}
