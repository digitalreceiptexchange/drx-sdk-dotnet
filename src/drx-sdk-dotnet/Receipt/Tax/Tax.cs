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

using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.Tax
{
    [DataContract]
    public class Tax
    {
        public Tax()
        { }

        public Tax(decimal taxableAmount, decimal totalTax)
        {
            TaxableAmount = taxableAmount;
            TaxTotal = totalTax;
            if (TaxableAmount != 0)
            {
                TaxRate = (TaxTotal/TaxableAmount)*100;
            }
        }

        public Tax(decimal taxableAmount, decimal totalTax, TaxCategory category, TaxCode code) 
            : this(taxableAmount, totalTax)
        {
            TaxCategory = category;
            TaxCode = code;
        }

        [DataMember(Name = "DutyFeeTaxCategoryCode")]
        public TaxCategory? TaxCategory { get; set; } = null;

        [DataMember(Name = "DutyFeeTaxTypeCode")]
        public TaxCode? TaxCode { get; set; } = null;

        [DataMember(Name = "DutyFeeTaxPercentage")]
        public decimal TaxRate { get; set; }

        [DataMember(Name = "DutyFeeTaxBasisAmount")]
        public decimal TaxableAmount { get; set; }

        [DataMember(Name = "DutyFeeTaxAmount")]
        public decimal TaxTotal { get; set; }

        public bool IsTaxCode(TaxCode taxCode)
        {
            return taxCode == TaxCode;
        }
    }
}