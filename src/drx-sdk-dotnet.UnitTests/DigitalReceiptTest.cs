using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    /// <summary>
    /// Test class for the DigitalRecipt class
    /// </summary>
    [TestFixture]
    public class DigitalReceiptTest
    {

        [Test]
        public void TestCreation()
        {
            DigitalReceipt digitalReceipt = new DigitalReceipt();



            //Assert.AreEqual("AUD", digitalReceipt.InvoiceCurrencyCode);
            //Assert.AreEqual("AU", digitalReceipt.CountryCode);
            //Assert.AreEqual("NOTSURE", digitalReceipt.MerchantGLN);

            LineItem lineItem = new LineItem(100, 10.01m, 3);
            lineItem.AddGeneralDiscount(1, "1 dollar discount for x", null);
            lineItem.AddMultibuyDiscount(2, "Buying 100 so slight discount", null);
            lineItem.AddTaxFee(new TaxFee(10));
            lineItem.GTIN = "00012345600012";
            lineItem.ISBN = "1234567890ABC";

            digitalReceipt.AddLineItem(lineItem);

        }


        /// <summary>
        /// Test to ensure that all the errors are reported 
        /// when nothing has been added to the DigitalReceipt
        /// </summary>
        [Test]
        public void TestValidateReturnsCorrectErrorsWhenAllChecksFail()
        {
            DigitalReceipt digitalReceipt = new DigitalReceipt();
            ReceiptValidation validationResult = digitalReceipt.Validate();
            Assert.IsFalse(validationResult.IsValid);
        }
    }
}

             