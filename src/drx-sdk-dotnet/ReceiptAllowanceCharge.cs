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
    //public class ReceiptAllowanceCharge
    //{
    //    protected ReceiptAllowanceCharge()
    //    { }

    //    public ReceiptAllowanceCharge(string allowanceOrCharge, string type,
    //        string settlement, decimal amount, string description,
    //        TaxFee tax)
    //    {
    //        AllowanceOrCharge = allowanceOrCharge;
    //        Type = type;
    //        Settlement = settlement;
    //        Amount = amount;
    //        Description = description;
    //        Tax = tax;
    //    }

    //    public string AllowanceOrCharge { get; set; }
    //    public string Type { get; set; }
    //    public string Settlement { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Description { get; set; }
    //    public TaxFee Tax { get; set; }
    //    public int ReceiptAllowanceChargeId { get; set; }

    //    public decimal TaxAmount
    //    {
    //        get { return Tax?.TaxAmount ?? 0; }
    //    }

    //    protected bool Equals(ReceiptAllowanceCharge other)
    //    {
    //        return string.Equals(AllowanceOrCharge, other.AllowanceOrCharge)
    //            && string.Equals(Type, other.Type)
    //            && string.Equals(Settlement, other.Settlement)
    //            && Amount == other.Amount
    //            && string.Equals(Description, other.Description)
    //            && Equals(Tax, other.Tax)
    //            && ReceiptAllowanceChargeId == other.ReceiptAllowanceChargeId;
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (ReferenceEquals(null, obj)) return false;
    //        if (ReferenceEquals(this, obj)) return true;
    //        if (obj.GetType() != GetType()) return false;
    //        return Equals((ReceiptAllowanceCharge)obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        unchecked
    //        {
    //            var hashCode = AllowanceOrCharge?.GetHashCode() ?? 0;
    //            hashCode     = (hashCode * 397) ^ (Type?.GetHashCode() ?? 0);
    //            hashCode     = (hashCode * 397) ^ (Settlement?.GetHashCode() ?? 0);
    //            hashCode     = (hashCode * 397) ^ Amount.GetHashCode();
    //            hashCode     = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
    //            hashCode     = (hashCode * 397) ^ (Tax?.GetHashCode() ?? 0);
    //            hashCode     = (hashCode * 397) ^ ReceiptAllowanceChargeId;
    //            return hashCode;
    //        }
    //    }

    //}
}