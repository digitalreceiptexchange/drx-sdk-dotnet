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

public class Tax {
    @SerializedName("dutyFeeTaxCategoryCode") private TaxCategory _category  = null;
    @SerializedName("dutyFeeTaxTypeCode") private TaxCode _code = null;
    @SerializedName("dutyFeeTaxBasisAmount") private double _taxableAmount;
    @SerializedName("dutyFeeTaxPercentage") private double _rate;
    @SerializedName("dutyFeeTaxAmount") private double _totalTax;

    public Tax(double taxableAmount, double totalTax)
    {
        _taxableAmount = taxableAmount;
        _totalTax = totalTax;
        _rate = (_totalTax/_taxableAmount)*100;
    }

    public Tax(TaxCategory category, TaxCode code, double taxableAmount, double totalTax)
    {
        _category = category;
        _code = code;
        _taxableAmount = taxableAmount;
        _totalTax = totalTax;
        _rate = (_totalTax/_taxableAmount)*100;
    }
    
    public void setTaxCategory(TaxCategory taxCategory){
        _category = taxCategory;
    }
    
    public void setTaxCode(TaxCode taxCode){
        _code = taxCode;
    }
    
    public double getTaxRate() {
        return _rate;
    }
    
    public double getTaxableAmount() {
        return _taxableAmount;
    }

    public double getTaxTotal() {
        return _totalTax;
    }

    public void setTaxTotal(double taxTotal) {
        _totalTax = taxTotal;
    }
    
    public TaxCategory getTaxCategory() {
        return _category;
    }
    
    public TaxCode getTaxCode() {
        return _code;
    }
    
    public bool is(TaxCode taxCode){
        return (taxCode == _code);
    }
}
