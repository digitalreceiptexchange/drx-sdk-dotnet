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
using System.Runtime.Remoting.Messaging;
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
        private static int _lineItemId = 0;
        private static int _allowanceOrChargeId = 1;
        private List<ReceiptAllowanceCharge> _allowanceOrCharges = new List<ReceiptAllowanceCharge>();
        private Dictionary<string, string> _companyTaxNumbers = new Dictionary<string, string>();
        private string _defaultTimeZone;

        public Invoice()
        {
            CreationDateTime = new DateTime();
        }

        public Invoice(Location location)  : this()
        {
            InvoiceCurrencyCode = location.Currency.Value();
            CountryOfSupplyOfGoods = location.Country.Value();
            
        }
        public Invoice(IConfigManager configManager) :this()
        {
            InvoiceCurrencyCode = configManager.GetConfigValue(ConfigKeys.DefaultCurrency);
            CountryOfSupplyOfGoods = configManager.GetConfigValue(ConfigKeys.DefaultCountry);
            _defaultTimeZone = configManager.GetConfigValue(ConfigKeys.DefaultTimeZone);
        }

        [DataMember]
        public string DocumentStatusCode { get; set; } = "ORIGINAL";

        [DataMember]
        public string InvoiceType { get; set; } = "TAX_INVOICE";

        public string MerchantName
        {
            get { return Seller?.OrganisationDetails?.OrganisationName; }
            set
            {
                if (Seller == null)
                {
                    Seller = new TransactionalParty();
                }
                if (Seller.OrganisationDetails == null)
                {
                    Seller.OrganisationDetails = new Organisation();
                }
                Seller.OrganisationDetails.OrganisationName = value;
            }
        }

        public string CompanyTaxNumber
        {
            get { return Seller?.DutyFeeTaxRegistration?.DutyFeeTaxRegistationID; }
            set
            {
                if (Seller == null)
                {
                    Seller = new TransactionalParty();
                }
                if (Seller.DutyFeeTaxRegistration == null)
                {
                    Seller.DutyFeeTaxRegistration = new DutyFeeTaxRegistration();
                    Seller.DutyFeeTaxRegistration.DutyFeeTaxRegistationID = value;
                    Seller.DutyFeeTaxRegistration.DutyFeeTaxTypeCode = TaxCode.GoodsAndServicesTax;
                }

            }
        }

        [DataMember]
        public TransactionalParty Seller { get; set; }

        [DataMember(Name = "BillTo")]
        public TransactionalParty BillingInformation { get; set; }

        public string BillingOrganisationName => BillingInformation?.OrganisationDetails?.OrganisationName;

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
                    TotalInvoiceAmount = new Amount(EnumExtensions.Currency(InvoiceCurrencyCode), GetSubTotal()),
                    TotalLineAmountInclusiveAllowanceesCharges =
                        new Amount(EnumExtensions.Currency(InvoiceCurrencyCode), Total),
                    TotalTaxAmount = new Amount(EnumExtensions.Currency(InvoiceCurrencyCode), TaxesTotal),
                };
            }
            set { /* do nothing */ }
        }


        [DataMember(Name = "SalesOrder")]
        public Identification SalesOrderReference { get; set; }

        public decimal Total => GetSubTotal() + TaxesTotal + SubTotalAllowances - SubTotalCharges;

        public decimal NetTotal
        {
            get
            {
                decimal total = GetSubTotal();
                total += SubTotalCharges;
                total -= SubTotalAllowances;
                return total;
            }
        }

        public decimal TaxPercentage
        {
            get
            {
                decimal subTotal = GetSubTotal() + SubTotalAllowances - SubTotalCharges;
                decimal taxPercentage = 0;
                if (subTotal != 0)
                {
                    taxPercentage = (TaxesTotal/subTotal)*100;
                }
                return taxPercentage;
            }
        }

        public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (var lineItem in InvoiceLineItems)
            {
                if(lineItem.ReturnOrExchange)
                {
                    subTotal -= lineItem.SubTotal;
                }
                else
                {
                    subTotal += lineItem.SubTotal;
                }
            }
            return subTotal;
        }

        public decimal TaxesTotal
        {
            get
            {
                decimal total = 0;
                total += InvoiceLineItems.Sum(x => x.ReturnOrExchange? -1 * x.TaxesTotal : x.TaxesTotal);
                total += _allowanceOrCharges.Sum(x => x.IsCharge? x.TaxesTotal : -1 * x.TaxesTotal);
                return total;
            }
        }

        public decimal TaxesTotalByTaxCode(TaxCode taxCode)
        {
            decimal total = 0;
            total += InvoiceLineItems.Sum(x => x.GetTaxTotal(taxCode));
            total += _allowanceOrCharges.Sum(x => x.GetTaxTotal(taxCode));
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