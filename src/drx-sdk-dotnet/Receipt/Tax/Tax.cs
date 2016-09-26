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

namespace Net.Dreceiptx.Receipt.Tax
{
    public class Tax
    {
        public Tax(double taxableAmount, double totalTax)
        {
            TaxableAmount = taxableAmount;
            TaxTotal = totalTax;
            TaxRate = (TaxTotal/TaxableAmount)*100;
        }

        public Tax(TaxCategory category, TaxCode code, double taxableAmount, double totalTax)
        {
            TaxCategory = category;
            TaxCode = code;
            TaxableAmount = taxableAmount;
            TaxTotal = totalTax;
            TaxRate = (TaxTotal/TaxableAmount)*100;
        }

        //@SerializedName("dutyFeeTaxCategoryCode")
        public TaxCategory? TaxCategory { get; set; } = null;

        //@SerializedName("dutyFeeTaxTypeCode")
        public TaxCode? TaxCode { get; set; } = null;

        //@SerializedName("dutyFeeTaxPercentage")
        public double TaxRate { get; set; }

        //@SerializedName("dutyFeeTaxBasisAmount")
        public double TaxableAmount { get; }

        //@SerializedName("dutyFeeTaxAmount")
        public double TaxTotal { get; set; }

        public bool IsTaxCode(TaxCode taxCode)
        {
            return taxCode == TaxCode;
        }
    }
}