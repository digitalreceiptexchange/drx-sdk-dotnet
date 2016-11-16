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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt.AllowanceCharge
{
    [DataContract]
    public class ReceiptAllowanceCharge
    {
        //TODO: ID should not be serialized
        
        private int _id;
        //TODO: If we keep it this should not be a double
        public double Id => _id;

        [DataMember]
        public AllowanceOrChargeType AllowanceOrChargeType { get; set; }
        [DataMember]
        public AllowanceChargeType AllowanceChargeType { get; set; }

        [DataMember]
        public decimal BaseAmount { get; set; }

        [DataMember(Name = "LeviedDutyFeeTax")]
        private List<Tax.Tax> _taxes = new List<Tax.Tax>();

        [DataMember]
        public SettlementType SettlementType { get; set; }

        public decimal SubTotal => BaseAmount;

        [DataMember(Name = "AllowanceChargeDescription")]
        public string Description { get; set; }

        public decimal Total => BaseAmount + TaxesTotal;

        public decimal NetTotal => BaseAmount;

        public bool HasTaxes => _taxes.Count != 0;

        public decimal TaxesTotal
        {
            get
            {
                decimal totalTaxes = 0.0m;
                foreach (Tax.Tax tax in _taxes)
                {
                    totalTaxes = +tax.TaxTotal;
                }
                return totalTaxes;
            }
        }

        /// <summary>
        /// Calculates the TaxesTotal for the given TaxCode
        /// </summary>
        /// <param name="taxCode">to filter the taxes</param>
        /// <returns>TotalTaxes for the given TaxCode if they exist otherwise 0</returns>
        public decimal TaxesTotalByTaxCode(TaxCode taxCode)
        {
            decimal totalTaxes = 0.0m;
            foreach (Tax.Tax tax in _taxes)
            {
                if (tax.TaxCode == taxCode)
                {
                    totalTaxes =+ tax.TaxTotal;
                }
            }
            return totalTaxes;
        }

        public List<Tax.Tax> Taxes => _taxes;

        public bool IsCharge => AllowanceOrChargeType == AllowanceOrChargeType.CHARGE;

        public bool IsAllowance => AllowanceOrChargeType == AllowanceOrChargeType.ALLOWANCE;

        public static ReceiptAllowanceCharge Tip(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge.SettlementType = SettlementType.TIP;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge Tip(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.Tip(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge DeliveryFee(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge.SettlementType = SettlementType.DeliveryFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge DeliveryFee(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.DeliveryFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge FreightFee(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge.SettlementType = SettlementType.FreightFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge FreightFee(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.FreightFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge PackagingFee(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge.SettlementType = SettlementType.PackagingFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge PackagingFee(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.PackagingFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge ProcessingFee(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge.SettlementType = SettlementType.PackagingFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge ProcessingFee(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.ProcessingFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge BookingFee(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge.SettlementType = SettlementType.BookingFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge BookingFee(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.BookingFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge GeneralDiscount(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.ALLOWANCE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CREDIT_CUSTOMER_ACCOUNT;
            receiptAllowanceCharge.SettlementType = SettlementType.GeneralDiscount;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge GeneralDiscount(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.GeneralDiscount(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge MultiBuyDiscount(decimal amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge.Description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.ALLOWANCE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CREDIT_CUSTOMER_ACCOUNT;
            receiptAllowanceCharge.SettlementType = SettlementType.MultiBuyDiscount;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge MultiBuyDiscount(decimal amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.MultiBuyDiscount(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }
    }
}