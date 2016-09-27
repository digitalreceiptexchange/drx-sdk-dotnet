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
namespace Net.Dreceiptx.Receipt.LineItem
{
    public class LineItemBuilder
    {
        private StandardLineItem _lineItem;

        public LineItemBuilder(string brand, string name, string description, int quantity, double price)
        {
            _lineItem = new StandardLineItem(brand, name, description, quantity, price);
        }

        public LineItemBuilder(TransactionalTradeItemType type, string code, int quantity, double price)
        {
            _lineItem = new StandardLineItem(type, code, quantity, price);
        }

        public LineItemBuilder addTax(Tax.Tax tax)
        {
            _lineItem.addTax(tax);
            return this;
        }

        public LineItemBuilder AddGeneralDiscount(double amount, string description)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.GeneralDiscount(amount, description));
            return this;
        }

        public LineItemBuilder AddGeneralDiscount(double amount, string description, Tax tax)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.GeneralDiscount(amount, description, tax));
            return this;
        }

        public LineItemBuilder AddPackagingFee(double amount, string description)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.PackagingFee(amount, description));
            return this;
        }

        public LineItemBuilder AddPackagingFee(double amount, string description, Tax tax)
        {
            _lineItem.AddReceiptAllowanceCharges(ReceiptAllowanceCharge.PackagingFee(amount, description, tax));
            return this;
        }

        public LineItemBuilder setSerialNumber(string serialNumber)
        {
            _lineItem.setSerialNumber(serialNumber);
            return this;
        }

        public LineItemBuilder setBatchNumber(string batchNumber)
        {
            _lineItem.setBatchNumber(batchNumber);
            return this;
        }

        public LineItemBuilder setBillingCostCentre(string billingCostCentre)
        {
            _lineItem.setBillingCostCentre(billingCostCentre);
            return this;
        }

        public LineItemBuilder setDespatchDate(Date despatchDate)
        {
            _lineItem.setDespatchDate(despatchDate);
            return this;
        }

        public LineItemBuilder setDeliveryDate(Date deliveryDate)
        {
            _lineItem.setDeliveryDate(deliveryDate);
            return this;
        }

        public LineItemBuilder setDeliveryInstructions(string deliveryInstructions)
        {
            _lineItem.setDeliveryInstructions(deliveryInstructions);
            return this;
        }

        public LineItem create()
        {
            return _lineItem;
        }
    }
}