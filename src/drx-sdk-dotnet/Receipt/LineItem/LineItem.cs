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
using Net.Dreceiptx.Receipt.Common.Measurements;
using Net.Dreceiptx.Receipt.Ecom;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.Tax;

namespace Net.Dreceiptx.Receipt.LineItem
{
    [DataContract]
    public  class LineItem
    {
        protected List<Tax.Tax> _taxes = new List<Tax.Tax>();
        protected AVPList _AVPList = new AVPList();
        protected TransactionalTradeItemType? _transactionalTradeItemType = null;
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
            ReceiptAllowanceCharges.Add(receiptAllowanceCharge);
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

        public void SetNetContent(double netContent, MeasurementType measurementType)
        {
            if (TransactionalTradeItem.TradeItemMeasurements != null)
            {
                if (TransactionalTradeItem.TradeItemMeasurements.NetContent != null)
                {
                    TransactionalTradeItem.TradeItemMeasurements.NetContent.Value = netContent;
                    TransactionalTradeItem.TradeItemMeasurements.NetContent.MeasurementType = measurementType;
                }
                else
                {
                    TransactionalTradeItem.TradeItemMeasurements.NetContent = new Measurement(netContent, measurementType);
                }
            }
            else
            {
                TransactionalTradeItem.TradeItemMeasurements = new TradeItemMeasurements(netContent, measurementType);
            }
        }

        public void SetMeasurements(double height, double width, double depth, MeasurementType measurementType)
        {
            if (TransactionalTradeItem.TradeItemMeasurements == null)
            {
                TransactionalTradeItem.TradeItemMeasurements = new TradeItemMeasurements();
            }
            TransactionalTradeItem.TradeItemMeasurements.SetMeasurements(height, width, depth, measurementType);
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
            TransactionalTradeItem.ItemIdentification.Add(code, value);
        }

        public bool HasTradeItemIdentificationValue(string code)
        {
            return TransactionalTradeItem.ItemIdentification.Contains(code);
        }

        public string getTradeItemIdentificationValue(string code)
        {
            if (TransactionalTradeItem.ItemIdentification.Contains(code))
            {
                return TransactionalTradeItem.ItemIdentification.Get(code);
            }

            return null;
        }

        [DataMember(Name = "AvpList")]
        public AVPList EcomAVPList
        {
            get { return _AVPList; }
            set { _AVPList = value; }
        }

        public void AddEcomAVP(AVP avp)
        {
            _AVPList.Add(avp);
        }

        public string SerialNumber
        {
            get { return TransactionalTradeItem?.TransactionItemData?.SerialNumber; }
            set
            {
                if (TransactionalTradeItem == null)
                {
                    TransactionalTradeItem = new TransactionalTradeItem();
                }
                if (TransactionalTradeItem.TransactionItemData == null)
                {
                    TransactionalTradeItem.TransactionItemData = new TransactionalItemData();
                }
                TransactionalTradeItem.TransactionItemData.SerialNumber = value;
            }
        }

        public string BatchNumber
        {
            get { return TransactionalTradeItem?.TransactionItemData?.BatchNumber; }
            set
            {
                if (TransactionalTradeItem == null)
                {
                    TransactionalTradeItem = new TransactionalTradeItem();
                }
                if (TransactionalTradeItem.TransactionItemData == null)
                {
                    TransactionalTradeItem.TransactionItemData = new TransactionalItemData();
                }
                TransactionalTradeItem.TransactionItemData.BatchNumber = value;
            }
        } 
        [DataMember]
        public Identification BillingCostCentre { get; set; } = null;

        public DateTime? DespatchDate
        {
            get { return DespatchInformation.DespatchDateTime; }
            set { DespatchInformation.DespatchDateTime = value; }
        }


        public DateTime? DeliveryDate
        {
            get { return DespatchInformation.DeliveryDate; }
            set { DespatchInformation.DeliveryDate = value; }
        }

        public string DeliveryInstructions
        {
            get { return DespatchInformation.DeliveryInstructions; }
            set { DespatchInformation.DeliveryInstructions = value; }
        }

        [DataMember(Name = "ShipFrom")]
        public LocationInformation OriginInformation { get; set; }

        //public bool ShouldSerializeOriginInformation()
        //{
        //    OriginInformation != null && OriginInformation.
        //}

        [DataMember(Name = "ShipTo")]
        public LocationInformation DestinationInformation { get; set; }

        [DataMember]
        public DespatchInformation DespatchInformation { get; set; } = new DespatchInformation();

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
                total += ReceiptAllowanceCharges.Sum(x => x.NetTotal);
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
                total += ReceiptAllowanceCharges.Sum(x => x.SubTotal);
                return total;
            }
        }

        public decimal TaxesTotal
        {
            get
            {
                decimal total = 0;
                total += _taxes.Sum(x => x.TaxTotal);
                total += ReceiptAllowanceCharges.Sum(x => x.TaxesTotal);
                return total;
            }
        }

        public decimal TaxesTotalByTaxCode(TaxCode taxCode)
        {
            decimal total = 0;
            total += _taxes.Where(x => x.TaxCode == taxCode).Sum(x => x.TaxTotal);
            total += ReceiptAllowanceCharges.Sum(x => x.TaxesTotal);
            return total;
        }

        public decimal AllowancesTotal
        {
            get
            {
                decimal total = 0;
                total += ReceiptAllowanceCharges.Sum(x => x.NetTotal);
                return total;
            }
        }

        [DataMember(Name = "InvoiceAllowanceCharge")]
        public List<ReceiptAllowanceCharge> ReceiptAllowanceCharges { get; set; } = new List<ReceiptAllowanceCharge>();

        public void AddTax(Tax.Tax tax)
        {
            _taxes.Add(tax);
        }

        [DataMember(Name = "InvoiceLineTaxInformation")]
        public List<Tax.Tax> Taxes
        {
            get { return _taxes; }
            set { _taxes = value; }
        }

        [DataMember(Name = "InvoicedQuantity")]
        public int Quantity { get; set; }
        [DataMember(Name = "ItemPriceExclusiveAllowancesCharges")]
        public decimal Price { get; set; }

        public bool HasTaxes => _taxes.Any();
    }
}