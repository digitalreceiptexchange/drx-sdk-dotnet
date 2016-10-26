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

using System;

namespace Net.Dreceiptx.Receipt.Tax
{
    public class TaxInfo
    {
        public TaxInfo(string taxCategory, string taxCode,
            decimal taxableAmount, decimal percentage,
            decimal totalTax)
        {
            Category = taxCategory;
            TaxCode = taxCode;
            TaxableAmount = taxableAmount;
            Percentage = percentage;
            TotalTax = totalTax;
        }

        public string Category { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal Percentage { get; set; }
        public decimal TotalTax { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TaxInfo other = obj as TaxInfo;
            if (other == null)
            {
                return false;
            }
            
            if (Category != other.Category)
            {
                return false;
            }
            if (TaxCode != other.TaxCode)
            {
                return false;
            }
            if (TaxableAmount != other.TaxableAmount)
            {
                return false;
            }
            if (Percentage != other.Percentage)
            {
                return false;
            }
            if (TotalTax != other.TotalTax)
            {
                return false;
            }
            return true;
        }

        
        public override int GetHashCode()
        {
            int result;
            long temp;
            result = Category?.GetHashCode() ?? 0;
            result = 31*result + (TaxCode?.GetHashCode() ?? 0);
            temp = TaxableAmount.GetHashCode();
            result = 31*result + (int) (temp ^ (temp >> 32));
            temp = Percentage.GetHashCode();
            result = 31*result + (int) (temp ^ (temp >> 32));
            temp = TotalTax.GetHashCode();
            result = 31*result + (int) (temp ^ (temp >> 32));
            return result;
        }
    }
}