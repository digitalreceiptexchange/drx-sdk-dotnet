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

package net.dreceiptx.receipt.tax;

import com.google.gson.annotations.SerializedName;
import java.util.HashMap;
import java.util.Map;

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

    private String _value;
    
    private String _displayName;
    
    private static Map<String, TaxCode> enums = new HashMap<String, TaxCode>();

    static {
        for (TaxCode TaxCodeEnum : TaxCode.values()) {
            enums.put(TaxCodeEnum._value, TaxCodeEnum);
        }
    }

    TaxCode(String value, String name) {
        _value = value;
        _displayName = name;
    }

    public String getValue() {
        return _value;
    }

    public String getDisplayName() {
        return _displayName;
    }

    @Override
    public String toString() {
        return _displayName;
    }
    
    TaxCode(final String taxCode) { _value = taxCode; }

    public static TaxCode codeOf(String taxCode) {
        return enums.get(taxCode);
    }
}
