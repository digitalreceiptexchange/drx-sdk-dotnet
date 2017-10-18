using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.Receipt.AllowanceCharge;

namespace Net.Dreceiptx.UnitTests.Receipt.Invoice
{
    [TestFixture]
    public class InvoiceTest
    {
        [Test]
        public void testSubTotalReturnsCorrectValue()
        {
            Net.Dreceiptx.Receipt.Invoice.Invoice invoice = new Net.Dreceiptx.Receipt.Invoice.Invoice();
            invoice.AddLineItem(createStandardLineItem(1, 10.1m));
            invoice.AddLineItem(createStandardLineItem(2, 100));

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem = createStandardLineItem(1, 10);
            lineItem.ReturnOrExchange = true;
            invoice.AddLineItem(lineItem);

            Assert.AreEqual(200.10, invoice.GetSubTotal());
        }

        [Test]
        public void testTaxesTotalReturnsCorrectValue()
        {
            Net.Dreceiptx.Receipt.Invoice.Invoice invoice = new Net.Dreceiptx.Receipt.Invoice.Invoice();
            invoice.AddLineItem(createStandardLineItem(1, 10.1m));

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem1 = createStandardLineItem(2, 100);
            lineItem1.AddTax(new Tax(200, 20));
            invoice.AddLineItem(lineItem1);

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem2 = createStandardLineItem(1, 10);
            lineItem2.ReturnOrExchange = true;
            invoice.AddLineItem(lineItem2);

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem3 = createStandardLineItem(1, 100);
            lineItem3.ReturnOrExchange = true;
            lineItem3.AddTax(new Tax(100, 10));
            invoice.AddLineItem(lineItem3);

            Assert.AreEqual(("100.10"), invoice.GetSubTotal());
            Assert.AreEqual(("10.0"), invoice.TaxesTotal);
        }

        [Test]
        public void testTaxesTotalByTaxCodeReturnsCorrectValue()
        {
            Net.Dreceiptx.Receipt.Invoice.Invoice invoice = new Net.Dreceiptx.Receipt.Invoice.Invoice();
            invoice.AddLineItem(createStandardLineItem(1, 10.1m));

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem1 = createStandardLineItem(2, 100);
            lineItem1.AddTax(new Tax(TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax, 200, 20));
            invoice.AddLineItem(lineItem1);

            lineItem1 = createStandardLineItem(2, 100);
            lineItem1.AddTax(new Tax(TaxCategory.APPLICABLE, TaxCode.CoffeeTax, 200, 20));
            invoice.AddLineItem(lineItem1);

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem2 = createStandardLineItem(1, 10);
            lineItem2.ReturnOrExchange = true;
            invoice.AddLineItem(lineItem2);

            Net.Dreceiptx.Receipt.LineItem.LineItem lineItem3 = createStandardLineItem(1, 100);
            lineItem3.ReturnOrExchange = true;
            lineItem3.AddTax(new Tax(TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax, 100, 10));
            invoice.AddLineItem(lineItem3);


            invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.BookingFee(50, "Booking Fee",
                    new Tax(TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax, 50, 5)));
            invoice.AddAllowanceOrCharge(ReceiptAllowanceCharge.GeneralDiscount(30, "Discount",
                    new Tax(TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax, 80, 8)));

            Assert.AreEqual(300.10, invoice.GetSubTotal());
            Assert.AreEqual(27.0, invoice.TaxesTotal);
            Assert.AreEqual(7.0, invoice.TaxesTotalByTaxCode(TaxCode.GoodsAndServicesTax));
            Assert.AreEqual(20.0, invoice.TaxesTotalByTaxCode(TaxCode.CoffeeTax));

        }

        private Net.Dreceiptx.Receipt.LineItem.LineItem createStandardLineItem(int quantity, decimal price)
        {
            return new StandardLineItem("Brand", "Name",
                    "description", quantity, price);
        }
    }
}
