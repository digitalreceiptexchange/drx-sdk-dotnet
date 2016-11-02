using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Receipt.Common;

namespace Net.Dreceiptx.Receipt.Invoice
{
    public class InvoiceSummary
    {
        public Amount TotalInvoiceAmount { get; set; }
        public Amount TotalLineAmountInclusiveAllowanceesCharges { get; set; }
        public Amount TotalTaxAmount { get; set; }
    }
}
