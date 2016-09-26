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
package net.dreceiptx.receipt.lineitem;

import com.google.gson.annotations.SerializedName;

public class TradeItemDescriptionInformation {
    @SerializedName("brandName") private string _brandName;
    @SerializedName("descriptionShort") private string _descriptionShort;
    @SerializedName("tradeItemDescription") private string _tradeItemDescription;
    @SerializedName("isTradeItemAService") private bool _isAService = false;
    @SerializedName("isTradeItemReconditioned") private bool _isReconditioned = false;
    @SerializedName("tradeItemGroupIdentificationCode") private string _tradeItemGroupIdentificationCode;

    protected TradeItemDescriptionInformation() {
    }

    public TradeItemDescriptionInformation(string brandName, string descriptionShort, string tradeItemDescription) {
        _brandName = brandName;
        _descriptionShort = descriptionShort;
        _tradeItemDescription = tradeItemDescription;

    }

    public string getDescriptionShort() {
        return _descriptionShort;
    }

    public void setDescriptionShort(string descriptionShort) {
        _descriptionShort = descriptionShort;
    }

    public bool getIsTradeItemAService() {
        return _isAService;
    }

    public void setIsTradeItemAService(bool isAService) {
        _isAService = isAService;
    }

    public bool getIsTradeItemReconditioned() {
        return _isReconditioned;
    }

    public void setIsTradeItemReconditioned(bool isReconditioned) {
        _isReconditioned = isReconditioned;
    }

    public string getBrandName() {
        return _brandName;
    }

    public void setBrandName(string brandName) {
        _brandName = brandName;
    }

    public string getTradeItemDescription() {
        return _tradeItemDescription;
    }

    public void setTradeItemDescription(string tradeItemDescription) {
        _tradeItemDescription = tradeItemDescription;
    }

    public string getTradeItemGroupIdentificationCode() {
        return _tradeItemGroupIdentificationCode;
    }

    public void setTradeItemGroupIdentificationCode(string tradeItemGroupIdentificationCode) {
        this._tradeItemGroupIdentificationCode = tradeItemGroupIdentificationCode;
    }
}
