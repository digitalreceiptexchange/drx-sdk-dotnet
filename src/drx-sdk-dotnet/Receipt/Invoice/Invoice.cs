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
using System.Threading;
using Net.Dreceiptx.Extensions;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.Receipt.Validation;

namespace Net.Dreceiptx.Receipt.Invoice
{
    [DataContract]
    public class Invoice
    {
        private static int _lineItemId = 1;
        private static int _allowanceOrChargeId = 1;
        private List<ReceiptAllowanceCharge> _allowanceOrCharges = new List<ReceiptAllowanceCharge>();
        private Dictionary<string, string> _companyTaxNumbers = new Dictionary<string, string>();
        private string _defaultTimeZone;

        public Invoice()
        {
        }

        public Invoice(IConfigManager configManager)
        {
            InvoiceCurrencyCode = configManager.GetConfigValue("default.currency");
            CountryOfSupplyOfGoods = configManager.GetConfigValue("default.country");
            _defaultTimeZone = configManager.GetConfigValue("default.timezone");
            CreationDateTime = new DateTime();
        }

        [DataMember]
        public string DocumentStatusCode { get; set; } = "ORIGINAL";

        [DataMember]
        public string InvoiceType { get; set; } = "TAX_INVOICE";

        //TODO: This does not appear to be serialized on the java side of thigns?
        public string MerchantName { get; set; }

        public string GetCompanyTaxNumber(TaxCode taxCode)
        {
            return _companyTaxNumbers.GetOrNull(taxCode.Value());
        }

        public void AddCompanyTaxNumber(string taxCode, string taxNumber)
        {
            _companyTaxNumbers.Add(taxCode, taxNumber);
        }

        [DataMember]
        public Identification PurchaseOrder { get; set; }

        [DataMember]
        public Identification CustomerReference { get; set; }

        [DataMember]
        public Identification InvoiceIdentification { get; set; }

        [DataMember]
        public DateTime? CreationDateTime { get; set; }

        [DataMember]
        public string InvoiceCurrencyCode { get; set; }

        [DataMember]
        public string CountryOfSupplyOfGoods { get; set; }

        [DataMember(Name = "InvoiceLineItem")]
        public List<LineItem.LineItem> InvoiceLineItems { get; set; } = new List<LineItem.LineItem>();

        [DataMember(Name = "InvoiceAllowanceCharge")]
        public List<ReceiptAllowanceCharge> AllowanceOrCharges => _allowanceOrCharges;

        [DataMember(Name = "ShipFrom")]
        public LocationInformation OriginInformation { get; set; } = new LocationInformation();

        [DataMember(Name = "ShipTo")]
        public LocationInformation DestinationInformation { get; set; } = new LocationInformation();

        [DataMember]
        public DespatchInformation DespatchInformation { get; set; } = new DespatchInformation();

        [DataMember]
        public InvoiceSummary InvoiceTotals
        {
            get
            {
                return new InvoiceSummary
                {
                    TotalInvoiceAmount = new Amount(EnumExtensions.Currency(InvoiceCurrencyCode), SubTotal),
                    TotalLineAmountInclusiveAllowanceesCharges =
                        new Amount(EnumExtensions.Currency(InvoiceCurrencyCode), Total),
                    TotalTaxAmount = new Amount(EnumExtensions.Currency(InvoiceCurrencyCode), TaxesTotal),
                };
            }
            set { /* do nothing */ }
        }

        public decimal Total => SubTotal + TaxesTotal + SubTotalAllowances - SubTotalCharges;

        public decimal TaxPercentage
        {
            get
            {
                decimal subTotal = SubTotal + SubTotalAllowances - SubTotalCharges;
                decimal taxPercentage = 0;
                if (subTotal != 0)
                {
                    taxPercentage = (TaxesTotal/subTotal)*100;
                }
                return taxPercentage;
            }
        }

        public decimal SubTotal => InvoiceLineItems.Sum(x => x.Total);

        public decimal TaxesTotal
        {
            get
            {
                decimal total = 0;
                total += InvoiceLineItems.Sum(x => x.TaxesTotal);
                total += _allowanceOrCharges.Sum(x => x.TaxesTotal);
                return total;
            }
        }

        public decimal TaxesTotalByTaxCode(TaxCode taxCode)
        {
            decimal total = 0;
            total += InvoiceLineItems.Sum(x => x.TaxesTotalByTaxCode(taxCode));
            total += _allowanceOrCharges.Sum(x => x.TaxesTotalByTaxCode(taxCode));
            return total;
        }

        public decimal SubTotalCharges => _allowanceOrCharges.Where(x => x.IsCharge).Sum(x => x.SubTotal);

        public decimal SubTotalAllowances => _allowanceOrCharges.Where(x => x.IsAllowance).Sum(x => x.SubTotal);

        public int AddLineItem(LineItem.LineItem lineItem)
        {
            lineItem.LineItemNumber = Interlocked.Add(ref _lineItemId, 1);
            InvoiceLineItems.Add(lineItem);
            return lineItem.LineItemNumber;
        }

        public void RemoveLineItem(int lineItemId)
        {
            LineItem.LineItem item = null;
            foreach (LineItem.LineItem lineItem in InvoiceLineItems)
            {
                if (lineItem.LineItemNumber == lineItemId)
                {
                    item = lineItem;
                    break;
                }
            }
            if (item != null)
            {
                InvoiceLineItems.Remove(item);
            }
        }

        public bool AddAllowanceOrCharge(ReceiptAllowanceCharge receiptAllowanceCharge)
        {
            _allowanceOrCharges.Add(receiptAllowanceCharge);
            return true;
        }

        public void RemoveAllowanceOrChange(int id)
        {
            ReceiptAllowanceCharge item = null;
            foreach (ReceiptAllowanceCharge receiptAllowanceCharge in _allowanceOrCharges)
            {
                if (receiptAllowanceCharge.Id == id)
                {
                    item = receiptAllowanceCharge;
                    break;
                }
            }
            if (item != null)
            {
                _allowanceOrCharges.Remove(item);
            }
        }

        public ReceiptValidation Validate(ReceiptValidation receiptValidation)
        {
            if (!InvoiceLineItems.Any())
            {
                receiptValidation.AddError(ValidationErrors.ReceiptMustHaveALeastLineItem);
            }

            return receiptValidation;
        }
    }
}