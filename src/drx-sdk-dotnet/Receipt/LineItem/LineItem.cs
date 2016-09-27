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
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Ecom;

namespace Net.Dreceiptx.Receipt.LineItem
{
    public abstract class LineItem
    {
        //transient
        protected int _quantity;
        //transient
        protected double _price;

        //transient
        protected readonly List<Tax.Tax> _taxes = new List<Tax.Tax>();
        //transient
        protected readonly List<ReceiptAllowanceCharge> _receiptAllowanceCharges = new List<ReceiptAllowanceCharge>();
        //transient
        protected DespatchInformation  _despatchInformation = new DespatchInformation();
        //transient
        protected TradeItemIdentification _tradeItemIdentification = new TradeItemIdentification();
        //transient
        protected TradeItemDescriptionInformation _tradeItemDescriptionInformation = null;
        //transient
        protected AVPList _AVPList = new AVPList();
        //transient
        protected TransactionalTradeItemType? _transactionalTradeItemType = null;
        //transient
        protected string _transactionalTradeItemCode = null;
        //transient
        protected string _serialNumber = null;
        //transient
        protected string _batchNumber = null;
        //transient
        protected string _billingCostCentre = null;
        //transient
        protected LocationInformation _origin = new LocationInformation();
        //transient 
        protected LocationInformation _destination = new LocationInformation();

        public static readonly string LineItemTypeIdentifier = "DRX_LINEITEM_TYPE";

        protected LineItem()
        {

        }

        public LineItem(string brand, string name, string description, int quantity, double price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            _tradeItemDescriptionInformation = new TradeItemDescriptionInformation(brand, name, description);
            _quantity = quantity;
            _price = price;
        }

        public LineItem(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            _tradeItemDescriptionInformation = tradeItemDescriptionInformation;
            _quantity = quantity;
            _price = price;
        }

        public LineItem(TransactionalTradeItemType transactionalTradeItemType, string transactionalTradeItemCode,
            int quantity, double price)
        {
            _transactionalTradeItemType = transactionalTradeItemType;
            _transactionalTradeItemCode = transactionalTradeItemCode;
            _quantity = quantity;
            _price = price;
        }

        public string BrandName => _tradeItemDescriptionInformation.BrandName;

        public string Name => _tradeItemDescriptionInformation.DescriptionShort;

        public string Description => _tradeItemDescriptionInformation.TradeItemDescription;

        //transient
        public int LineItemId { get; set; }

        //TODO: boolean does not seem right here. Eat in or take away? Yes.
        //transient
        public bool ReturnOrExchange { get; set; } = false;

        public void AddReceiptAllowanceCharges(ReceiptAllowanceCharge receiptAllowanceCharge)
        {
            _receiptAllowanceCharges.Add(receiptAllowanceCharge);
        }

        public void SetTradeItemDescriptionInformation(string brand, string name, string description)
        {
            if (_tradeItemDescriptionInformation != null)
            {
                _tradeItemDescriptionInformation.BrandName = brand;
                _tradeItemDescriptionInformation.DescriptionShort = name;
                _tradeItemDescriptionInformation.TradeItemDescription = description;
            }
            else
            {
                _tradeItemDescriptionInformation = new TradeItemDescriptionInformation(brand, name, description);
            }
        }

        public void setTradeItemDescriptionInformation(TradeItemDescriptionInformation tradeItemDescriptionInformation)
        {
            _tradeItemDescriptionInformation = tradeItemDescriptionInformation;
        }

        protected void setTradeItemGroupIdentificationCode(string code)
        {
            if (_tradeItemDescriptionInformation != null)
            {
                _tradeItemDescriptionInformation.TradeItemGroupIdentificationCode = code;
            }
        }

        protected <
        T extends
        Enum<T>
        &
        LineItemTypeDescription
        >

        LineItemTypeDescription getLineItemType(Class<T> lineItemTypeDescription, LineItemTypeDescription defaultValue)
        {
            if (_tradeItemDescriptionInformation != null)
            {
                for (T lineItemTypeDescriptionEnum :
                lineItemTypeDescription.getEnumConstants())
                {
                    if (
                        lineItemTypeDescriptionEnum.code()
                            .equals(_tradeItemDescriptionInformation.getTradeItemGroupIdentificationCode()))
                    {
                        return lineItemTypeDescriptionEnum;
                    }
                }
            }

            return defaultValue;
        }

        public TradeItemDescriptionInformation getTradeItemDescriptionInformation()
        {
            return _tradeItemDescriptionInformation;
        }

        public void setTransactionalTradeItemType(TransactionalTradeItemType transactionalTradeItemType,
            string transactionalTradeItemCode)
        {
            _transactionalTradeItemType = transactionalTradeItemType;
            _transactionalTradeItemCode = transactionalTradeItemCode;
        }

        public TransactionalTradeItemType getTransactionalTradeItemType()
        {
            return _transactionalTradeItemType;
        }

        public string getTransactionalTradeItemCode()
        {
            return _transactionalTradeItemCode;
        }

        public void setTradeItemIdentification(TradeItemIdentification tradeItemIdentification)
        {
            _tradeItemIdentification = tradeItemIdentification;
        }

        public TradeItemIdentification getTradeItemIdentification()
        {
            return _tradeItemIdentification;
        }

        public void addTradeItemIdentification(string code, string value)
        {
            _tradeItemIdentification.add(code, value);
        }

        public bool hasTradeItemIdentificationValue(string code)
        {
            return _tradeItemIdentification.has(code);
        }

        public string getTradeItemIdentificationValue(string code)
        {
            if (_tradeItemIdentification.has(code))
            {
                return _tradeItemIdentification.get(code);
            }

            return null;
        }

        public AVPList getEcomAVPList()
        {
            return this._AVPList;
        }

        public void addEcomAVP(AVP avp)
        {
            this._AVPList.add(avp);
        }

        public void setSerialNumber(string serialNumber)
        {
            _serialNumber = serialNumber;
        }

        public string getSerialNumber()
        {
            return _serialNumber;
        }

        public void setBatchNumber(string batchNumber)
        {
            _batchNumber = batchNumber;
        }

        public string getBatchNumber()
        {
            return _batchNumber;
        }

        public void setBillingCostCentre(string billingCostCentre)
        {
            _billingCostCentre = billingCostCentre;
        }

        public string getBillingCostCentre()
        {
            return _billingCostCentre;
        }

        public void setDespatchDate(Date despatchDate)
        {
            _despatchInformation.setDespatchDate(despatchDate);
        }

        public Date getDespatchDate()
        {
            return _despatchInformation.getDespatchDate();
        }

        public void setDeliveryDate(Date deliveryDate)
        {
            _despatchInformation.setDeliveryDate(deliveryDate);
        }

        public DateTime DeliveryDate
        {
            get { return _despatchInformation.DeliveryDate; }
            set { _despatchInformation.DeliveryDate = value; }
        }

        public void setDespatchInformation(DespatchInformation despatchInformation)
        {
            _despatchInformation = despatchInformation;
        }

        public DespatchInformation getDespatchInformation()
        {
            return _despatchInformation;
        }

        public void setOriginInformation(LocationInformation originInformation)
        {
            _origin = originInformation;
        }

        public LocationInformation getOriginInformation()
        {
            return _origin;
        }

        public void setDestinationInformation(LocationInformation destinationInformation)
        {
            _destination = destinationInformation;
        }

        public LocationInformation getDestinationInformation()
        {
            return _destination;
        }

        public double getSubTotal()
        {
            double total = this._price*this._quantity;
            return total;
        }

        public double getNetTotal()
        {
            double total = this._price*this._quantity;
            for (ReceiptAllowanceCharge receiptAllowanceCharge :
            _receiptAllowanceCharges)
            {
                total += receiptAllowanceCharge.getNetTotal();
            }
            return total;
        }

        public double getTotal()
        {
            double total = this._price*this._quantity;
            for (Tax tax :
            _taxes)
            {
                total += tax.getTaxTotal();
            }
            for (ReceiptAllowanceCharge receiptAllowanceCharge :
            _receiptAllowanceCharges)
            {
                total += receiptAllowanceCharge.getTotal();
            }
            return total;
        }

        public double getTaxesTotal()
        {
            double total = 0;
            for (Tax tax :
            _taxes)
            {
                total += tax.getTaxTotal();
            }
            for (ReceiptAllowanceCharge receiptAllowanceCharge :
            _receiptAllowanceCharges)
            {
                total += receiptAllowanceCharge.getTaxesTotal();
            }
            return total;
        }

        public double getTaxesTotal(TaxCode taxCode)
        {
            double total = 0;
            for (Tax tax :
            _taxes)
            {
                if (tax.getTaxCode().equals(taxCode))
                {
                    total += tax.getTaxTotal();
                }
            }
            for (ReceiptAllowanceCharge receiptAllowanceCharge :
            _receiptAllowanceCharges)
            {
                total += receiptAllowanceCharge.getTaxesTotal(taxCode);
            }
            return total;
        }

        public double getAllowancesTotal()
        {
            double total = 0;
            for (ReceiptAllowanceCharge receiptAllowanceCharge :
            _receiptAllowanceCharges)
            {
                total += receiptAllowanceCharge.getNetTotal();
            }
            return total;
        }

        public List<ReceiptAllowanceCharge> getReceiptAllowanceCharges()
        {
            return _receiptAllowanceCharges;
        }

        public void addTax(Tax tax)
        {
            _taxes.add(tax);
        }

        public List<Tax> getTaxes()
        {
            return _taxes;
        }

        public long getQuantity()
        {
            return _quantity;
        }

        public void setQuantity(int quantity)
        {
            _quantity = quantity;
        }

        public double getPrice()
        {
            return _price;
        }

        public void setPrice(double price)
        {
            _price = price;
        }

        public bool hasTaxes()
        {
            return !_taxes.isEmpty();
        }
    }
}