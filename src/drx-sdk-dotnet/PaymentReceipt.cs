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

namespace Net.Dreceiptx
{
    public class PaymentReceipt
    {
        private readonly List<ReceiptAllowanceCharge> _adminFees = new List<ReceiptAllowanceCharge>();
        public string PaymentReceiptId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public string CurrencyCode { get; set; }

        public PaymentReceipt(string paymentReceiptId, PaymentMethod paymentMethod,
            decimal paymentAmount, string currencyCode)
        {
            PaymentReceiptId = paymentReceiptId;
            PaymentMethod = paymentMethod;
            PaymentAmount = paymentAmount;
            CurrencyCode = currencyCode;
        }

        public PaymentAuthorisation PaymentAuthorisationDetail { get; set; }
        public List<ReceiptAllowanceCharge> PaymentAdminFees { get; set; }

        public void AddPaymentAdminFee(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge adminFee = new ReceiptAllowanceCharge("CHARGE",
                "FI", "CHARGE_TO_BE_PAID_BY_CUSTOMER", amount, description, tax);
            _adminFees.Add(adminFee);
        }
    }

    public enum PaymentMethod : short
    {
        NotDefined,
        Cash,
        CreditCard,
        DebitCard,
        Cheque
    }
}