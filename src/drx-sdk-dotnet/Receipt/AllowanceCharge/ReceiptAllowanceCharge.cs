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
using System.Collections.Generic;

namespace Net.Dreceiptx.Receipt.AllowanceCharge
{
    public class ReceiptAllowanceCharge
    {
        //TODO: ID should not be serialized
        private int _id;
        //@SerializedName("allowanceOrChargeType")
        public AllowanceOrChargeType AllowanceOrChargeType { get; set; }
        //@SerializedName("allowanceChargeType")
        public AllowanceChargeType AllowanceChargeType { get; set; }
        //@SerializedName("settlementType")
        private SettlementType _settlementType;
        //@SerializedName("baseAmount")
        public double BaseAmount { get; set; }
        //@SerializedName("allowanceChargeDescription")
        private string _description;
        //@SerializedName("leviedDutyFeeTax")
        private List<Tax.Tax> _taxes = new List<Tax.Tax>();

        //TODO: If we keep it this should not be a double
        public double Id => _id;

        public SettlementType SettlementType => _settlementType;

        public double SubTotal => BaseAmount;

        public string Description => _description;

        public double Total => BaseAmount + TaxesTotal;

        public double NetTotal => BaseAmount;

        public bool HasTaxes => _taxes.Count != 0;

        public double TaxesTotal
        {
            get
            {
                double totalTaxes = 0.0;
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
        public double TaxesTotalByTaxCode(TaxCode taxCode)
        {
            double totalTaxes = 0.0;
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

        public static ReceiptAllowanceCharge Tip(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge._settlementType = SettlementType.TIP;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge Tip(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.Tip(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge DeliveryFee(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge._settlementType = SettlementType.DeliveryFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge DeliveryFee(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.DeliveryFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge FreightFee(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge._settlementType = SettlementType.FreightFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge FreightFee(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.FreightFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge PackagingFee(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge._settlementType = SettlementType.PackagingFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge PackagingFee(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.PackagingFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge ProcessingFee(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge._settlementType = SettlementType.PackagingFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge ProcessingFee(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.ProcessingFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge BookingFee(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.CHARGE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CHARGE_TO_BE_PAID_BY_CUSTOMER;
            receiptAllowanceCharge._settlementType = SettlementType.BookingFee;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge BookingFee(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.BookingFee(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge GeneralDiscount(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.ALLOWANCE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CREDIT_CUSTOMER_ACCOUNT;
            receiptAllowanceCharge._settlementType = SettlementType.GeneralDiscount;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge GeneralDiscount(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.GeneralDiscount(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge MultiBuyDiscount(double amount, string description)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge();
            receiptAllowanceCharge.BaseAmount = amount;
            receiptAllowanceCharge._description = description;
            receiptAllowanceCharge.AllowanceOrChargeType = AllowanceOrChargeType.ALLOWANCE;
            receiptAllowanceCharge.AllowanceChargeType = AllowanceChargeType.CREDIT_CUSTOMER_ACCOUNT;
            receiptAllowanceCharge._settlementType = SettlementType.MultiBuyDiscount;
            return receiptAllowanceCharge;
        }

        public static ReceiptAllowanceCharge MultiBuyDiscount(double amount, string description, Tax.Tax tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = ReceiptAllowanceCharge.MultiBuyDiscount(amount, description);
            receiptAllowanceCharge._taxes.Add(tax);
            return receiptAllowanceCharge;
        }
    }
}