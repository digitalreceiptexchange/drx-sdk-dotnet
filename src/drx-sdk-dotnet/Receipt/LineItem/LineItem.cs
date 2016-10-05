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
using System.Linq;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Ecom;
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt.LineItem
{
    public abstract class LineItem
    {
        //transient
        protected readonly List<Tax.Tax> _taxes = new List<Tax.Tax>();
        //transient
        protected readonly List<ReceiptAllowanceCharge> _receiptAllowanceCharges = new List<ReceiptAllowanceCharge>();
        //transient
        protected AVPList _AVPList = new AVPList();
        //transient
        protected TransactionalTradeItemType? _transactionalTradeItemType = null;
        //transient
        protected string _transactionalTradeItemCode = null;
        public static readonly string LineItemTypeIdentifier = "DRX_LINEITEM_TYPE";

        protected LineItem()
        {
        }

        public LineItem(string brand, string name, string description, int quantity, double price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            ItemDescriptionInformation = new TradeItemDescriptionInformation(brand, name, description);
            Quantity = quantity;
            Price = price;
        }

        public LineItem(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, double price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            ItemDescriptionInformation = tradeItemDescriptionInformation;
            Quantity = quantity;
            Price = price;
        }

        public LineItem(TransactionalTradeItemType transactionalTradeItemType, string transactionalTradeItemCode,
            int quantity, double price)
        {
            _transactionalTradeItemType = transactionalTradeItemType;
            _transactionalTradeItemCode = transactionalTradeItemCode;
            Quantity = quantity;
            Price = price;
        }

        public string BrandName => ItemDescriptionInformation.BrandName;

        public string Name => ItemDescriptionInformation.DescriptionShort;

        public string Description => ItemDescriptionInformation.TradeItemDescription;

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
            if (ItemDescriptionInformation != null)
            {
                ItemDescriptionInformation.BrandName = brand;
                ItemDescriptionInformation.DescriptionShort = name;
                ItemDescriptionInformation.TradeItemDescription = description;
            }
            else
            {
                ItemDescriptionInformation = new TradeItemDescriptionInformation(brand, name, description);
            }
        }

        //transient
        public TradeItemDescriptionInformation ItemDescriptionInformation { get; set; } = null;

        protected string TradeItemGroupIdentificationCode
        {
            get { return ItemDescriptionInformation?.TradeItemGroupIdentificationCode; }
            set {
                if (ItemDescriptionInformation != null)
                {
                    ItemDescriptionInformation.TradeItemGroupIdentificationCode = value;
                }
            }
            
        }

        //protected <
        //T extends
        //Enum<T>
        //&
        //LineItemTypeDescription
        //>

        //LineItemTypeDescription getLineItemType(Class<T> lineItemTypeDescription, LineItemTypeDescription defaultValue)
        //{
        //    if (_tradeItemDescriptionInformation != null)
        //    {
        //        for (T lineItemTypeDescriptionEnum :
        //        lineItemTypeDescription.getEnumConstants())
        //        {
        //            if (
        //                lineItemTypeDescriptionEnum.code()
        //                    .equals(_tradeItemDescriptionInformation.getTradeItemGroupIdentificationCode()))
        //            {
        //                return lineItemTypeDescriptionEnum;
        //            }
        //        }
        //    }

        //    return defaultValue;
        //}



        //TODO: why does this take two parameters and not just a simple getter/setter?
        public void SetTransactionalTradeItemType(TransactionalTradeItemType transactionalTradeItemType,
            string transactionalTradeItemCode)
        {
            _transactionalTradeItemType = transactionalTradeItemType;
            _transactionalTradeItemCode = transactionalTradeItemCode;
        }

        public TransactionalTradeItemType? TransTradeItemType => _transactionalTradeItemType;

        public string TransTradeItemCode => _transactionalTradeItemCode;

        //transient
        public TradeItemIdentification ItemIdentification { get; set; } = new TradeItemIdentification();

        public void AddTradeItemIdentification(string code, string value)
        {
            ItemIdentification.Add(code, value);
        }

        public bool HasTradeItemIdentificationValue(string code)
        {
            return ItemIdentification.Contains(code);
        }

        public string getTradeItemIdentificationValue(string code)
        {
            if (ItemIdentification.Contains(code))
            {
                return ItemIdentification.Get(code);
            }

            return null;
        }

        public AVPList EcomAVPList => _AVPList;

        public void AddEcomAVP(AVP avp)
        {
            _AVPList.Add(avp);
        }

        //transient
        public string SerialNumber { get; set; } = null;
        //transient
        public string BatchNumber { get; set; } = null;
        //transient
        public string BillingCostCentre { get; set; } = null;

        public DateTime DespatchDate
        {
            get { return _information.DespatchDate; }
            set { _information.DespatchDate = value; }
        }


        public DateTime DeliveryDate
        {
            get { return _information.DeliveryDate; }
            set { _information.DeliveryDate = value; }
        }

        public string DeliveryInstructions
        {
            get { return _information.DeliveryInstructions; }
            set { _information.DeliveryInstructions = value; }
        }

        //transient
        private DespatchInformation _information  = new DespatchInformation();
        //transient
        public LocationInformation Origin { get; set; } = new LocationInformation();
        //transient 
        public LocationInformation Destination { get; set; } = new LocationInformation();


        public double SubTotal
        {
            get
            {
                double total = Price*Quantity;
                return total;
            }
        }

        public double NetTotal
        {
            get
            {
                double total = Price*Quantity;
                total += _receiptAllowanceCharges.Sum(x => x.NetTotal);
                return total;
            }
        }

        public double Total
        {
            get
            {
                double total = Price*Quantity;
                total += _taxes.Sum(x => x.TaxTotal);
                total += _receiptAllowanceCharges.Sum(x => x.Total);
                return total;
            }
        }

        public double TaxesTotal
        {
            get
            {
                double total = 0;
                total += _taxes.Sum(x => x.TaxTotal);
                total += _receiptAllowanceCharges.Sum(x => x.TaxesTotal);
                return total;
            }
        }

        public double TaxesTotalByTaxCode(TaxCode taxCode)
        {
            double total = 0;
            total += _taxes.Where(x => x.TaxCode == taxCode).Sum(x => x.TaxTotal);
            total += _receiptAllowanceCharges.Sum(x => x.TaxesTotal);
            return total;
        }

        public double AllowancesTotal
        {
            get
            {
                double total = 0;
                total += _receiptAllowanceCharges.Sum(x => x.NetTotal);
                return total;
            }
        }

        public List<ReceiptAllowanceCharge> ReceiptAllowanceCharges => _receiptAllowanceCharges;

        public void AddTax(Tax.Tax tax)
        {
            _taxes.Add(tax);
        }

        public List<Tax.Tax> Taxes => _taxes;

        //transient
        public int Quantity { get; set; }
        //transient
        public double Price { get; set; }

        public bool HasTaxes => _taxes.Any();
    }
}