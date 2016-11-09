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
using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Ecom;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt.LineItem
{
    [DataContract]
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

        protected LineItem(string brand, string name, string description, int quantity, decimal price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            TransactionalTradeItem = new TransactionalTradeItem();
            TransactionalTradeItem.TradeItemDescriptionInformation = new TradeItemDescriptionInformation(brand, name, description);
            Quantity = quantity;
            Price = price;
        }

        protected LineItem(TradeItemDescriptionInformation tradeItemDescriptionInformation, int quantity, decimal price)
        {
            _transactionalTradeItemType = TransactionalTradeItemType.MANUAL;
            TransactionalTradeItem = new TransactionalTradeItem();
            TransactionalTradeItem.TradeItemDescriptionInformation = tradeItemDescriptionInformation;
            Quantity = quantity;
            Price = price;
        }

        protected LineItem(TransactionalTradeItemType transactionalTradeItemType, string transactionalTradeItemCode,
            int quantity, decimal price)
        {
            _transactionalTradeItemType = transactionalTradeItemType;
            _transactionalTradeItemCode = transactionalTradeItemCode;
            TransactionalTradeItem = new TransactionalTradeItem();
            if (TransTradeItemType == TransactionalTradeItemType.GTIN)
            {
                TransactionalTradeItem.TradeItemDescriptionInformation.Gtin = _transactionalTradeItemCode;
            }
            Quantity = quantity;
            Price = price;
        }

        public string BrandName => TransactionalTradeItem.TradeItemDescriptionInformation.BrandName;

        public string Name => TransactionalTradeItem.TradeItemDescriptionInformation.DescriptionShort;

        public string Description => TransactionalTradeItem.TradeItemDescriptionInformation.TradeItemDescription;

        [DataMember]
        public int LineItemNumber { get; set; }

        //TODO: boolean does not seem right here. Eat in or take away? Yes.
        //transient
        [DataMember(Name = "CreditLineIndicator")]
        public bool ReturnOrExchange { get; set; } = false;

        public void AddReceiptAllowanceCharges(ReceiptAllowanceCharge receiptAllowanceCharge)
        {
            _receiptAllowanceCharges.Add(receiptAllowanceCharge);
        }

        public void SetTradeItemDescriptionInformation(string brand, string name, string description)
        {

            if (TransactionalTradeItem.TradeItemDescriptionInformation != null)
            {
                TransactionalTradeItem.TradeItemDescriptionInformation.BrandName = brand;
                TransactionalTradeItem.TradeItemDescriptionInformation.DescriptionShort = name;
                TransactionalTradeItem.TradeItemDescriptionInformation.TradeItemDescription = description;
            }
            else
            {
                TransactionalTradeItem.TradeItemDescriptionInformation = new TradeItemDescriptionInformation(brand, name, description);
            }
        }

        //[DataMember]
        //public TradeItemDescriptionInformation TradeItemDescriptionInformation { get; set; } = null;

        [DataMember]
        public TransactionalTradeItem TransactionalTradeItem { get; set; }

        protected string TradeItemGroupIdentificationCode
        {
            get { return TransactionalTradeItem.TradeItemDescriptionInformation?.TradeItemGroupIdentificationCode; }
            set {
                if (TransactionalTradeItem.TradeItemDescriptionInformation != null)
                {
                    TransactionalTradeItem.TradeItemDescriptionInformation.TradeItemGroupIdentificationCode = value;
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



        public void AddTradeItemIdentification(string code, string value)
        {
            TransactionalTradeItem.TradeItemDescriptionInformation.ItemIdentification.Add(code, value);
        }

        public bool HasTradeItemIdentificationValue(string code)
        {
            return TransactionalTradeItem.TradeItemDescriptionInformation.ItemIdentification.Contains(code);
        }

        public string getTradeItemIdentificationValue(string code)
        {
            if (TransactionalTradeItem.TradeItemDescriptionInformation.ItemIdentification.Contains(code))
            {
                return TransactionalTradeItem.TradeItemDescriptionInformation.ItemIdentification.Get(code);
            }

            return null;
        }

        public AVPList EcomAVPList => _AVPList;

        public void AddEcomAVP(AVP avp)
        {
            _AVPList.Add(avp);
        }

        public string SerialNumber
        {
            get { return TransactionalTradeItem?.TransactionItemData?.SerialNumber; }
            set { TransactionalTradeItem.TransactionItemData.SerialNumber = value; }
        }

        public string BatchNumber
        {
            get { return TransactionalTradeItem?.TransactionItemData?.BatchNumber; }
            set { TransactionalTradeItem.TransactionItemData.BatchNumber = value; }
        } 
        [DataMember]
        public Identification BillingCostCentre { get; set; } = null;

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

        [DataMember(Name = "AmountExclusiveAllowancesCharges")]
        public decimal SubTotal
        {
            get
            {
                decimal total = Price*Quantity;
                return total;
            }
            set
            {
                // Do nothing. Calculated value?
            }
        }

        [DataMember(Name = "AmountInclusiveAllowancesCharges")]
        public decimal NetTotal
        {
            get
            {
                decimal total = Price*Quantity;
                total += _receiptAllowanceCharges.Sum(x => x.NetTotal);
                return total;
            }
            set
            { 
                // Calculated value
                //do nothing
            }
        }

        public decimal Total
        {
            get
            {
                decimal total = Price*Quantity;
                total += _taxes.Sum(x => x.TaxTotal);
                total += _receiptAllowanceCharges.Sum(x => x.Total);
                return total;
            }
        }

        public decimal TaxesTotal
        {
            get
            {
                decimal total = 0;
                total += _taxes.Sum(x => x.TaxTotal);
                total += _receiptAllowanceCharges.Sum(x => x.TaxesTotal);
                return total;
            }
        }

        public decimal TaxesTotalByTaxCode(TaxCode taxCode)
        {
            decimal total = 0;
            total += _taxes.Where(x => x.TaxCode == taxCode).Sum(x => x.TaxTotal);
            total += _receiptAllowanceCharges.Sum(x => x.TaxesTotal);
            return total;
        }

        public decimal AllowancesTotal
        {
            get
            {
                decimal total = 0;
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

        [DataMember(Name = "InvoicedQuantity")]
        public int Quantity { get; set; }
        [DataMember(Name = "ItemPriceExclusiveAllowancesCharges")]
        public decimal Price { get; set; }

        public bool HasTaxes => _taxes.Any();
    }
}