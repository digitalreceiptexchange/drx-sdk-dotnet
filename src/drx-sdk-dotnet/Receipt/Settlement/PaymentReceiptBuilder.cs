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

using Net.Dreceiptx.Receipt.Common;

namespace Net.Dreceiptx.Receipt.Settlement
{
    public class PaymentReceiptBuilder
    {
        private PaymentReceipt _paymentReceipt;

        public PaymentReceiptBuilder(PaymentMethodType paymentMethodCode, decimal paymentAmount)
        {
            _paymentReceipt = new PaymentReceipt(paymentMethodCode, paymentAmount);
        }

        public PaymentReceiptBuilder SettlementCurrency(Currency currency)
        {
            _paymentReceipt.SettlementCurrency = currency;
            return this;
        }

        public PaymentReceiptBuilder PaymentAuthorisation(string paymentProvider, string authorisationNumber,
            string authorisationCode)
        {
            _paymentReceipt.PaymentAuthorisation = new PaymentAuthorisation(paymentProvider, authorisationNumber, authorisationCode);
            return this;
        }

        public PaymentReceipt Build()
        {
            return _paymentReceipt;
        }
    }
}