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

package net.dreceiptx.receipt.lineitem;

import com.google.gson.annotations.SerializedName;

public class TradeItemDescriptionInformation {
    @SerializedName("brandName") private String _brandName;
    @SerializedName("descriptionShort") private String _descriptionShort;
    @SerializedName("tradeItemDescription") private String _tradeItemDescription;
    @SerializedName("isTradeItemAService") private boolean _isAService = false;
    @SerializedName("isTradeItemReconditioned") private boolean _isReconditioned = false;
    @SerializedName("tradeItemGroupIdentificationCode") private String _tradeItemGroupIdentificationCode;

    protected TradeItemDescriptionInformation() {
    }

    public TradeItemDescriptionInformation(String brandName, String descriptionShort, String tradeItemDescription) {
        _brandName = brandName;
        _descriptionShort = descriptionShort;
        _tradeItemDescription = tradeItemDescription;

    }

    public String getDescriptionShort() {
        return _descriptionShort;
    }

    public void setDescriptionShort(String descriptionShort) {
        _descriptionShort = descriptionShort;
    }

    public boolean getIsTradeItemAService() {
        return _isAService;
    }

    public void setIsTradeItemAService(boolean isAService) {
        _isAService = isAService;
    }

    public boolean getIsTradeItemReconditioned() {
        return _isReconditioned;
    }

    public void setIsTradeItemReconditioned(boolean isReconditioned) {
        _isReconditioned = isReconditioned;
    }

    public String getBrandName() {
        return _brandName;
    }

    public void setBrandName(String brandName) {
        _brandName = brandName;
    }

    public String getTradeItemDescription() {
        return _tradeItemDescription;
    }

    public void setTradeItemDescription(String tradeItemDescription) {
        _tradeItemDescription = tradeItemDescription;
    }

    public String getTradeItemGroupIdentificationCode() {
        return _tradeItemGroupIdentificationCode;
    }

    public void setTradeItemGroupIdentificationCode(String tradeItemGroupIdentificationCode) {
        this._tradeItemGroupIdentificationCode = tradeItemGroupIdentificationCode;
    }
}
