using Net.Dreceiptx.Client;
using Net.Dreceiptx.Receipt.Builders;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Settlement;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.Users;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    /// <summary>
    /// This class contains the code that is documented within the DigitalReceiptExchange
    /// API Documentation (pptx)
    /// </summary>
    [TestFixture]
    public class DocumentationTest
    {
        [Test]
        public void TestDocumentation()
        {
            DigitalReceiptBuilder builder = new DigitalReceiptBuilder(Location.Australia);

            builder.SetMerchantReference("12345678910");;
            builder.SetUserGUID(UserIdentifierType.Email, "test@test.com");
            builder.AddLineItem("Brand", "Name", "Description", quantity: 5, price: 12.99m,
                tax: new Tax(taxableAmount: 12.99m, totalTax: 1.29m));

            StandardLineItem lineItem = new StandardLineItem("Brand", "Name", "Description", quantity: 5, price: 12.99m);
            lineItem.AddTax(new Tax(taxableAmount: 12.99m, totalTax: 1.29m));
            builder.AddLineItem(lineItem);

            Tax otherTax = new Tax(TaxCategory.APPLICABLE, TaxCode.OtherTaxes, taxableAmount: 10.00m, totalTax: 1.00m);

            builder.AddAdminFee(20.00m, "Admin Processing Fee");
            builder.AddGeneralDiscount(20.00m, "Bulk buy discount");
            builder.AddBookingFee(20.00m, "5% Booking Fee");
            builder.AddDeliveryFee(20.00m, "Local/Metro Delivery");
            builder.AddTip(20.00m, "Included Gratuity");

            builder.AddDeliveryFee(20.00m, "Local/Metro Delivery", new Tax(20.00m, 2.00m));

            builder.AddPaymentReceipt(PaymentMethodType.Cash, 57.00m);

            PaymentReceipt paymentReceipt = new PaymentReceipt(PaymentMethodType.CreditCard, 57.00m);
            paymentReceipt.SettlementCurrency = Currency.AustralianDollar;
            paymentReceipt.PaymentAuthorisation = new PaymentAuthorisation("Credit Card Provider", "AuthNumbner", "AuthCode");
            builder.AddPaymentReceipt(paymentReceipt);

            IExchangeClient dRxClient = new DRXClient(Region.Australasia);
            builder.DryRunReceipt = true;
            dRxClient.SendDryRunReceipt(builder.BuildReceiptPostRequest());

            builder.DryRunReceipt = false;
            dRxClient.SendProductionReceipt(builder.BuildReceiptPostRequest());




        }
    }
}
