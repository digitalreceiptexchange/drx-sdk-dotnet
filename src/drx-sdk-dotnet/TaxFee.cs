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
namespace Net.Dreceiptx
{
    public class TaxFee
    {
        protected TaxFee()
        {
        }

        public TaxFee(decimal taxAmount)
        {
            TaxAmount = taxAmount;
        }

        public decimal TaxAmount { get; set; }

        public override bool Equals(object obj)
        {
            TaxFee taxInfo = (TaxFee)obj;
            return Equals(taxInfo);
        }

        public bool Equals(TaxFee taxFee)
        {
            if (taxFee == null)
            {
                return false;
            }
            bool equals = TaxAmount.Equals(taxFee.TaxAmount);
            return equals;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (13 * hash) + TaxAmount.GetHashCode();
                return hash;
            }

        }
    }
}