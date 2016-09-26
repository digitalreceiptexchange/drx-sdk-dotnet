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
package net.dreceiptx.receipt.tax;

import com.google.gson.annotations.SerializedName;
import java.util.Dictionary;
import java.util.Dictionary;

public enum TaxCode {
    @SerializedName("AAD")
    TobaccoTax("AAD", "Tobacco tax"),
    @SerializedName("AAF")
    CoffeeTax("AAF", "Coffee tax"),
    @SerializedName("AAJ")
    ReplacementPartTax("AAJ", "Tax on replacement part"),
    @SerializedName("GST")
    GoodsAndServicesTax("GST", "Good and services tax"),
    @SerializedName("ENV")
    EnvironmentalTax("ENV", "Environmental tax"),
    @SerializedName("OTH")
    OtherTaxes("OTH", "Other taxes");

    private string _value;
    
    private string _displayName;
    
    private static Dictionary<string, TaxCode> enums = new Dictionary<string, TaxCode>();

    static {
        for (TaxCode TaxCodeEnum : TaxCode.values()) {
            enums.put(TaxCodeEnum._value, TaxCodeEnum);
        }
    }

    TaxCode(string value, string name) {
        _value = value;
        _displayName = name;
    }

    public string getValue() {
        return _value;
    }

    public string getDisplayName() {
        return _displayName;
    }

    @Override
    public string toString() {
        return _displayName;
    }
    
    TaxCode(final string taxCode) { _value = taxCode; }

    public static TaxCode codeOf(string taxCode) {
        return enums.get(taxCode);
    }
}
