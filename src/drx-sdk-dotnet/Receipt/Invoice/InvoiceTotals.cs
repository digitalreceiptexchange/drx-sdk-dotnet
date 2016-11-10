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
