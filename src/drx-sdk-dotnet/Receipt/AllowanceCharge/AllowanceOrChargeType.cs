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

package net.dreceiptx.receipt.allowanceCharge;

import com.google.gson.annotations.SerializedName;

public enum AllowanceOrChargeType {
    @SerializedName("ALLOWANCE")
    ALLOWANCE("ALLOWANCE", "Allowance"),
    @SerializedName("CHARGE")
    CHARGE("CHARGE", "Charge");

    private String _value;

    private String _displayName;

    AllowanceOrChargeType(String value, String name) {
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
}
