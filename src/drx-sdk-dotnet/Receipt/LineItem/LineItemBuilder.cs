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
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Invoice;

namespace Net.Dreceiptx.Receipt.LineItem
{
    public class LineItemBuilder
    {
        private StandardLineItem _lineItem;

        public LineItemBuilder(string brand, string name, string description, int quantity, decimal price)
        {
            _lineItem = new StandardLineItem(brand, name, description, quantity, price);
        }

        public LineItemBuilder(TransactionalTradeItemType type, string code, int quantity, decimal price)
        {
            _lineItem = new StandardLineItem(type, code, quantity, price);
        }

        public LineItemBuilder addTax(Tax.Tax tax)
        {
            _lineItem.AddTax(tax);
            return this;
        }

        public LineItemBuilder AddGeneralDiscount(decimal amount, string description)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.GeneralDiscount(amount, description));
            return this;
        }

        public LineItemBuilder AddGeneralDiscount(decimal amount, string description, Tax.Tax tax)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.GeneralDiscount(amount, description, tax));
            return this;
        }

        public LineItemBuilder AddPackagingFee(decimal amount, string description)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.PackagingFee(amount, description));
            return this;
        }

        public LineItemBuilder AddPackagingFee(decimal amount, string description, Tax.Tax tax)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.PackagingFee(amount, description, tax));
            return this;
        }

        public LineItemBuilder SerialNumber(string serialNumber)
        {
            _lineItem.SerialNumber = serialNumber;
            return this;
        }

        public LineItemBuilder BatchNumber(string batchNumber)
        {
            _lineItem.BatchNumber = batchNumber;
            return this;
        }

        public LineItemBuilder BillingCostCentre(string billingCostCentre)
        {
            _lineItem.BillingCostCentre = new Identification(billingCostCentre);
            return this;
        }

        public LineItemBuilder DespatchDate(DateTime despatchDate)
        {
            _lineItem.DespatchDate = despatchDate;
            return this;
        }

        public LineItemBuilder DeliveryDate(DateTime deliveryDate)
        {
            _lineItem.DeliveryDate = deliveryDate;
            return this;
        }

        public LineItemBuilder SetDeliveryInstructions(string deliveryInstructions)
        {
            _lineItem.DeliveryInstructions = deliveryInstructions;
            return this;
        }

        public LineItem Build()
        {
            return _lineItem;
        }
    }
}