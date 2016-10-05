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

namespace Net.Dreceiptx
{
    //public class TaxInfo : IEquatable<TaxInfo>
    //{
    //    public TaxCategory TaxCategory { get; set; }
    //    public TaxCode TaxCode { get; set; }
    //    public decimal TaxableAmount { get; set; }
    //    public decimal Percentage { get; set; }
    //    public decimal TotalTax { get; set; }

    //    public TaxInfo(TaxCategory taxCategory, TaxCode taxCode, 
    //        decimal taxableAmount, decimal percentage,
    //        decimal totalTax)
    //    {
    //        TaxCategory = taxCategory;
    //        TaxCode = taxCode;
    //        TaxableAmount = taxableAmount;
    //        Percentage = percentage;
    //        TotalTax = totalTax;
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (ReferenceEquals(null, obj)) return false;
    //        if (ReferenceEquals(this, obj)) return true;
    //        if (obj.GetType() != GetType()) return false;
    //        return Equals(obj as TaxInfo);
    //    }

    //    public bool Equals(TaxInfo taxInfo)
    //    {
    //        if (taxInfo == null)
    //        {
    //            return false;
    //        }
    //        bool equals = TaxCategory.Equals(taxInfo.TaxCategory);
    //        equals &= TaxCode.Equals(taxInfo.TaxCode);
    //        equals &= TaxableAmount == taxInfo.TaxableAmount;
    //        equals &= Percentage == taxInfo.Percentage;
    //        equals &= TotalTax == taxInfo.TotalTax;
    //        return equals;
    //    }

    //    public override int GetHashCode()
    //    {
    //        unchecked
    //        {
    //            int hash = 17;
    //            hash = (13*hash) + TaxCategory.GetHashCode();
    //            hash = (13 * hash) + TaxCode.GetHashCode();
    //            hash = (13 * hash) + TaxableAmount.GetHashCode();
    //            hash = (13 * hash) + Percentage.GetHashCode();
    //            hash = (13 * hash) + TotalTax.GetHashCode();
    //            return hash;
    //        }
            
    //    }
    //}

    //public enum TaxCategory : short
    //{
    //    APPLICABLE,
    //    DOMESTIC_REVERSE_CHARGE,
    //    EXEMPT,
    //    FREE_EXPORT_ITEM,
    //    HIGH,
    //    LOW,
    //    MEDIUM,
    //    MIXED,
    //    NOT_APPLICABLE,
    //    PREPAID,
    //    SERVICES_OUTSIDE_SCOPE_OF_TAX,
    //    STANDARD,
    //    VALUE_ADDED_TAX_NOT_NOW_DUE_FOR_PAYMENT,
    //    ZERO
    //}

    //public enum TaxCode : short
    //{
    //    AAD,
    //    AAF,
    //    AAJ,
    //    ACT,
    //    ENV,
    //    CAR,
    //    EXC,
    //    GST,
    //    IMP,
    //    OIL,
    //    OTH,
    //    VAT
    //}
}
