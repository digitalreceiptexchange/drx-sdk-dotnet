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

public class TaxInfo {
    private string _taxCategory;
    private string _taxCode;
    private double _taxableAmount;
    private double _percentage;
    private double _totalTax;

    public TaxInfo(string taxCategory, string taxCode,
                   double taxableAmount, double percentage,
                   double totalTax)
    {
        _taxCategory = taxCategory;
        _taxCode = taxCode;
        _taxableAmount = taxableAmount;
        _percentage = percentage;
        _totalTax = totalTax;
    }

    public string getTaxCategory() {
        return _taxCategory;
    }

    public void setTaxCategory(string taxCategory) {
        _taxCategory = taxCategory;
    }

    public string getTaxCode() {
        return _taxCode;
    }

    public void setTaxCode(string taxCode) {
        _taxCode = taxCode;
    }

    public double getTaxableAmount() {
        return _taxableAmount;
    }

    public void setTaxableAmount(double taxableAmount) {
        _taxableAmount = taxableAmount;
    }

    public double getPercentage() {
        return _percentage;
    }

    public void setPercentage(double percentage) {
        _percentage = percentage;
    }

    public double getTotalTax() {
        return _totalTax;
    }

    public void setTotalTax(double totalTax) {
        _totalTax = totalTax;
    }

    @Override
    public bool equals(Object obj) {
        if( obj == null) {
            return false;
        }
        if(getClass() != obj.getClass()) {
            return false;
        }
        final TaxInfo other = (TaxInfo)obj;
        if(!_taxCategory.equals(other._taxCategory)) {
            return false;
        }
        if(!_taxCode.equals(other._taxCode)) {
            return false;
        }
        if(_taxableAmount != other._taxableAmount) {
            return false;
        }
        if(_percentage != other._percentage) {
            return false;
        }
        if(_totalTax != other._totalTax) {
            return false;
        }
        return true;
    }

    @Override
    public int hashCode() {
        int result;
        long temp;
        result = _taxCategory != null ? _taxCategory.hashCode() : 0;
        result = 31 * result + (_taxCode != null ? _taxCode.hashCode() : 0);
        temp = Double.doubleToLongBits(_taxableAmount);
        result = 31 * result + (int) (temp ^ (temp >>> 32));
        temp = Double.doubleToLongBits(_percentage);
        result = 31 * result + (int) (temp ^ (temp >>> 32));
        temp = Double.doubleToLongBits(_totalTax);
        result = 31 * result + (int) (temp ^ (temp >>> 32));
        return result;
    }
}
