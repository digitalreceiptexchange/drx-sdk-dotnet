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
using Net.Dreceiptx.Extensions;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;

namespace Net.Dreceiptx.Receipt.Invoice
{
    public class Invoice
    {
        //@SerializedName("documentStatusCode")
        private readonly string _documentStatusCode = "ORIGINAL";
        //@SerializedName("invoiceType")
        private readonly string _invoiceType = "TAX_INVOICE";



        private readonly static int _lineItemId = 1;
        private readonly static int _allowanceOrChargeId = 1;
        //transient
        private List<ReceiptAllowanceCharge> _allowanceOrCharges = new List<ReceiptAllowanceCharge>();
        //transient 
        private Dictionary<string, string> _companyTaxNumbers = new Dictionary<string, string>();
        //transient
        private string _defaultTimeZone;
        //transient
        private string _dateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";

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

        //transient
        public string MerchantName { get; set; }

        public string getCompanyTaxNumber(TaxCode taxCode)
        {
            return _companyTaxNumbers.GetOrNull(taxCode.V.getValue());
        }

        public void addCompanyTaxNumber(string taxCode, string taxNumber)
        {
            _companyTaxNumbers.put(taxCode, taxNumber);
        }

        //transient
        public string PurchaseOrder { get; set; }

        //transient
        public string CustomerReference { get; set; }

        //transient
        public DateTime? CreationDateTime { get; set; } = null;

        public string CreationDateTimeString => CreationDateTime?.ToString(_dateTimeFormat);

        //transient
        public string Identification { get; set; }

        //@SerializedName("invoiceCurrencyCode")
        public string InvoiceCurrencyCode { get; set; }

        //@SerializedName("countryOfSupplyOfGoods")
        public string CountryOfSupplyOfGoods { get; set; }

        //transient
        public List<LineItem> InvoiceLineItems { get; set; } = new List<LineItem>();

        public List<ReceiptAllowanceCharge> getAllowanceOrCharges()
        {
            return _allowanceOrCharges;
        }

        //transient
        public LocationInformation OriginInformation { get; set; } = new LocationInformation();

        //transient
        public LocationInformation DestinationInformation { get; private set; } = new LocationInformation();

        //transient
        public DespatchInformation Information { get; set; } = new DespatchInformation();

        public double Total
        {
            get
            {
                return this.getSubTotal() + this.getTaxesTotal() + this.getSubTotalAllowances() -
                       this.getSubTotalCharges();
            }
        }

        public double getTaxPercentage()
        {
            double subTotal = this.getSubTotal() + this.getSubTotalAllowances() - this.getSubTotalCharges();
            double taxPercentage = 0;
            if (subTotal != 0)
            {
                taxPercentage = (this.getTaxesTotal()/subTotal)*100;
            }
            return taxPercentage;
        }

        private bool isNullOrWhiteSpace(string value)
        {
            return value == null || value.isEmpty();
        }

        public double SubTotal
        {
            get
            {
                return InvoiceLineItems.Sum(x => x);
                
            }
        }

        public double getTaxesTotal()
        {
            double total = 0;
            for (LineItem lineItem :
            InvoiceLineItems)
            {
                total += lineItem.getTaxesTotal();
            }
            for (ReceiptAllowanceCharge allowanceCharge :
            _allowanceOrCharges)
            {
                total += allowanceCharge.getTaxesTotal();
            }
            return total;
        }

        public double getTaxesTotal(TaxCode taxCode)
        {
            double total = 0;
            for (LineItem lineItem :
            InvoiceLineItems)
            {
                total += lineItem.getTaxesTotal(taxCode);
            }
            for (ReceiptAllowanceCharge allowanceCharge :
            _allowanceOrCharges)
            {
                total += allowanceCharge.getTaxesTotal(taxCode);
            }
            return total;
        }

        public double getSubTotalCharges()
        {
            double total = 0;
            for (ReceiptAllowanceCharge allowanceCharge :
            _allowanceOrCharges)
            {
                if (allowanceCharge.isCharge())
                {
                    total += allowanceCharge.getSubTotal();
                }
            }
            return total;
        }

        public double getSubTotalAllowances()
        {
            double total = 0;
            for (ReceiptAllowanceCharge allowanceCharge :
            _allowanceOrCharges)
            {
                if (allowanceCharge.isAllowance())
                {
                    total += allowanceCharge.getSubTotal();
                }
            }
            return total;
        }

        public int addLineItem(LineItem lineItem)
        {
            lineItem.setLineItemId(_lineItemId.getAndIncrement());
            InvoiceLineItems.add(lineItem);
            return lineItem.getLineItemId();
        }

        public void removeLineItem(int lineItemId)
        {
            LineItem item = null;
            for (LineItem lineItem :
            InvoiceLineItems)
            {
                if (lineItem.getLineItemId() == lineItemId)
                {
                    item = lineItem;
                    break;
                }
            }
            if (item != null)
            {
                InvoiceLineItems.remove(item);
            }
        }

        public bool addAllowanceOrCharge(ReceiptAllowanceCharge receiptAllowanceCharge)
        {
            _allowanceOrCharges.add(receiptAllowanceCharge);
            return true;
        }

        public void removeAllowanceOrChange(int id)
        {
            ReceiptAllowanceCharge item = null;
            for (ReceiptAllowanceCharge receiptAllowanceCharge :
            _allowanceOrCharges)
            {
                if (receiptAllowanceCharge.getId() == id)
                {
                    item = receiptAllowanceCharge;
                    break;
                }
            }
            if (item != null)
            {
                _allowanceOrCharges.remove(item);
            }
        }

        public ReceiptValidation validate(ReceiptValidation receiptValidation)
        {
            if (InvoiceLineItems.size() < 1)
            {
                receiptValidation.AddError(ReceiptMustHaveALeastLineItem);
            }

            return receiptValidation;
        }
    }
}